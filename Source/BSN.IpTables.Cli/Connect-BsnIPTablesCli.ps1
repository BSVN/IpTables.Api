function Connect-BsnIPTablesCli {
    [CmdletBinding()]
    param()

    begin {
        # Prompt the user to enter the server address
        $ServerAddress = Read-Host "Enter the target server address"

        # Save the ServerAddress in a session variable
        $script:Global:SessionServerAddress = $ServerAddress

        # Write the server address to a file
        $ServerAddress | Out-File -FilePath "D:\Iptables\IpTables.Api\Source\BSN.IpTables.Cli\serverAddress.txt"

        Write-Output "Connect begin"
    }

    process {
        Write-Output "Connect process, server address: $ServerAddress"
    }

    end {
        Write-Output "Connect end"
    }
}

# Call the function to connect and save the server address
Connect-BsnIPTablesCli
