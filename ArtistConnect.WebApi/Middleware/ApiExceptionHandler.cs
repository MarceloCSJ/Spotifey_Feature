using System.ComponentModel.DataAnnotations;
using ArtistConnect.Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ArtistConnect.WebApi.Middleware;

internal sealed class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        (int statusCode, string title)? error = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado"),
            ValidationException => (StatusCodes.Status400BadRequest, "Dados inválidos"),
            DuplicateEntityException => (StatusCodes.Status409Conflict, "Conflito"),
            _ => null
        };

        if (error is null)
        {
            return false;
        }

        logger.LogWarning(exception, "Erro de API: {Title}", error.Value.title);
        httpContext.Response.StatusCode = error.Value.statusCode;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = error.Value.statusCode,
            Title = error.Value.title,
            Detail = exception is ValidationException or DuplicateEntityException
                ? exception.Message
                : error.Value.title,
            Instance = httpContext.Request.Path
        }, cancellationToken);

        return true;
    }
}
