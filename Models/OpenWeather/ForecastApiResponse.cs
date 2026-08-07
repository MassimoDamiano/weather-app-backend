using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa la respuesta completa del pronóstico recibida desde OpenWeather.
/// </summary>
internal sealed class ForecastApiResponse
{
    [JsonPropertyName("list")]
    public required IReadOnlyList<ForecastItemApiResponse> Items
    {
        get;
        init;
    }

    [JsonPropertyName("city")]
    public required ForecastCityApiResponse City { get; init; }
}
