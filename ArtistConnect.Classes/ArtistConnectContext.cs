using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Classes;

public class ArtistConnectContext : DbContext
{
    public DbSet<Artista> Artistas => Set<Artista>();
    public DbSet<Musica> Musicas => Set<Musica>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        string connectionString = Environment.GetEnvironmentVariable("ARTISTCONNECT_CONNECTION_STRING")
            ?? "Server=localhost;Port=3306;Database=ArtistConnect;User=root;Password=;";

        optionsBuilder.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 0)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artista>(entity =>
        {
            entity.ToTable("Artistas");
            entity.HasKey(artista => artista.Id);
            entity.Property(artista => artista.Id).ValueGeneratedOnAdd();
            entity.Property(artista => artista.Nome).HasMaxLength(100).IsRequired();
            entity.Property(artista => artista.Bio).HasColumnType("text");
            entity.Property(artista => artista.GeneroMusical).HasMaxLength(50);
        });

        modelBuilder.Entity<Musica>(entity =>
        {
            entity.ToTable("Musicas");
            entity.HasKey(musica => musica.Id);
            entity.Property(musica => musica.Id).ValueGeneratedOnAdd();
            entity.Property(musica => musica.Titulo).HasMaxLength(100).IsRequired();
            entity.Property(musica => musica.ArtistaResponsavel).HasMaxLength(100);
            entity.Property(musica => musica.Duracao).HasMaxLength(10);
        });
    }
}