using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa la respuesta de clima actual recibida desde OpenWeather.
/// </summary>
internal sealed class CurrentWeatherApiResponse
{
    [JsonPropertyName("coord")]
    public required CoordinatesApiResponse Coordinates { get; init; }

    [JsonPropertyName("weather")]
    public required IReadOnlyList<WeatherConditionApiResponse> WeatherConditions
    {
        get;
        init;
    }

    [JsonPropertyName("main")]
    public required MainWeatherApiResponse Main { get; init; }

    [JsonPropertyName("wind")]
    public required WindApiResponse Wind { get; init; }

    [JsonPropertyName("dt")]
    public long Timestamp { get; init; }

    [JsonPropertyName("name")]
    public required string CityName { get; init; }
}