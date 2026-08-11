using WeatherApi.Clients;
using WeatherApi.Models.Responses;

namespace WeatherApi.Services;

internal sealed class LocationService : ILocationService
{
    private readonly OpenWeatherClient _client;

    public LocationService(OpenWeatherClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<LocationDto>> SearchAsync(
        string query,
        int limit,
        CancellationToken cancellationToken)
    {
        var apiLocations = await _client.SearchLocationsAsync(
            query,
            limit,
            cancellationToken);

        return apiLocations
            .Select(location => new LocationDto
            {
                Name = location.Name,
                Country = location.Country,
                State = location.State,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            })
            .ToList();
    }
}
