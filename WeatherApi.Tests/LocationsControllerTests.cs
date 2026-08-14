using Moq;
using WeatherApi.Services;
using WeatherApi.Models.Responses;
using WeatherApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Tests;

public class LocationsControllerTests
{
    [Fact]
    public async Task SearchAsync_ReturnsOkWithLocations()
    {
        // Arrange: preparar
        var serviceMock = new Mock<ILocationService>();

        IReadOnlyList<LocationDto> expectedLocations = new[]
        {
            new LocationDto
            {
                Name = "Córdoba",
                Country = "AR",
                State = "Córdoba",
                Latitude = -31.42,
                Longitude = -64.18
            }
        };

        serviceMock
            .Setup(service => service.SearchAsync(
                "Córdoba",
                5,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedLocations);

        var controller = new LocationsController(serviceMock.Object);

        // Act: ejecutar y guardar el resultado
        var result = await controller.SearchAsync("Córdoba", 5, CancellationToken.None);

        // Assert: comprobar
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var locations = Assert.IsAssignableFrom<IReadOnlyList<LocationDto>>(
            okResult.Value);

        var location = Assert.Single(locations);
        Assert.Equal("Córdoba", location.Name);
        Assert.Equal("AR", location.Country);

        serviceMock.Verify(
            service => service.SearchAsync(
                "Córdoba",
                5,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SearchAsync_TrimsQueryBeforeCallingService()
    {
        // Arrange
        var serviceMock = new Mock<ILocationService>();

        serviceMock
            .Setup(service => service.SearchAsync(
                "Córdoba",
                5,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Array.Empty<LocationDto>());

        var controller = new LocationsController(serviceMock.Object);

        // Act
        await controller.SearchAsync(
            "  Córdoba  ",
            5,
            CancellationToken.None);

        // Assert
        serviceMock.Verify(
            service => service.SearchAsync(
                "Córdoba",
                5,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
