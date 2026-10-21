using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business.Interfaces;

public interface IUsuarioDAO
{
    Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken);
    Task InserirAsync(Usuario usuario, CancellationToken cancellationToken);
}
