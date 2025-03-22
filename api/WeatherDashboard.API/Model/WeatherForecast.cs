namespace WeatherDashboard.API.Model;

public record WeatherForecast(
    double Temperature,
    int Humidity,
    double WindSpeed,
    string WeatherIcon
);