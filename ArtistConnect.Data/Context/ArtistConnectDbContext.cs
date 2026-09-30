using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Data.Context;

public class ArtistConnectDbContext : DbContext
{
    public DbSet<Artista> Artistas => Set<Artista>();
    public DbSet<Musica> Musicas => Set<Musica>();

    public ArtistConnectDbContext(DbContextOptions<ArtistConnectDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArtistConnectDbContext).Assembly);
    }
}