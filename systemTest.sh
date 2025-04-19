#!/bin/bash
set -e

cwd=$(pwd)

# delete the existing system test database if it exists 
if [ -f "$cwd/systemTest.db" ]; then
    echo "Deleting $cwd/systemTest.db from previous run"
    rm "$cwd/systemTest.db"
fi

# delete old log files 
if [ -f "$cwd/PTS.Api/Logs/ptsapi.log" ]; then
    echo "Deleting $cwd/PTS.Api/Logs/ptsapi.log from previous run"
    rm "$cwd/PTS.Api/Logs/ptsapi.log"
fi

# run the insaller to create a database 
(dotnet run --project PTS.Installer/PTS.Installer.csproj -- "Data Source=$cwd/systemTest.db" )> ptsapi.log 2>&1 &
sleep 3 

# start up PTS.Api
dotnet run --project PTS.Api/PTS.Api.csproj -- "Data Source=$cwd/systemTest.db" &
PID=$!
echo "started with PID $PID"

# wait for start up to complete 
sleep 3
echo "Starting test run" 
# run the PTS.Api system tests 
dotnet test PTS.Api.SystemTests/PTS.Api.SystemTests.csproj
# shut down PTS.Api
echo "Killing PID $PID" 
kill $PID