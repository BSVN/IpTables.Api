Set-Variable -Name VERSION -Value "1.2.0" -Description "Docker image version" -Option readonly

docker build . -f Source/BSN.IpTables.Api/Dockerfile -t resaa.ir/iptables-api:${VERSION} -t resaa.ir/iptables-api:latest
docker image save resaa.ir/iptables-api:${VERSION} -o bsn-iptales-api-image-${VERSION}.docker
Write-Host "scp ./bsn-iptales-api-image-${VERSION}.docker root@192.168.21.56:~/"