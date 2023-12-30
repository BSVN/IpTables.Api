# ITNOA

# This script installs required packages to generate and build BSN Iptable command-line
# Requirements:
#     Powershell Core
#     nvm (Node Version Manager)
#     Internet (to install packages)

$ErrorActionPreference = 'Stop'

if ($PSEdition -ne 'Core') {
    Write-Error 'This script requires PowerShell Core to execute. [Note] Generated cmdlets will work in both PowerShell Core or Windows PowerShell.'
}

try {
    Write-Host "Nvm is installed, version" $(nvm --version)
}
catch {
    Write-Error "Nvm is not installed, install it manually to continue."
}

# 18.18.0 is the latest LTS node version
New-Variable -Name desiredNodeVersion -Value 'v18.18.0' -Option ReadOnly
if ($(nvm current) -ne $desiredNodeVersion) {
    nvm install $desiredNodeVersion
    nvm use $desiredNodeVersion
}
else {
    Write-Host "Node is installed, version" $desiredNodeVersion
}

try {
    (autorest --version).Split([Environment]::NewLine) | Select -First 1
}
catch {
    Write-Host "autorest is not installed, installing .." -ForegroundColor Yellow
    npm install -g "autorest@3.6.3"
}

# Below command should be used if Autorest is not clean:
# autorest --reset

Write-Host "Generating Cli .." -ForegroundColor Green
autorest configuration.yaml --verbose

# Copy custom files
Write-Host "Copy custom files .." -ForegroundColor Green
cp custom/* generated/custom

# Build Module
Write-Host "Building generating Cli .." -ForegroundColor Green
.\generated\build-module.ps1

Write-Host "Generating Cli completed successfully .." -ForegroundColor Green