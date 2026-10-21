using System.Text;
using ArtistConnect.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtistConnect.Service.Services;

public sealed class JwtTokenValidationService(IConfiguration configuration) : ITokenValidationService
{
    public TokenValidationParameters CreateValidationParameters()
    {
        string issuer = GetRequiredSetting("Jwt:Issuer");
        string audience = GetRequiredSetting("Jwt:Audience");
        string? signingKey = configuration["Jwt:SigningKey"];
        string? authority = configuration["Jwt:Authority"];
        SymmetricSecurityKey? issuerSigningKey = null;

        if (!string.IsNullOrWhiteSpace(signingKey))
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(signingKey);
            if (keyBytes.Length < 32)
            {
                throw new InvalidOperationException("Jwt:SigningKey deve possuir ao menos 32 bytes.");
            }

            issuerSigningKey = new SymmetricSecurityKey(keyBytes);
        }
        else if (string.IsNullOrWhiteSpace(authority))
        {
            throw new InvalidOperationException(
                "Configure Jwt:Authority ou Jwt:SigningKey para validar tokens.");
        }

        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateIssuerSigningKey = issuerSigningKey is not null || !string.IsNullOrWhiteSpace(authority),
            IssuerSigningKey = issuerSigningKey,
            ValidAlgorithms = issuerSigningKey is null ? null : [SecurityAlgorithms.HmacSha256],
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    }

    private string GetRequiredSetting(string key) =>
        configuration[key] is { Length: > 0 } value
            ? value
            : throw new InvalidOperationException($"A configuração obrigatória '{key}' não foi definida.");
}
