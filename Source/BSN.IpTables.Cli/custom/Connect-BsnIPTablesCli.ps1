# Function to connect to BsnIPTablesCli and save serverAddress to file
function Connect-BsnIPTablesCli {
    param(
        [Parameter(Mandatory)]
        [string]
        # Target Server Address
        $ServerAddress
    )

    begin {
        Write-Output "Connect begin"
    }

    process {
        Write-Output "Connect process, server address: $ServerAddress"

        # Save the server address to a text file
        Save-ServerAddressToFile $ServerAddress
    }

    end {
        Write-Output "Connect end"
    } 
}

# Function to save the server address to a text file
function Save-ServerAddressToFile {
    param (
        [string]$ServerAddress
    )

    $filePath = "D:\Iptables\IpTables.Api\Source\BSN.IpTables.Cli\serverAddress.txt"

    try {
        # Save server address to file
        $ServerAddress | Out-File -FilePath $filePath -Force -Encoding UTF8

        Write-Output "Server address saved to $filePath"
    }
    catch {
        Write-Error "Error saving server address to file: $_"
    }
}
