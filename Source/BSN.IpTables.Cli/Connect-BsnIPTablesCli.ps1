function Connect-BsnIPTablesCli {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true, HelpMessage = 'Please enter the target server address')]
        [string]$ServerAddress
    )

    process {
        Write-Output "Connected To: $ServerAddress"
        # Perform other actions related to IPTables or any other tasks here
    }

    end {
        # You can add cleanup or finalization steps here if needed
    }
}

# Call the function with the entered server address
Connect-BsnIPTablesCli -ServerAddress $enteredServerAddress
