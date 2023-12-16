# Read the server address from your file
$serverAddress = Get-Content "D:\Iptables\IpTables.Api\Source\BSN.IpTables.Cli\serverAddress.txt"

# Load the Swagger JSON file
$swaggerPath = "path\to\your\swagger.json"
$swagger = Get-Content $swaggerPath | ConvertFrom-Json

# Replace the default serverAddress in Swagger JSON
$swagger.servers[0].variables.serverAddress.default = $serverAddress

# Save the modified Swagger JSON
$swagger | ConvertTo-Json | Set-Content $swaggerPath
