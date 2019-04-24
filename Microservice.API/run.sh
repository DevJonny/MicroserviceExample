#!/usr/bin/env bash

dotnet publish -c Release
docker build -t microservice.api .
docker run \
    -it \
    --rm \
    -p 5001:80 \
    -e ASPNETCORE_ENVIRONMENT=development \
    microservice.api:latest