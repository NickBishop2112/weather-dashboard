using Microsoft.AspNetCore.Mvc;
using Serilog;
using WeatherDashboard.API.Interfaces;

namespace WeatherDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DefaultLocationController(IDefaultLocationService defaultLocationService) : ControllerBase
{
    [HttpPut("{location}")]
    public IActionResult SetLocation(string location)
    {
        try
        {
            Log.Information($"Setting default location to: {location}");
            defaultLocationService.SetLocation(location);
            return Ok("Success");
        }
        catch (Exception e)
        {
            Log.Information($"Error has occurred: {e}");
            return StatusCode(500, new { message = $"An unexpected error has occurred: ${e.Message}" });
        }
    }
    
    [HttpGet()]
    public IActionResult GetLocation()
    {
        try
        {
            Log.Information("Getting default location");
            var location = defaultLocationService.GetLocation();
            return Ok(location);
        }
        catch (Exception e)
        {
            Log.Information($"Error has occurred: {e}");
            return StatusCode(500, new { message = $"An unexpected error has occurred: ${e.Message}" });
        }
    }
}