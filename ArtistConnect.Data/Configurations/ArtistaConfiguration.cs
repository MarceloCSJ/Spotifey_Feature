using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtistConnect.Data.Configurations;

public class ArtistaConfiguration : IEntityTypeConfiguration<Artista>
{
    public void Configure(EntityTypeBuilder<Artista> builder)
    {
        builder.ToTable("Artistas");
        builder.HasKey(artista => artista.ID);
        builder.Property(artista => artista.ID).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(artista => artista.Nome).HasMaxLength(100).IsRequired();
        builder.Property(artista => artista.Bio).HasColumnType("text");
        builder.Property(artista => artista.GeneroMusical).HasMaxLength(50);
    }
}