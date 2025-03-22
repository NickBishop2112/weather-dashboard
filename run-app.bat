@echo off

echo Build and test the API
dotnet build .\api
dotnet test .\api\WeatherDasbboard.API.Tests\WeatherDashboard.API.Tests.csproj

echo Starting API in the background...
start /B dotnet run --project .\api\WeatherDashboard.API\WeatherDashboard.API.csproj > output.log 2>&1
start http://localhost:5002/swagger/index.html

echo Build the databoard
cd .\dashboard\
npm install --no-audit && npm run dev
cd ..

echo Both backend and frontend are running.
pause
