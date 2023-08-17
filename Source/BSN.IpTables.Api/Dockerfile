#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG version=0.1.0_ia0.2.0

LABEL org.opencontainers.image.description "This image is for BSN iptables API"
LABEL org.opencontainers.image.url "https://github.com/BSVN/iptables-api"
LABEL org.opencontainers.image.version ${version}
LABEL org.opencontainers.image.licenses GPL-3.0
LABEL org.opencontainers.image.title "BSN iptables API"
LABEL org.opencontainers.image.authors "s.hosseinalipour@resaa.net"
LABEL ir.resaa.version ${version}
LABEL ir.resaa.release-date 2023-08-02
LABEL ir.resaa.version.is-production true
LABEL ir.resaa.vendor "Resaa Co."
LABEL website="https://www.resaa.ir"

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/BSN.IpTables.Api/BSN.IpTables.Api.csproj", "Source/BSN.IpTables.Api/"]
RUN dotnet restore "Source/BSN.IpTables.Api/BSN.IpTables.Api.csproj"
COPY . .
WORKDIR "/src/Source/BSN.IpTables.Api"
RUN dotnet build "BSN.IpTables.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BSN.IpTables.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN apt update && apt install -y --no-install-recommends iptables
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BSN.IpTables.Api.dll"]