using System.ComponentModel.DataAnnotations;

namespace ArtistConnect.Service.Contracts;

/// <summary>Dados necessários para registrar uma conta.</summary>
public sealed record RegisterRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public required string Nome { get; init; }

    [Required, EmailAddress, StringLength(254)]
    public required string Email { get; init; }

    [Required, MinLength(8), StringLength(128)]
    public required string Senha { get; init; }
}

/// <summary>Credenciais usadas para autenticar uma conta.</summary>
public sealed record LoginRequest
{
    [Required, EmailAddress, StringLength(254)]
    public required string Email { get; init; }

    [Required, MinLength(8), StringLength(128)]
    public required string Senha { get; init; }
}

/// <summary>Token JWT e informações da sua validade.</summary>
public sealed record AuthTokenResponse(string AccessToken, string TokenType, DateTimeOffset ExpiresAt);
