using WeatherApi.Models.Responses;

namespace WeatherApi.Services;

public interface ILocationService
{
    Task<IReadOnlyList<LocationDto>> SearchAsync(
        string query,
        int limit,
        CancellationToken cancellationToken);
}