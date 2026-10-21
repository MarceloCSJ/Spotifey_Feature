using ArtistConnect.Data;

namespace ArtistConnect.WebApi.Infrastructure;

public static class DatabaseConnection
{
    public static IServiceCollection AddArtistConnectDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("ArtistConnect")
            ?? throw new InvalidOperationException(
                "A conexão MySQL não foi configurada. Em Development, defina-a com " +
                "`dotnet user-secrets set \"ConnectionStrings:ArtistConnect\" \"<sua-connection-string>\" --project ArtistConnect.WebApi`.");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "A conexão MySQL está vazia. Defina-a usando User Secrets; veja os comandos no README da WebApi.");
        }

        return services.AddArtistConnectData(connectionString);
    }
}
