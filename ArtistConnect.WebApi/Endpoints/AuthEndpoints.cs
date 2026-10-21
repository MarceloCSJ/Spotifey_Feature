using System.ComponentModel.DataAnnotations;
using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ArtistConnect.WebApi.Endpoints;

public static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthEndpoints(
        this IEndpointRouteBuilder routes,
        bool allowRegistration)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/auth")
            .WithTags("Autenticação")
            .AllowAnonymous();

        if (allowRegistration)
        {
            group.MapPost("/register", async Task<Results<NoContent, ValidationProblem>> (
                RegisterRequest request,
                IAuthService authService,
                CancellationToken cancellationToken) =>
            {
                Dictionary<string, string[]> errors = Validate(request);
                if (errors.Count > 0)
                {
                    return TypedResults.ValidationProblem(errors);
                }

                await authService.RegisterAsync(request, cancellationToken);
                return TypedResults.NoContent();
            })
                .WithName("RegistrarUsuario")
                .WithSummary("Registra uma conta de teste")
                .WithDescription("Disponível somente em Development. A senha é armazenada como hash.")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesValidationProblem();
        }

        group.MapPost("/login", async Task<Results<Ok<AuthTokenResponse>, UnauthorizedHttpResult, ValidationProblem>> (
            LoginRequest request,
            IAuthService authService,
            CancellationToken cancellationToken) =>
        {
            Dictionary<string, string[]> errors = Validate(request);
            if (errors.Count > 0)
            {
                return TypedResults.ValidationProblem(errors);
            }

            AuthTokenResponse? token = await authService.LoginAsync(request, cancellationToken);
            return token is null
                ? TypedResults.Unauthorized()
                : TypedResults.Ok(token);
        })
            .WithName("Login")
            .WithSummary("Autentica uma conta e retorna um JWT")
            .WithDescription("Retorna um token Bearer assinado para acessar os endpoints protegidos.")
            .Produces<AuthTokenResponse>()
            .Produces(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem();

        return group;
    }

    private static Dictionary<string, string[]> Validate<TRequest>(TRequest request)
    {
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(
            request!,
            new ValidationContext(request!),
            validationResults,
            validateAllProperties: true);

        return isValid
            ? []
            : validationResults
                .SelectMany(result => result.MemberNames.Select(memberName => new
                {
                    MemberName = memberName,
                    ErrorMessage = result.ErrorMessage ?? "Valor inválido."
                }))
                .GroupBy(error => error.MemberName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());
    }
}
