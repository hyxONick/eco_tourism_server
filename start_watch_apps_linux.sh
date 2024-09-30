#!/bin/bash

APP1_PATH="./eco_tourism_accommodation"
APP2_PATH="./eco_tourism_gateway"
APP3_PATH="./eco_tourism_outdoor"
APP4_PATH="./eco_tourism_tourist"
APP5_PATH="./eco_tourism_user"
APP6_PATH="./eco_tourism_weather"

echo "Starting app1 with dotnet watch..."
dotnet watch --project "$APP1_PATH" &

echo "Starting app2 with dotnet watch..."
dotnet watch --project "$APP2_PATH" &

echo "Starting app3 with dotnet watch..."
dotnet watch --project "$APP3_PATH" &

echo "Starting app4 with dotnet watch..."
dotnet watch --project "$APP4_PATH" &

echo "Starting app5 with dotnet watch..."
dotnet watch --project "$APP5_PATH" &

echo "Starting app6 with dotnet watch..."
dotnet watch --project "$APP6_PATH" &

echo "All apps started with dotnet watch."

wait
