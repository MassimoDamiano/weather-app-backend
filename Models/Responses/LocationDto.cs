namespace WeatherApi.Models.Responses;

/// <summary>
///     Buscar localizacion del pais elegida por el usuario
/// </summary>

public class LocationDto
{
    public string Name { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string? State { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
