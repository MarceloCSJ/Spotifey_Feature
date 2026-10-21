using ArtistConnect.Classes;
using ArtistConnect.Data;
using ArtistConnect.Data.Context;
using ArtistConnect.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets("418ad990-dfd1-4dea-8a25-452bc76fecc1")
    .AddEnvironmentVariables()
    .Build();

string? connectionString = configuration.GetConnectionString("ArtistConnect");
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("Connection string do MySQL não configurada. Defina com:");
    Console.WriteLine("dotnet user-secrets set \"ConnectionStrings:ArtistConnect\" \"<connection-string>\" --project ArtistConnect.WebApi");
    return;
}

var services = new ServiceCollection();
services.AddArtistConnectData(connectionString);
services.AddArtistConnectServices();
services.AddScoped<MenuArtista>();
services.AddScoped<MenuMusica>();
services.AddScoped<MenuPrincipal>();

using ServiceProvider provider = services.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

try
{
    scope.ServiceProvider.GetRequiredService<ArtistConnectDbContext>().Database.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine($"Não foi possível conectar ao banco: {ex.Message}");
    return;
}

Console.Clear();
scope.ServiceProvider.GetRequiredService<MenuPrincipal>().Menu();
