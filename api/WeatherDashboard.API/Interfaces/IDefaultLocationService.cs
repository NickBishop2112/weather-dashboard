namespace WeatherDashboard.API.Interfaces;

public interface IDefaultLocationService
{
    void SetLocation(string location);
    string GetLocation();
}