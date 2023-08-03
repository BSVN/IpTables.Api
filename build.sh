#!/usr/bin/env bash
set -euox pipefail

# For bootstrapping build process in linux (Ubuntu, CentOS)

cd "$(dirname "${BASH_SOURCE[0]}")"

export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_NOLOGO=1

# https://askubuntu.com/a/1214268/101335
readonly os_name=$(cat /etc/os-release | awk -F '=' '/^NAME/{print $2}' | awk '{print $1}' | tr -d '"')

# Check dotnet exist or not?
# https://stackoverflow.com/a/677212/1539100
if ! command -v dotnet &> /dev/null ; then

	# https://askubuntu.com/a/1214268/101335
	readonly os_name=$(cat /etc/os-release | awk -F '=' '/^NAME/{print $2}' | awk '{print $1}' | tr -d '"')
	if [ "$os_name" == "Ubuntu" ]
	then
		echo "system is Ubuntu"

		# Update the apt package index and install packages needed to use the .NET SDK apt repository:
		sudo apt-get update
		sudo apt-get install -y apt-transport-https ca-certificates curl

		sudo apt-get update
		sudo apt-get install -y dotnet-sdk-6.0

	elif [ "$os_name" == "CentOS" ]
	then
		echo "system is CentOS"
		sudo dnf install dotnet-sdk-6.0
	else
		echo "system is $os_name"
	fi
fi

# Check docker-compose exist or not?
# https://stackoverflow.com/a/677212/1539100
if ! command -v docker-compose &> /dev/null ; then
	if [ "$os_name" == "Ubuntu" ]
	then
		echo "system is Ubuntu"

		sudo apt-get update
		sudo apt-get install -y docker-compose

	elif [ "$os_name" == "CentOS" ]
	then
		echo "system is CentOS"
		sudo dnf install docker-compose
	else
		echo "system is $os_name"
	fi
fi

dotnet new tool-manifest --force
dotnet tool install Cake.Tool

dotnet tool restore

dotnet --info

cd Build/ && dotnet cake linux-build.cake "$@" && cd ..

if [ -f 'build/build-call-control-system.cake' ]
then
	cd build/ && dotnet cake build-call-control-system.cake "$@"
fi
