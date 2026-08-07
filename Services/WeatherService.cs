using WeatherApi.Clients;
using WeatherApi.Models.Responses;

namespace WeatherApi.Services;

/// <summary>
/// Combina y transforma los datos meteorológicos para la aplicación.
/// </summary>
internal sealed class WeatherService : IWeatherService
{
    private readonly OpenWeatherClient _openWeatherClient;

    public WeatherService(OpenWeatherClient openWeatherClient)
    {
        _openWeatherClient = openWeatherClient;
    }

    public async Task<WeatherResponse> GetWeatherAsync(
        double latitude,
        double longitude,
        CancellationToken cancellationToken)
    {
        var currentWeatherTask =
            _openWeatherClient.GetCurrentWeatherAsync(
                latitude,
                longitude,
                cancellationToken);

        var forecastTask =
            _openWeatherClient.GetForecastAsync(
                latitude,
                longitude,
                cancellationToken);

        await Task.WhenAll(
            currentWeatherTask,
            forecastTask);

        var currentWeather = await currentWeatherTask;
        var forecast = await forecastTask;

        var currentCondition =
            currentWeather.WeatherConditions.FirstOrDefault()
            ?? throw new InvalidOperationException(
                "OpenWeather no devolvió una condición meteorológica.");

        var timezoneOffset = TimeSpan.FromSeconds(
            forecast.City.TimezoneOffsetSeconds);

        var hourlyForecasts = forecast.Items
            .Select(item =>
            {
                var condition =
                    item.WeatherConditions.FirstOrDefault()
                    ?? throw new InvalidOperationException(
                        "Un intervalo no contiene una condición meteorológica.");

                return new HourlyForecastDto
                {
                    Temperature = item.Main.Temperature,
                    PrecipitationProbability =
                        item.PrecipitationProbability,
                    DateTime = DateTimeOffset
                        .FromUnixTimeSeconds(item.Timestamp)
                        .ToOffset(timezoneOffset),
                    IconCode = condition.IconCode
                };
            })
            .ToArray();

        var dailyForecasts = hourlyForecasts
            .GroupBy(item =>
                DateOnly.FromDateTime(item.DateTime.DateTime))
            .OrderBy(group => group.Key)
            .Take(5)
            .Select(group =>
            {
                var representativeForecast = group
                    .OrderBy(item =>
                        Math.Abs(
                            (item.DateTime.TimeOfDay
                             - TimeSpan.FromHours(12))
                            .TotalMinutes))
                    .First();

                return new DailyForecastDto
                {
                    Date = group.Key,
                    MaxTemp = group.Max(
                        item => item.Temperature),
                    MinTemp = group.Min(
                        item => item.Temperature),
                    IconCode = representativeForecast.IconCode
                };
            })
            .ToArray();

        return new WeatherResponse
        {
            City = new CityDto
            {
                Name = currentWeather.CityName,
                Latitude = currentWeather.Coordinates.Latitude,
                Longitude = currentWeather.Coordinates.Longitude
            },
            Temperature = currentWeather.Main.Temperature,
            MaxTemp = currentWeather.Main.MaxTemperature,
            MinTemp = currentWeather.Main.MinTemperature,
            Description = currentCondition.Description,
            IconCode = currentCondition.IconCode,
            Humidity = currentWeather.Main.Humidity,
            WindSpeed = currentWeather.Wind.Speed,
            FeelsLike = currentWeather.Main.FeelsLike,
            HourlyForecasts = hourlyForecasts,
            DailyForecasts = dailyForecasts
        };
    }
}