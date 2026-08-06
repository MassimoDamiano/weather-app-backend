using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa un intervalo de tres horas del pronóstico de OpenWeather.
/// </summary>
internal sealed class ForecastItemApiResponse
{
    [JsonPropertyName("dt")]
    public long Timestamp { get; init; }

    [JsonPropertyName("main")]
    public required MainWeatherApiResponse Main { get; init; }

    [JsonPropertyName("weather")]
    public required IReadOnlyList<WeatherConditionApiResponse> WeatherConditions
    {
        get;
        init;
    }

    [JsonPropertyName("pop")]
    public double PrecipitationProbability { get; init; }
}