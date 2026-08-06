using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa las mediciones principales del clima recibidas desde OpenWeather.
/// </summary>
internal sealed class MainWeatherApiResponse
{
    [JsonPropertyName("temp")]
    public double Temperature { get; init; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }

    [JsonPropertyName("temp_min")]
    public double MinTemperature { get; init; }

    [JsonPropertyName("temp_max")]
    public double MaxTemperature { get; init; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; init; }
}