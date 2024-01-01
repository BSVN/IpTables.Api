<#
.SYNOPSIS
This cmdlet establishes a connection to the BSN IP Tables with the specified server address.

.DESCRIPTION
The Connect-BsnIPTablesCli cmdlet connects to the BSN IP Tables using the provided server address. 
It is a mandatory parameter, and the connection is established in the begin block.

.PARAMETER ServerAddress
Specifies the target server address for the connection. This is a mandatory parameter.

.EXAMPLE
Connect-BsnIPTablesCli -ServerAddress "http://example.com"
Establishes a connection to the BSN IP Tables with the server address "http://example.com".

.NOTES
File Name      : Connect-BsnIPTablesCli.ps1
Prerequisite   : PowerShell V5
Copyright 2019 - The BSN Team
#>

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
        # Check if $ServerAddress is null
        if ($null -eq $ServerAddress) {
            Write-Error "ServerAddress is mandatory. Please provide a valid value."
            return
        }
        # Save the ServerAddress in a session variable
        $env:ServerAddress = $ServerAddress
    }

    process {
        Write-Output "Connected to: $ServerAddress"
    }

    end {
    }
}
