using System.Text.Json.Serialization;

namespace WeatherApi.Models.OpenWeather;

/// <summary>
/// Representa una condición meteorológica recibida desde OpenWeather.
/// </summary>
internal sealed class WeatherConditionApiResponse
{
	[JsonPropertyName("description")]
	public required string Description { get; init; }

	[JsonPropertyName("icon")]
	public required string IconCode { get; init; }
}