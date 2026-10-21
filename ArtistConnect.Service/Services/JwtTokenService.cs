using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ArtistConnect.Model.Entities;
using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtistConnect.Service.Services;

public sealed class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public AuthTokenResponse GerarToken(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        string issuer = GetRequiredSetting("Jwt:Issuer");
        string audience = GetRequiredSetting("Jwt:Audience");
        string signingKey = GetRequiredSetting("Jwt:SigningKey");
        byte[] keyBytes = Encoding.UTF8.GetBytes(signingKey);
        if (keyBytes.Length < 32)
        {
            throw new InvalidOperationException("Jwt:SigningKey deve possuir ao menos 32 bytes.");
        }

        int expirationInHours = configuration.GetValue<int?>("Jwt:ExpirationInHours") ?? 24;
        if (expirationInHours is < 1 or > 24)
        {
            throw new InvalidOperationException("Jwt:ExpirationInHours deve estar entre 1 e 24 horas.");
        }

        DateTimeOffset expiresAt = DateTimeOffset.UtcNow.AddHours(expirationInHours);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials);

        return new AuthTokenResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            "Bearer",
            expiresAt);
    }

    private string GetRequiredSetting(string key) =>
        configuration[key] is { Length: > 0 } value
            ? value
            : throw new InvalidOperationException($"A configuração obrigatória '{key}' não foi definida.");
}
