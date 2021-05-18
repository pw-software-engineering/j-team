#!/bin/bash
docker run --expose=80 -p:80:80 \
-e ASPNETCORE_Kestrel__Certificates__Default__Password="password" \
-e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx \
-v ${HOME}/.aspnet/https:/https/  \
-t hotel-system/server:latest
