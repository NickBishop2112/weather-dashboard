# Weather Dashboard

## Overview
The Weather Forecast Dashboard contains the following features:
- Weather API (`http://localhost:5002/swagger/index.html`)
- Dashboard (`http://localhost:3000/`)

## Prerequistes

- .Net SDK 8 is installed
- Rider or Visual Studio is installed if needed
- NodeJs is installed

## Step 1 - clone the Git repo
```
git clone https://github.com/NickBishop2112/weather-dashboard
```
## Step 2 - Change the Open Weather API
If required change the `ApiKey` located in the OpenWeatherMap section of the `app\WeatherDashboard.API\appsettings.json`.

## Step 3 - Run the app
The instruction are:
1. Open a command prompt such as a vscode terminal. 
2. Go to the `weather-dashboard` folder.
3. Run the app:
```
run-app.bat
```

### View the dashboard
Start the browser and go to `http://localhost:3000/`.

## Step 4 - Stopping the app
Kill the dotnet api processes:
```
taskkill /F /IM dotnet.exe
```

