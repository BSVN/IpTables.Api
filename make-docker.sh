﻿#!/bin/bash

# ITNOA

#-- https://github.com/BSVN/IpTables.Api
echo ""
echo ""
echo ""
echo ""
echo ""
echo ""
echo ""
echo " ██████╗░███████╗░██████╗░█████╗░"
echo " █╔══███╗██╔════╝██╔════╝██╔══██╗"
echo " ██████╔╝█████╗░░╚█████╗░███████║"
echo " ██╔══██╗██╔══╝░░░╚═══██╗██╔══██║"
echo " ██║░░██║███████╗██████╔╝██║░░██║"
echo " ╚═╝░░╚═╝╚══════╝╚═════╝░╚═╝░░╚═╝"
echo ""
echo ""
echo ""
echo " need support? https://resaa.net"
echo ""
echo " Copyright (C) 2023	The Resa, Co. (resaa.net)"
echo ""
echo " MIT License"
echo ""
echo " Copyright (c) 2023 Green and Silver Leaves, Co."
echo ""
echo " Permission is hereby granted, free of charge, to any person obtaining a copy"
echo " of this software and associated documentation files (the "BSN.IpTables.Api"), to deal"
echo " in the Software without restriction, including without limitation the rights"
echo " to use, copy, modify, merge, publish, distribute, sublicense, and/or sell"
echo " copies of the Software, and to permit persons to whom the Software is"
echo " furnished to do so, subject to the following conditions:"
echo ""
echo " The above copyright notice and this permission notice shall be included in all"
echo " copies or substantial portions of the Software."
echo "" 
echo " THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR"
echo " IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,"
echo " FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE"
echo " AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER"
echo " LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,"
echo " OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE"
echo " SOFTWARE."
echo ""

readonly VERSION="1.2.0"

docker build . -f Source/BSN.IpTables.Api/Dockerfile -t resaa.ir/iptables-api:${VERSION} -t resaa.ir/iptables-api:latest
docker image save resaa.ir/iptables-api:${VERSION} -o bsn-iptales-api-image-${VERSION}.docker