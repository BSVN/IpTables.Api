<!-- region Generated -->
# BSN.IpTables.Cli

This directory contains the PowerShell module for the BSN.IpTables.Api service.

---

## Status

[![BSN.IpTables.Cli](https://img.shields.io/powershellgallery/v/BSN.IpTables.Cli.svg?style=flat-square&label=BSN.IpTables.Cli "BSN.IpTables.Cli")](https://www.powershellgallery.com/packages/BSN.IpTables.Cli/)

## Info

- Modifiable: yes
- Generated: all
- Committed: yes
- Packaged: yes

---

## Detail

This module was primarily generated via [AutoRest](https://github.com/Azure/autorest) using the [PowerShell](https://github.com/Azure/autorest.powershell) extension.

## Module Requirements

- NVM (nvm install 8.0) - You need to install Node.js v10.13.0 or greater.
- autorest in linux - npm install -g autorest
- dotnet sdk if not exists - sudo snap install dotnet-sdk

---

### AutoRest Configuration

> see <https://aka.ms/autorest>

## Cli Generation Details

The Cli is generated based on `IpTables.Api` swagger file. The swagger file is generated as a post-process in IpTables.Api build process. It can be created manulally by running (in IpTables.Api directory):

    dotnet tool restore
    dotnet swagger tofile --output swagger.json bin\Debug\net6.0\BSN.IpTables.Api.dll v1

Autorest is used to generate Powershell Cli from swagger file. `configuration.yaml` is Autorest config file. After generating Cli

## Generated Commands Info

All Cli commands are encapsulated in `BsnIPTablesCli` module. To show all commands run:

    Get-Command -Module BsnIPTablesCli

Sample output:

    CommandType     Name                                               Version    Source
    -----------     ----    
    Function        Connect-BsnIPTablesCli                             1.2.0      BsnIPTablesCli
    Function        Add-BsnIPTablesCli                                 1.2.0      BsnIPTablesCli
    Function        Get-BsnIPTablesCli                                 1.2.0      BsnIPTablesCli
    Function        Remove-BsnIPTablesCli                              1.2.0      BsnIPTablesCli

To see a command input parameters run:

    Get-Command -Name Add-BsnIPTablesCli -Args Cert: -Syntax

Sample output:

    Add-BsnIPTablesCli [-Chain <string>] [-RuleDestinationIP <string>] [-RuleDestinationPort <string>] [-RuleInterfaceName <string>] [-RuleJump <string>] [-RuleProtocol <string>] [-RuleSourceIP <string>] [-RuleSourcePort <string>] [-Break] [-HttpPipelineAppend <SendAsyncStep[]>] [-HttpPipelinePrepend <SendAsyncStep[]>] [-Proxy <uri>] [-ProxyCredential <pscredential>] [-ProxyUseDefaultCredentials] [-WhatIf] [-Confirm] [<CommonParameters>]


To see full help for a command run:

    Get-Help Add-BsnIPTablesCli -Full

## Sample Commands in Windows
First run .\Generate-PowerShellClient.ps1 in cli directory

Then .\generated\run-module.ps1

1. Connect to main server.

    Connect-BsnIPTablesCli -serverAddress 192.168.21.56:8080

2. List all existing IpTable rules.

    Get-BsnIPTablesCli 

Sample output:

    "name": "INPUT",
    "tableName": "filter",
    "ipVersion": 4,
    "rules": [
      {
        "interfaceName": "",
        "protocol": "tcp",
        "sourceIp": "1.2.3.4",
        "destinationIp": "",
        "target": "DROP"
      }
    ]

Which means only one rule exists. The rule casues to drop incoming tcp packets from `1.2.3.4` IPv4 address.

3. Drop all incoming ICMP packets from any source, on all interfaces:

    Add-BsnIPTablesCli -Chain INPUT -RuleJump DROP -RuleProtocol icmp

4. Remove the previous rule:

    Remove-BsnIPTablesCli -Chain INPUT -RuleJump DROP -RuleProtocol icmp
 
## Verification

Each CLI command is equivalent to an `iptables` command. Valid execution of CLI commands could be verified by checking existing rules in the destination server.
Another way to verify a successful operation is to check rule enforcement in a traffic flow. Below are some scenarios to test IpTables by these two methods.

Scenario 1: Connect to server, then add a rule with `iptables`, then list existing rules with CLI and check its existence.

First flush rules:

    iptables -t filter -F

Then add a rule to drop tcp packets from specific IP adddress and port:

    iptables -A INPUT -p tcp -s 1.2.3.4 --dport 80 -j DROP

List rules with CLI:

    Get-BsnIPTablesCli

Check and find added rule in output:

    "name": "INPUT",
      "tableName": "filter",
      "ipVersion": 4,
      "rules": [
        {
          "interfaceName": "",
          "protocol": "tcp",
          "sourceIp": "1.2.3.4",
          "destinationIp": "",
          "target": "DROP"
        }

Scenario 2: Connect to server, then add a rule with CLI, then list existing rules with `iptables` and check its existence:

Add a rule to drop tcp packets to specific IP adddress range on specific interface:

    Add-BsnIPTablesCli -Chain OUTPUT -RuleInterfaceName ens160 -RuleProtocol tcp -RuleDestinationIP 69.171.224.0/19 -RuleJump DROP

List output rules with `iptables`:

    iptables -L OUTPUT -n --line-numbers

Check and find added rule in output:

    Chain OUTPUT (policy ACCEPT)
    num  target     prot opt source               destination
    1    DROP       tcp  --  0.0.0.0/0            69.171.224.0/19

Scenario 3: Connect to server, then add a rule with CLI, then check its effect in traffic:

Check ping to the server:

    ping 192.168.21.56

Add a rule to drop incoming icmp packets:

    Add-BsnIPTablesCli -Chain INPUT -RuleJump DROP -RuleProtocol icmp

Check ping to the server, it should not be available:

    ping 192.168.21.56

## Sample Commands in Linux
First - autorest ./configuration.yaml in cli directory 

Then - pwsh Generate-PowerShellClient.ps1

Finally - pwsh ./generated\run-module.ps1

Next steps are the same as windows
