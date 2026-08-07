using System.Globalization;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using WeatherApi.Configuration;
using WeatherApi.Models.OpenWeather;

namespace WeatherApi.Clients;

/// <summary>
/// Se comunica con la API externa de OpenWeather.
/// </summary>
internal sealed class OpenWeatherClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherOptions _options;

    public OpenWeatherClient(
        HttpClient httpClient,
        IOptions<OpenWeatherOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<CurrentWeatherApiResponse> GetCurrentWeatherAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken)
    {
        var queryParameters = new Dictionary<string, string?>
        {
            ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["appid"] = _options.ApiKey,
            ["units"] = "metric",
            ["lang"] = "es"
        };

        var requestUri = QueryHelpers.AddQueryString(
            "/data/2.5/weather",
            queryParameters);

        using var response = await _httpClient.GetAsync(
            requestUri,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var weather = await response.Content
            .ReadFromJsonAsync<CurrentWeatherApiResponse>(
                cancellationToken);

        return weather
            ?? throw new InvalidOperationException(
                "OpenWeather devolvió una respuesta vacía.");
    }

    public async Task<ForecastApiResponse> GetForecastAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken)
    {
        var queryParameters = new Dictionary<string, string?>
        {
            ["lat"] = latitude.ToString(CultureInfo.InvariantCulture),
            ["lon"] = longitude.ToString(CultureInfo.InvariantCulture),
            ["appid"] = _options.ApiKey,
            ["units"] = "metric",
            ["lang"] = "es"
        };

        var requestUri = QueryHelpers.AddQueryString(
            "/data/2.5/forecast",
            queryParameters);

        using var response = await _httpClient.GetAsync(
            requestUri,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var forecast = await response.Content
            .ReadFromJsonAsync<ForecastApiResponse>(
                cancellationToken);

        return forecast
            ?? throw new InvalidOperationException(
                "OpenWeather devolvió un pronóstico vacío.");
    }

    public async Task<IReadOnlyList<GeocodingApiResponse>> SearchLocationsAsync(
        string query,
        int limit,
        CancellationToken cancellationToken)
    {
        var queryParameters = new Dictionary<string, string?>
        {
            ["q"] = query,
            ["limit"] = limit.ToString(CultureInfo.InvariantCulture),
            ["appid"] = _options.ApiKey
        };

        var requestUri = QueryHelpers.AddQueryString(
            "/geo/1.0/direct",
            queryParameters);

        using var response = await _httpClient.GetAsync(
            requestUri,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var locations = await response.Content
            .ReadFromJsonAsync<List<GeocodingApiResponse>>(
                cancellationToken);

        return locations
            ?? new List<GeocodingApiResponse>();
    }
}
