#!/usr/bin/env bash

dotnet publish -c Release
docker build -t microservice.datastore .
docker run -it --rm -p 5001:80 microservice.datastore:latest