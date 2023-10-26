Set-Variable -Name VERSION -Value "0.1.0" -Description "Docker image version" -Option readonly

docker build . -f Source/BSN.IpTables.Api/Dockerfile -t resaa.ir/iptables-api:${VERSION} -t resaa.ir/iptables-api:latest
docker image save resaa.ir/iptables-api:${VERSION} -o bsn-iptales-api-image-${VERSION}.docker