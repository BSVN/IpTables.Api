# Solution Test Structures

All projects must have unit test project equivalent in UnitTests directory.

All test must have Liquid reports

## LiquidTestReports.Markdown

[![NuGet Badge](https://buildstats.info/nuget/LiquidTestReports.Markdown?includePreReleases=false)](https://www.nuget.org/packages/LiquidTestReports.Markdown) 

The Markdown logger package is a ready to use  implementation of the test logger that generates Markdown format reports. 

[Sample Report](docs/samples/xUnit.md)

**How to use**:

1. Install the markdown logger to your test project by running the following command
 `dotnet add package LiquidTestReports.Markdown`

2. Run the tests using the supplied logger
 `dotnet test --logger "liquid.md"`

3. Report will be generated in the test results folder

See also: [Testing .NET Core Apps with GitHub Actions](https://dev.to/kurtmkurtm/testing-net-core-apps-with-github-actions-3i76)
