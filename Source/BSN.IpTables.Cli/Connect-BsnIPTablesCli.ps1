function Connect-BsnIPTablesCli {
    param(
        [Parameter(Mandatory)]
        [BSN.IpTables.V1.Category('Uri')]
        [System.String]
        # Target Server Address
        ${ServerAddress}
    )

    begin {
        Write-Output "Mehran begin"
    }

    process {
        Write-Output "Mehran process, server address: $ServerAddress"

        # Save the server address in the session
        $global:SessionServerAddress = $ServerAddress

        # Call the UpdateSwaggerJson function in Module.cs
        Update-SwaggerJson $ServerAddress
    }

    end {
        Write-Output "Mehran end"
    } 
}

# Function to update Swagger JSON
function Update-SwaggerJson {
    param (
        [string]$ServerAddress
    )

    $modulePath = "D:\Iptables\IpTables.Api\Source\BSN.IpTables.Cli\generated\custom\Module.cs"

    # Invoke the UpdateSwaggerJson function in Module.cs
    Add-Type -Path $modulePath
    [Module]::BsnIPTablesCli::UpdateSwaggerJson($ServerAddress)
}
