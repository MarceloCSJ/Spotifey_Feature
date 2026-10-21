using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtistConnect.Data.Configurations;

public sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(usuario => usuario.Id);
        builder.Property(usuario => usuario.Id).ValueGeneratedOnAdd();
        builder.Property(usuario => usuario.Nome).HasMaxLength(100).IsRequired();
        builder.Property(usuario => usuario.Email).HasMaxLength(254).IsRequired();
        builder.HasIndex(usuario => usuario.Email).IsUnique();
        builder.Property(usuario => usuario.SenhaHash).HasMaxLength(500).IsRequired();
    }
}
