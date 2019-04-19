#!/usr/bin/env bash

dotnet build Microservice.EventConsumer.csproj
dotnet ./bin/Debug/netcoreapp2.2/Microservice.EventConsumer.dll
