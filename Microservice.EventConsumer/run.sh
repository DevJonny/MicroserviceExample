#!/usr/bin/env bash

dotnet publish -c Release
docker build -t microservice.eventconsumer .
docker run -it --rm -p 5001:80 microservice.eventconsumer:latest