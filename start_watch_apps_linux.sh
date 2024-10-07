#!/bin/bash

APP1_PATH="./eco_tourism_accommodation"
APP2_PATH="./eco_tourism_gateway"
APP3_PATH="./eco_tourism_outdoor"
APP4_PATH="./eco_tourism_tourist"
APP5_PATH="./eco_tourism_user"
APP6_PATH="./eco_tourism_weather"

# Function to kill all running dotnet watch processes
cleanup() {
    echo "Killing all dotnet watch processes..."
    kill $APP1_PID $APP2_PID $APP3_PID $APP4_PID $APP5_PID $APP6_PID
    echo "All processes killed."
}

# Trap SIGINT (Ctrl+C) and call cleanup function
trap cleanup SIGINT

echo "Starting app1 with dotnet watch..."
dotnet watch --project "$APP1_PATH" &
APP1_PID=$!

echo "Starting app2 with dotnet watch..."
dotnet watch --project "$APP2_PATH" &
APP2_PID=$!

echo "Starting app3 with dotnet watch..."
dotnet watch --project "$APP3_PATH" &
APP3_PID=$!

echo "Starting app4 with dotnet watch..."
dotnet watch --project "$APP4_PATH" &
APP4_PID=$!

echo "Starting app5 with dotnet watch..."
dotnet watch --project "$APP5_PATH" &
APP5_PID=$!

echo "Starting app6 with dotnet watch..."
dotnet watch --project "$APP6_PATH" &
APP6_PID=$!

echo "All apps started with dotnet watch."

wait
