function Connect-BsnIPTablesCli {
    [CmdletBinding()]
    param()

    begin {
        # Prompt the user to enter the server address
        $ServerAddress = Read-Host "Enter the target server address"

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
