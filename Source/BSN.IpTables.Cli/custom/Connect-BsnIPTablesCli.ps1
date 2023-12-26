function Connect-BsnIPTablesCli {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true)]
        [string]$ServerAddress
    )

    begin {
        # Save the ServerAddress in a session variable
        $env:ServerAddress = $ServerAddress
    }

    process {
        Write-Output "Connected To : $ServerAddress"
    }

    end {
    }
}

# Call the function to connect and save the server address
Connect-BsnIPTablesCli
