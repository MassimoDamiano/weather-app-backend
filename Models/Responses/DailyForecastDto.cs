namespace WeatherApi.Models.Responses;

/// <summary>
/// Representa el resumen meteorológico de un día.
/// </summary>

public sealed class DailyForecastDto
{
    public DateOnly Date { get; init; }

    public double MaxTemp { get; init; }

    public double MinTemp { get; init; }

    public required string IconCode { get; init; }
}