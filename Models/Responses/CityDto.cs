namespace WeatherApi.Models.Responses;

/// <summary>
/// Representa la ubicación geográfica asociada al clima.
/// </summary>

public sealed class CityDto
{
    public required string Name { get; init; }

    public double Latitude { get; init; }

    public double Longitude { get; init; }

}