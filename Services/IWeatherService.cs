using WeatherApi.Models.Responses;

namespace WeatherApi.Services;

/// <summary>
/// Define las operaciones meteorológicas disponibles para la aplicación.
/// </summary>
public interface IWeatherService
{
    Task<WeatherResponse> GetWeatherAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken);
}