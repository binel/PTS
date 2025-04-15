# delete the existing system test database if it exists 
if [ -f PTS.Api/systemTest.db ]; then
    echo "Deleting systemTest.db from previous run"
    rm PTS.Api/systemTest.db
fi
# start up PTS.Api
dotnet run --project PTS.Api/PTS.Api.csproj -- "Data Source=systemTest.db" &
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