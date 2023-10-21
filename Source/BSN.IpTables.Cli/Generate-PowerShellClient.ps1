# ITNOA

# TODO: Make sure nodejs install correctlly
# nvm list available
# nvm install 18.16.1
# nvm use 18.16.1

npm install -g "autorest@3.6.3"

autorest configuration.yaml --verbose

# Build Module
Write-Host $PSScriptRoot

# FIXME: Check to run on powershell core
# FIXME: Comment blocks which have 'WindowsAzure' in "generated\exports\ProxyCmdletDefinitions.ps1"
& $PSScriptRoot\generated\build-module.ps1

Write-Host "Run with $PSScriptRoot\generated\run-module.ps1"
