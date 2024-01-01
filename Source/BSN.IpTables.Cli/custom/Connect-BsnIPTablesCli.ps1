function Connect-BsnIPTablesCli {
    param(
        [Parameter(Mandatory)]
        [BSN.IpTables.V1.Category('Uri')]
        [System.String]
        # Target Server Address
        ${ServerAddress}
    )

    begin {
        Write-Output "Connect begin"
    }

    process {
        Write-Output "Connect process, server address: $ServerAddress"

        # FIXME: Save server address for future use
    }

    end {
        Write-Output "Connect end"
    } 
}