using System.Text.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WeatherApi.Infrastructure;

namespace WeatherApi.Tests;

public sealed class GlobalExceptionHandlerTests
{
    [Theory]
    [InlineData(
        typeof(HttpRequestException),
        StatusCodes.Status502BadGateway,
        "No se pudo consultar el proveedor meteorológico.")]
    [InlineData(
        typeof(TaskCanceledException),
        StatusCodes.Status504GatewayTimeout,
        "El proveedor meteorológico tardó demasiado en responder.")]
    [InlineData(
        typeof(InvalidOperationException),
        StatusCodes.Status500InternalServerError,
        "Ocurrió un error interno inesperado.")]
    public async Task TryHandleAsync_WritesExpectedProblemDetails(
        Type exceptionType,
        int expectedStatusCode,
        string expectedTitle)
    {
        // Arrange
        var handler = new GlobalExceptionHandler();
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();

        var exception = Assert.IsAssignableFrom<Exception>(
            Activator.CreateInstance(exceptionType));

        // Act
        var handled = await handler.TryHandleAsync(
            httpContext,
            exception,
            CancellationToken.None);

        httpContext.Response.Body.Position = 0;

        var problem = await JsonSerializer.DeserializeAsync<ProblemDetails>(
            httpContext.Response.Body,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        // Assert
        Assert.True(handled);
        Assert.Equal(expectedStatusCode, httpContext.Response.StatusCode);
        Assert.NotNull(problem);
        Assert.Equal(expectedStatusCode, problem.Status);
        Assert.Equal(expectedTitle, problem.Title);
    }
}
