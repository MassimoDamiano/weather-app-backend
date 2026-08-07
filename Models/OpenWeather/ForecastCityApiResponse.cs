using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa los datos de ciudad necesarios para interpretar el pronóstico.
/// </summary>
internal sealed class ForecastCityApiResponse
{
    [JsonPropertyName("timezone")]
    public int TimezoneOffsetSeconds { get; init; }
}