using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ArtistConnect.Data.Context;

public sealed class ArtistConnectDbContextFactory : IDesignTimeDbContextFactory<ArtistConnectDbContext>
{
    public ArtistConnectDbContext CreateDbContext(string[] args)
    {
        string connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__ArtistConnect")
            ?? "Server=localhost;Database=ArtistConnect;User=root;Password=S&nac2024;";

        var optionsBuilder = new DbContextOptionsBuilder<ArtistConnectDbContext>();
        optionsBuilder.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 0)));

        return new ArtistConnectDbContext(optionsBuilder.Options);
    }
}
