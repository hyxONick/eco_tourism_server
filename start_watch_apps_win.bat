@echo off

SET APP1_PATH=.\eco_tourism_accommodation
SET APP2_PATH=.\eco_tourism_gateway
SET APP3_PATH=.\eco_tourism_outdoor
SET APP4_PATH=.\eco_tourism_tourist
SET APP5_PATH=.\eco_tourism_user
SET APP6_PATH=.\eco_tourism_weather

echo Starting app1 with dotnet watch...
start cmd /k "cd /d %APP1_PATH% && dotnet watch"

echo Starting app2 with dotnet watch...
start cmd /k "cd /d %APP2_PATH% && dotnet watch"

echo Starting app3 with dotnet watch...
start cmd /k "cd /d %APP3_PATH% && dotnet watch"

echo Starting app4 with dotnet watch...
start cmd /k "cd /d %APP4_PATH% && dotnet watch"

echo Starting app5 with dotnet watch...
start cmd /k "cd /d %APP5_PATH% && dotnet watch"

echo Starting app6 with dotnet watch...
start cmd /k "cd /d %APP6_PATH% && dotnet watch"

echo All apps started with dotnet watch.
