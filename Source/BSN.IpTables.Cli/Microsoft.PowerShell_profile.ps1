function Set-ServerAddress {
    param (
        [string]$ServerAddress
    )

    # Store the server address in a session variable
    $global:APISession = @{
        ServerAddress = $ServerAddress
    }

    # Function to update the server address
    function Update-ServerAddress($newServerAddress) {
        $global:APISession.ServerAddress = $newServerAddress
    }

    # Display a message indicating the current server address
    Write-Host "Server address set to $($global:APISession.ServerAddress)"

    # Return the session variable
    return $global:APISession
}

function Get-IpTablesWrapper {
    # Set the server address if not already set
    if (-not $global:APISession) {
        Set-ServerAddress -ServerAddress '192.168.21.56:8080'
    }

    # Now, call the actual cmdlet
    Get-BsnIPTablesCli -ServerAddress $global:APISession.ServerAddress
}
