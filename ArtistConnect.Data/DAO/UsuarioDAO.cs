using ArtistConnect.Business.Interfaces;
using ArtistConnect.Data.Context;
using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Data.DAO;

public sealed class UsuarioDAO(ArtistConnectDbContext context) : IUsuarioDAO
{
    public Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken) =>
        context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(usuario => usuario.Email == email, cancellationToken);

    public async Task InserirAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        await context.Usuarios.AddAsync(usuario, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
