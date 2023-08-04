# IpTables.Api
API for local iptables management

IpTables.Api is the RESTful server which applies iptables rules to block or allow IP addresses on request from a remote client.
IpTables.Api maintains the list of blocked IP addresses which may be updated on the fly from many sources.
IpTables.Api also solves the problem of concurrent modifications to iptables since the requests are serialized.

This project powered on [IPTables.Net](https://github.com/splitice/IPTables.Net) for interacting with Linux IPTables
