function Connect-BsnIPTablesCli {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory)]
        [BSN.IpTables.V1.Category('Uri')]
        [System.String]
        # Target Server Address
        ${ServerAddress}
    )

    begin {
        # Save the ServerAddress in a session variable
        $env:ServerAddress = $ServerAddress
    }

    process {
        Write-Output "Connected to: $ServerAddress"
    }

    end {
    }
}
