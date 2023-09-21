# ITNOA

# TODO: Make sure nodejs install correctlly
# nvm list available
# nvm install 18.16.1
# nvm use 18.16.1

npm install -g "autorest"
autorest configuration.yaml

# Build Module
Write-Host $PSScriptRoot
& $PSScriptRoot\generated\build-module.ps1