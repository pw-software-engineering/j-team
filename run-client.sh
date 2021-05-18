#!/bin/bash
docker run --expose=80 -p:80:80 \
-t hotel-system/client:latest
