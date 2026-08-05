namespace WeatherApi.Models.Responses;
/// <summary>
/// Representa el pronóstico meteorológico correspondiente a una hora.
/// </summary>

public sealed class HourlyForecastDto
{
	public double Temperature { get; init; }

	public double PrecipitationProbability { get; init; }

	public DateTimeOffset DateTime { get; init; }

	public required string IconCode { get; init; }
}