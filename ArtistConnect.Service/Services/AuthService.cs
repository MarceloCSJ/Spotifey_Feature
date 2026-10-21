using ArtistConnect.Business.Interfaces;
using ArtistConnect.Model.Entities;
using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ArtistConnect.Service.Services;

public sealed class AuthService(
    IUsuarioDAO usuarioDAO,
    IPasswordHasher<Usuario> passwordHasher,
    ITokenService tokenService) : IAuthService
{
    public async Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        string normalizedEmail = NormalizarEmail(request.Email);
        if (await usuarioDAO.BuscarPorEmailAsync(normalizedEmail, cancellationToken) is not null)
        {
            throw new InvalidOperationException("Já existe uma conta cadastrada com esse e-mail.");
        }

        var usuario = new Usuario
        {
            Nome = request.Nome.Trim(),
            Email = normalizedEmail
        };
        usuario.SenhaHash = passwordHasher.HashPassword(usuario, request.Senha);

        await usuarioDAO.InserirAsync(usuario, cancellationToken);
    }

    public async Task<AuthTokenResponse?> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        string normalizedEmail = NormalizarEmail(request.Email);
        Usuario? usuario = await usuarioDAO.BuscarPorEmailAsync(normalizedEmail, cancellationToken);
        if (usuario is null)
        {
            return null;
        }

        PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(
            usuario,
            usuario.SenhaHash,
            request.Senha);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return tokenService.GerarToken(usuario);
    }

    private static string NormalizarEmail(string email) => email.Trim().ToUpperInvariant();
}
