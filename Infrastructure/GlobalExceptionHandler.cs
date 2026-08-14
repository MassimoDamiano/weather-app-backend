using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Infrastructure;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        int statusCode = exception switch
        {
            HttpRequestException => 502,
            TaskCanceledException => 504,
            _ => 500
        };

        string title = exception switch
        {
            HttpRequestException => "No se pudo consultar el proveedor meteorológico.",
            TaskCanceledException => "El proveedor meteorológico tardó demasiado en responder.",
            _ => "Ocurrió un error interno inesperado."
        };

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}
