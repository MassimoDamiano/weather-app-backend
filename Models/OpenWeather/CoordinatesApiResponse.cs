using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa las coordenadas recibidas desde OpenWeather.
/// </summary>
internal sealed class CoordinatesApiResponse
{
    [JsonPropertyName("lon")]
    public double Longitude { get; init; }

    [JsonPropertyName("lat")]
    public double Latitude { get; init; }
}