namespace WeatherApi.Configuration;

/// <summary>
/// Contiene la configuracion necesaria para conectarse con OpenWeather
/// </summary>

public sealed class OpenWeatherOptions
{
    public const string SectionName = "OpenWeather";

    public string BaseUrl { get; init; } = string.Empty;

    public string ApiKey { get; init; } = string.Empty;

}
