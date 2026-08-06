using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa los datos del viento recibidos desde OpenWeather.
/// </summary>

internal sealed class WindApiResponse
{
    [JsonPropertyName("speed")]
    public double Speed { get; init; }
}
