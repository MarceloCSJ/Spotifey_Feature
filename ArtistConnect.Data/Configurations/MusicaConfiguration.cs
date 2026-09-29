using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtistConnect.Data.Configurations;

public class MusicaConfiguration : IEntityTypeConfiguration<Musica>
{
    public void Configure(EntityTypeBuilder<Musica> builder)
    {
        builder.ToTable("Musicas");
        builder.HasKey(musica => musica.ID);
        builder.Property(musica => musica.ID).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(musica => musica.Titulo).HasMaxLength(100).IsRequired();
        builder.Property(musica => musica.ArtistaResponsavel).HasMaxLength(100);
        builder.Property(musica => musica.Duracao).HasMaxLength(10);
    }
}