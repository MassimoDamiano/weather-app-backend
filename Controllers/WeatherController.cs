using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models.Responses;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/weather")]
public sealed class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<ActionResult<WeatherResponse>> GetWeatherAsync(
        [FromQuery, Range(-90, 90)] double latitude,
        [FromQuery, Range(-180, 180)] double longitude,
        CancellationToken cancellationToken)
    {
        var weather = await _weatherService.GetWeatherAsync(
            latitude,
            longitude,
            cancellationToken);

        return Ok(weather);
    }
}
