#!/bin/bash
#docker build -f src/WebUI/HotelApp -t hotel-system/hotel .
docker build --pull --rm -f "src/WebUI/HotelApp/Dockerfile" -t hotel-system/hotel "src/WebUI/HotelApp"