using ArtistConnect.Business.Interfaces;
using ArtistConnect.Data.Context;
using ArtistConnect.Data.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArtistConnect.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddArtistConnectData(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ArtistConnectDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IArtistaDAO, ArtistaDAO>();
        services.AddScoped<IMusicaDAO, MusicaDAO>();

        return services;
    }
}