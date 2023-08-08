// ITNOA

#addin nuget:?package=Cake.Coverlet&version=3.0.4
#addin nuget:?package=Cake.Docker&version=1.2.0
#addin nuget:?package=Cake.FileHelpers&version=6.1.3

var target = Argument("target", "Default");
var profile = Argument("profile", IsRunningOnWindows() ? "dotnetFramework" : "dotnetcore");
var targetPlatform = Argument("targetPlatform", "windowsservercore-20H2");
var version = Argument("projectVersion", "1.1.0");
var dockerRegistry = Argument("dockerRegistry", "harbor.resaa.net/mci/");

// TODO: .Net Framework projects failed with Release configuration, so default configuration for .Net Framework is Debug,
// But please correct it after resolving the problem in the build using .Net Framework
var configuration = Argument("configuration", profile == "dotnetFramework" ? "Debug" : "Release");

const string ARTIFACTS_DIRECTORY = "artifacts/";
const string DEFAULT_ARTIFACTS_DIRECTORY = ARTIFACTS_DIRECTORY;
const string TEST_RESULTS_FILE = "TestResult.xml";

#if _WINDOWS_
DirectoryPath vsLatest  = IsRunningOnWindows() ? VSWhereLatest() : null;
FilePath msBuildPath = (vsLatest==null)
							? null
							: vsLatest.CombineWithFilePath(@"./MSBuild\Current\Bin\MSBuild.exe");
FilePath vsTestPath = (vsLatest==null)
							? null
							: vsLatest.CombineWithFilePath(@"./Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe");
#endif

class Project
{
	[Flags]
	public enum FrameworkType
	{
		DotNetFramework = 1 << 0,
		DotNetStandard2_0 = 1 << 1,
		DotNetStandard2_1 = 1 << 2,
		DotNetCore2_1 = 1 << 3,
		DotNetCore3_1 = 1 << 4,
		DotNet6 = 1 << 5
	}

	public Project(string name, string boundedContext = "", FrameworkType framework = FrameworkType.DotNetFramework, string artifactDirectory = DEFAULT_ARTIFACTS_DIRECTORY, string version = "1.0", string dockerImageName = "", string defaultScope = DEFAULT_SCOPE)
	: this(name, boundedContext, SOURCE_SOLUTION_DIRECTORY, framework, artifactDirectory, version, dockerImageName)
	{
	}

	public Project(string name, string boundedContext, string solutionDirectory, FrameworkType framework, string artifactDirectory, string version = "1.0", string dockerImageName = "", string defaultScope = DEFAULT_SCOPE)
	{
		Name = name;
		BoundedContext = boundedContext;
		SolutionDirectory = solutionDirectory;
		Framework = framework;
		Version = version;
		DockerImageName = dockerImageName;
		ArtifactDirectory = artifactDirectory;
		DefaultScope = defaultScope;
	}

	public string MSBuildFriendlyName
	{
		get
		{
			return (
				SolutionDirectory +
				IntermediateDirectory +
				BoundedContextSolutionDirectory +
				DefaultScope +
				BoundedContextScope +
				Name
				).Replace(".", "_");
		}
	}

	public string Name { get; }

	public string BoundedContext { get; }

	// TODO
	public virtual string Path
	{
		get
		{
			return ("../" +
				SolutionDirectory +
				BoundedContextSolutionDirectory +
				DefaultScope +
				BoundedContextScope +
				Name
				);
		}
	}

	// TODO
	public string ProjectFileName { get; }

	public string FileSystemFriendlyFullProjectName {
		get
		{
			return (IntermediateDirectory +
				BoundedContextSolutionDirectory +
				DefaultScope +
				BoundedContextScope +
				Name
				).Replace(".", "_");
		}
	}

	// TODO
	public string ArtifactDirectory { get; }

	// FIXME
	public string Version { get; }

	// FIXME
	public string DockerImageName { get; }

	public virtual string DllFilePath
	{
		get
		{
			// TODO: Fix it and use configuration argument variable
			return $"{Path}/bin/Debug/{FrameworkBuildDirectory}{DefaultScope}{BoundedContextScope}{Name}.dll".Replace("/", @"\");
		}
	}

	public virtual string DllName
	{
		get
		{
			return $"{DefaultScope}{BoundedContextScope}{Name}.dll";
		}
	}

	public string BoundedContextSolutionDirectory
	{
		get
		{
			return string.IsNullOrEmpty(BoundedContext) ? string.Empty : BoundedContext + @"\";
		}
	}

	public string BoundedContextScope
	{
		get
		{
			return string.IsNullOrEmpty(BoundedContext) ? string.Empty : BoundedContext + ".";
		}
	}

	public FrameworkType Framework
	{
		get;
	}

	public string DefaultScope { get; }

	protected string FrameworkBuildDirectory
	{
		get
		{
			switch (Framework)
			{
				case FrameworkType.DotNetFramework:
					return "";
				case FrameworkType.DotNetStandard2_0:
					return "netstandard2.0/";
				case FrameworkType.DotNetStandard2_1:
					return "netstandard2.1/";
				case FrameworkType.DotNetCore2_1:
					return "netcoreapp2.1/";
				case FrameworkType.DotNetCore3_1:
					return "netcoreapp3.1/";
				case FrameworkType.DotNet6:
					return "net6.0/";
				default:
					// TODO: Better throw exception
					return "";
			}
		}
	}

	protected string SolutionDirectory { get; }

	protected virtual string IntermediateDirectory { get; }

	public const string DEFAULT_SCOPE = "BSN.";

	private const string SOURCE_SOLUTION_DIRECTORY = @"Source\";
}

class TestProject : Project
{
	public enum Type
	{
		UnitTests,
		IntegrationTests
	}

	public TestProject(string name, Type typeOfTest, string boundedContext = "", FrameworkType framework = FrameworkType.DotNetFramework)
	: base(name, boundedContext, TEST_SOLUTION_DIRECTORY, framework, DEFAULT_ARTIFACTS_DIRECTORY)
	{
		TypeOfTest = typeOfTest;
	}

	public override string Path
	{
		get
		{
			return ("../" +
				SolutionDirectory +
				BoundedContextSolutionDirectory +
				IntermediateDirectory +
				DefaultScope +
				BoundedContextScope +
				Name
				);
		}
	}

	public Type TypeOfTest { get; }

	protected override string IntermediateDirectory
	{
		get
		{
			return TypeOfTest.ToString() + @"\";
		}
	}

	public const string TEST_SOLUTION_DIRECTORY = @"Test\";
}

var projects = new List<Project>();

projects = new List<Project>
{
	new Project(name: "IpTables.Api", framework: Project.FrameworkType.DotNet6, version: version),
};

var testProjects = new List<Project>
{
	new TestProject(name: "IpTables.Api.Tests", typeOfTest: TestProject.Type.UnitTests, framework: Project.FrameworkType.DotNet6),
};

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
	.WithCriteria(c => HasArgument("rebuild"))
	.Does(() =>
{
#if _WINDOWS_
	MSBuildSettings settings = new MSBuildSettings
	{
		ArgumentCustomization = args => args.Append("-ignoreProjectExtensions:.vdproj")
			.Append("-verbosity:normal"),
		ToolPath = msBuildPath
	};

	MSBuild("../BSN.IpTables.Api.sln", settings.WithTarget("clean"));
#elif _LINUX_
	var settings = new DotNetCleanSettings
	{
		Configuration = configuration
	};
	foreach(var project in projects)
	{
		if (project.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
			continue;
		DotNetClean(project.Path + project.ProjectFileName, settings);
	}
#endif

	// Remove forcely all contents in obj and bin directory for prevents errors in branch changing during build stage
	foreach(var project in projects)
	{
		CleanDirectory($"{project.Path}/bin");
		CleanDirectory($"{project.Path}/obj");
	}
});

Task("Restore")
	.Does(() =>
{
#if _WINDOWS_
	if(profile == "dotnetFramework")
	{
		MSBuildSettings settings = new MSBuildSettings
		{
			ArgumentCustomization = args => args.Append("-ignoreProjectExtensions:.vdproj")
				.Append("-verbosity:normal")
				.Append("-p:RestoreUseSkipNonexistentTargets=false")
				.Append("-p:RestorePackagesConfig=true"),
			ToolPath = msBuildPath
		};

		MSBuild("../BSN.IpTables.Api.sln", settings.WithTarget("restore"));

	}
#elif _LINUX_
	foreach(var project in projects)
	{
		if (project.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
			continue;
		Information("Project: " + project.Name + " Framework: " + project.Framework);
		DotNetRestore(project.Path + project.ProjectFileName);
	}
#endif
});

Task("Build")
	.IsDependentOn("Restore")
	.IsDependentOn("Clean")
	.Does(() =>
{
#if _WINDOWS_
	MSBuildSettings settings = new MSBuildSettings
	{
		Configuration = configuration,
		Verbosity = Verbosity.Normal,
		MSBuildPlatform = MSBuildPlatform.x86,
		PlatformTarget = PlatformTarget.MSIL,
		MaxCpuCount = 8,
		ToolPath = msBuildPath
	};

	foreach(Project project in projects)
	{
		if (project.DefaultScope.Contains("CallCenter"))
			continue;
		settings.WithTarget(project.MSBuildFriendlyName);
	}

	MSBuild("../BSN.IpTables.Api.sln", settings);
#elif _LINUX_
	foreach(var project in projects)
	{
		if (project.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
			continue;
		Information("Project: " + project.Name + " Framework: " + project.Framework);

		DotNetBuild(project.Path + project.ProjectFileName);
	}
#endif
});

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
{
	var coverletSettings = new CoverletSettings {
		CollectCoverage = true,
		CoverletOutputFormat = CoverletOutputFormat.opencover,
		CoverletOutputDirectory = Directory(@".\coverage-test\"),
		CoverletOutputName =  $"results-{DateTime.UtcNow:dd-MM-yyyy-HH-mm-ss-FFF}"
	};

#if _WINDOWS_
	MSBuildSettings msbuildSettings = new MSBuildSettings
	{
		Configuration = configuration,
		Verbosity = Verbosity.Normal,
		MSBuildPlatform = MSBuildPlatform.x86,
		PlatformTarget = PlatformTarget.MSIL,
		MaxCpuCount = 8,
		ToolPath = msBuildPath
	};

	foreach(Project project in testProjects)
	{
		msbuildSettings = msbuildSettings.WithTarget(project.MSBuildFriendlyName);
	}

	if (testProjects.Any())
		MSBuild("../BSN.IpTables.Api.sln", msbuildSettings);

	var testResultsFiles = new List<FilePath>();
	var vsTestResultsFiles = new List<FilePath>();
	foreach(Project testProject in testProjects)
	{
		string resultFile = ARTIFACTS_DIRECTORY + testProject.Name + ".xml";

		if (testProject.Framework == Project.FrameworkType.DotNetCore2_1)
		{
			var settings = new VSTestSettings
			{
				// TODO Use ResultsDirectory instead of using palin text (GH:#3127)
				ArgumentCustomization = args => args.Append($"/ResultsDirectory:{ARTIFACTS_DIRECTORY}"),
				SettingsFile = new FilePath("../DefaultVSTestSettings.runsettings"),
				HandleExitCode = _ => true,
				Logger = $"trx;LogFileName={testProject.Name}.xml",
				ToolPath = vsTestPath,
				// For example to filter test case, you can act like below
				// TestCaseFilter = "Name=GetChains_WithInvalidMsisdn_ShouldReturnBadRequest"
			};

			VSTest(testProject.DllFilePath, settings);
			vsTestResultsFiles.Add(new FilePath(resultFile));			
		}
		else
		{
			var settings = new NUnit3Settings
			{
				Configuration = configuration,
				HandleExitCode = _ => true,
				Results = new[]
				{
					new NUnit3Result
					{
						FileName = resultFile
					}
				}
			};

			foreach (var dir in GetSubDirectories("."))
				Information(dir.ToString());
			Information("Before run nunit3: ........... " + testProject.DllFilePath);
			NUnit3(testProject.DllFilePath, settings);

			testResultsFiles.Add(new FilePath(resultFile));			
			foreach (var f in testResultsFiles)
				Information(f);
		}
	}

	if (testProjects.Any() && AzurePipelines.IsRunningOnAzurePipelines)
	{
		Information("Publish test result.....................................");
		if (testResultsFiles.Any())
		{
			Information("testResultsFiles publish...");
			//testResultsFiles.Add(new FilePath("artifacts/Service.IntegrationTest.xml"));

			foreach (var f in testResultsFiles)
				Information(f);

			AzurePipelines.Commands.PublishTestResults(
				new AzurePipelinesPublishTestResultsData
				{
					TestResultsFiles = testResultsFiles.ToArray(),
					TestRunner = AzurePipelinesTestRunnerType.NUnit
				}
			);
		}
		if (vsTestResultsFiles.Any())
		{
			Information("vsTestResultsFiles publish...");
			AzurePipelines.Commands.PublishTestResults(
				new AzurePipelinesPublishTestResultsData
				{
					TestResultsFiles = vsTestResultsFiles.ToArray(),
					TestRunner = AzurePipelinesTestRunnerType.VSTest
				}
			);
		}
	}
#elif _LINUX_
	var testSettings = new DotNetTestSettings {
	};
	foreach(var testProject in testProjects)
	{
		if (testProject.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
			continue;
		Information("Project: " + testProject.Name + " Framework: " + testProject.Framework);
		DotNetTest(testProject.Path, testSettings, coverletSettings);
	}
#endif
});

Task("DBPublish")
	.Does(() =>
{
		if(profile == "dotnetFramework")
		{
#if _WINDOWS_
			// Generate Migration for Core Sub System
			// TODO: Make general approach
			// FIXME: As you can see in https://github.com/cake-contrib/Cake.EntityFramework6/issues/5#issue-1724849582
			// new EntityFramework6 does not support management on migration in efficient way,
			// so I have to disable these codes, until feature is available
//			Project dataProject = projects.Find(project => project.Name == "Data");
//			Project serviceProject = projects.Find(project => project.Name == "Service");
//			var migrationSettings = new EfMigratorSettings
//			{
//				AssemblyPath = dataProject.DllFilePath,
//				ConfigurationClass = "BSN.IpTables.Api.Core.Data.Migrations.Configuration",
//				AppConfigPath = serviceProject.Path + "/bin/app.publish/Web.config",
//				ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Resa.Accounting; Integrated Security=True; MultipleActiveResultSets=True;",
//				ConnectionProvider = "System.Data.SqlClient"
//				//ConnectionName = ""
//			};
//			using (var migrator = CreateEfMigrator(migrationSettings))
//			{
//				string migrationScript = migrator.GenerateScriptForLatest();
//				FileWriteText(ARTIFACTS_DIRECTORY + "migration.sql", migrationScript);
//
//				migrator.MigrateToLatest();
//				migrator.Commit();
//			}
#endif
		}
});

Task("Publish")
	//.IsDependentOn("Test")
	.Does(() =>
{
	foreach(var project in projects)
	{
		if (string.IsNullOrEmpty(project.DockerImageName))
			continue;
		if (project.DefaultScope.Contains("CallCenter"))
			continue;

		if(project.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
		{
#if _WINDOWS_
			// TODO: Replace with all publishable project
			if (project.Name == "Service" && project.BoundedContext == "Core")
			{
				MSBuildSettings msbuildSettings = new MSBuildSettings
				{
					Configuration = configuration,
					Verbosity = Verbosity.Normal,
					MSBuildPlatform = MSBuildPlatform.x86,
					PlatformTarget = PlatformTarget.MSIL,
					MaxCpuCount = 8,
					ToolPath = msBuildPath
				};

				msbuildSettings = msbuildSettings
												.WithTarget(project.MSBuildFriendlyName)
												.WithProperty("DeployOnBuild", "true")
												.WithProperty("PublishProfile", $"Resa Core Service Folder Test Profile");

				MSBuild("../BSN.IpTables.Api.sln", msbuildSettings);
			}
#endif
		}
		else
		{
				Information("Project: " + project.Name + " Framework: " + project.Framework);
				DotNetPublish(project.Path + project.ProjectFileName, new DotNetPublishSettings
				{
					Configuration = configuration,
					Verbosity = DotNetVerbosity.Normal,
					OutputDirectory = project.ArtifactDirectory + @"/" + project.FileSystemFriendlyFullProjectName
				});
		}
#if _LINUX_


		if (project.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
			continue;
		Information("Project: " + project.Name + " Framework: " + project.Framework);

		var dockerImageBuildSettings = new DockerImageBuildSettings
		{
			File = $"{project.Path}/Dockerfile",
			// File = $"{project.Path.Replace("../", "")}/Dockerfile",
			Target = "final",
			BuildArg = new[]
			{
				// $"source={MakeAbsolute(Directory(project.Path)).FullPath}"
				// $"source={project.Path.Replace("../", "")}",
				$"target_os_name={targetPlatform}"
				// $"target_os_name=windowsservercore-ltsc2019"
			},
			Tag = new[]
			{
				project.DockerImageName + $":" + project.Version, 
				project.DockerImageName + $":" + "latest"
			},
			Compress = true,
			NoCache = false
		};

		DockerBuild(dockerImageBuildSettings, "../");
#endif
		// DockerBuild(dockerImageBuildSettings, "../");
	}
});

Task("PushDockerImagesToRepository").Does(() =>
{
	foreach(var project in projects)
	{
		if (string.IsNullOrEmpty(project.DockerImageName))
			continue;
		if (project.DefaultScope.Contains("CallCenter"))
			continue;
		if(project.Framework.HasFlag(Project.FrameworkType.DotNetFramework))
			continue;
		DockerTag(project.DockerImageName + $":" + project.Version, dockerRegistry + project.DockerImageName + $":" + project.Version);
		DockerPush(dockerRegistry + project.DockerImageName + $":" + project.Version);
	}
});

Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Test")
	.IsDependentOn("Publish");

Task("JustBuild")
	.IsDependentOn("Clean")
	.IsDependentOn("Build");
	
Task("JustTest")
	.IsDependentOn("Clean")
	.IsDependentOn("Test");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
