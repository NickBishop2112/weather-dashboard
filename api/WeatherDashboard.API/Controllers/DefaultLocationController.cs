
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WeatherDashboard.API.Interfaces;

namespace WeatherDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DefaultLocationController(IDefaultLocationService defaultLocationService) : ControllerBase
{
    /// <summary>
    /// Sets the default location.
    /// </summary>
    /// <param name="location">The location to set as default.</param>
    /// <returns>A response indicating success or failure.</returns>
    /// <response code="200">If the location is set successfully.</response>
    /// <response code="400">if the location is missing</response>
    /// <response code="500">If an unexpected error occurs.</response>
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
    
    /// <summary>
    /// Retrieves the current default location.
    /// </summary>
    /// <returns>The current default location.</returns>
    /// <response code="200">Returns the default location.</response>
    /// <response code="500">If an unexpected error occurs.</response>
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