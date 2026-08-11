using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models.Responses;
using WeatherApi.Services;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/locations")]
public sealed class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LocationDto>>> SearchAsync(
        [FromQuery, Required, MinLength(2), MaxLength(100)] string query,
        [FromQuery, Range(1, 5)] int limit = 5,
        CancellationToken cancellationToken = default)
    {
        var locations = await _locationService.SearchAsync(
            query.Trim(),
            limit,
            cancellationToken);

        return Ok(locations);
    }
}
