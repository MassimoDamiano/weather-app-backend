namespace WeatherApi.Models.Responses;

/// <summary>
/// Representa el clima actual y los pronósticos de una ubicación.
/// </summary>
public sealed class WeatherResponse
{
    public required CityDto City { get; init; }

    public double Temperature { get; init; }

    public double MaxTemp { get; init; }

    public double MinTemp { get; init; }

    public required string Description { get; init; }

    public required string IconCode { get; init; }

    public int Humidity { get; init; }

    public double WindSpeed { get; init; }

    public double FeelsLike { get; init; }

    public required IReadOnlyList<HourlyForecastDto> HourlyForecasts
    {
        get;
        init;
    }

    public required IReadOnlyList<DailyForecastDto> DailyForecasts
    {
        get;
        init;
    }
}