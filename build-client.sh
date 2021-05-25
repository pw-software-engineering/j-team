#!/bin/bash
docker build --pull --rm -f "src/WebUI/ClientApp/Dockerfile" -t hotel-system/client "src/WebUI/ClientApp"