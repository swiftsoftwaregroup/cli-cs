#!/usr/bin/env bash

export DOTNET_ROOT=$HOME/.dotnet
export PATH="$HOME/.dotnet:$PATH"

export DOTNET_CLI_TELEMETRY_OPTOUT=1

nuget_source='https://api.nuget.org/v3/index.json'
if [ -z $(dotnet nuget list source | grep $nuget_source) ]; then
    dotnet nuget add source "$nuget_source" --name "nuget.org"
fi

dotnet restore
