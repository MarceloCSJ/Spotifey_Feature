using ArtistConnect.Business;
using ArtistConnect.Model.Entities;
using ArtistConnect.Service.Interfaces;
using ArtistConnect.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ArtistConnect.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddArtistConnectServices(this IServiceCollection services)
    {
        services.AddScoped<ArtistaBusiness>();
        services.AddScoped<MusicaBusiness>();
        services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<ITokenService, JwtTokenService>();
        services.AddScoped<IArtistaService, Services.ArtistaService>();
        services.AddScoped<IMusicaService, Services.MusicaService>();
        services.AddSingleton<ITokenValidationService, JwtTokenValidationService>();

        return services;
    }
}
