cwd=$(pwd)

# delete the existing system test database if it exists 
if [ -f $cwd/systemTest.db ]; then
    echo "Deleting $cwd/systemTest.db from previous run"
    rm $cwd/systemTest.db
fi

# run the insaller to create a database 
dotnet run --project PTS.Installer/PTS.Installer.csproj -- "Data Source=$cwd/systemTest.db" &
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