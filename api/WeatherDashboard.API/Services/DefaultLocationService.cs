using WeatherDashboard.API.Interfaces;

namespace WeatherDashboard.API.Services;

public class DefaultLocationService : IDefaultLocationService
{
    private readonly string _path; 
        
    public DefaultLocationService()
    {
        _path = Path.Combine(Environment.CurrentDirectory, "location.txt");
    }
    
    public void SetLocation(string location)
    {
        File.WriteAllText(_path, location);
    }
    
    public string GetLocation()
    {
        return File.ReadAllText(_path);
    }
}