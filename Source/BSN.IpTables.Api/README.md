# IpTables Api

## Overview

IpTables Api is a REST API, which enables remote running of iptables utility commands, in order to configure the IP Packet filter rules of the Linux kernel firewall.

The provided API is based on `iptables` commands. Currently available commands for a rule are:

- list
- append
- insert
- delete

A rule is consists of:
- chain
- interface name
- protocol
- source IP
- destination IP
- target

## Build

The project could be built by 'sbuild.bat' script as well as Visual Studio build system.

## Publish

'make-docker.ps1' script creates docker images in Windows. 'make-docker.sh' do this in Linux distributions.

## Run

IpTables Api supports Linux as host OS. The project has docker compose to facilitate deploy and running it.

## Test

The Project has swagger in `/swagger` path. It could be used to test existing APIs.
