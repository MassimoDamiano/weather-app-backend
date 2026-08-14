
using Moq;
using WeatherApi.Services;
using WeatherApi.Models.Responses;
using WeatherApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Tests;

public sealed class WeatherControllerTests
{
    [Fact]
    public async Task GetWeatherAsync_ReturnsOkWithWeather()
    {
        //Arrange
        var serviceMock = new Mock<IWeatherService>();
        var expectedWeather = new WeatherResponse
        {
            City = new CityDto
            {
                Name = "Córdoba",
                Latitude = -31.42,
                Longitude = -64.18
            },
            Description = "nublado",
            IconCode = "04d",
            HourlyForecasts = [],
            DailyForecasts = []
        };

        serviceMock
            .Setup(service => service.GetWeatherAsync(
                -31.42,
                -64.18,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedWeather);

        var controller = new WeatherController(serviceMock.Object);

        // Act
        var result = await controller.GetWeatherAsync(
            -31.42,
            -64.18,
            CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Same(expectedWeather, okResult.Value);

        serviceMock.Verify(
            service => service.GetWeatherAsync(
                -31.42,
                -64.18,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

