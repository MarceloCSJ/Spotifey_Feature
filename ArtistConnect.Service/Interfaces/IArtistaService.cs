using ArtistConnect.Service.Contracts;

namespace ArtistConnect.Service.Interfaces;

public interface IArtistaService
{
    Task<IReadOnlyList<ArtistaResponse>> ListarAsync(CancellationToken cancellationToken);
    Task<ArtistaResponse?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<ArtistaResponse> CadastrarAsync(CreateArtistaRequest request, CancellationToken cancellationToken);
    Task<ArtistaResponse?> AtualizarAsync(int id, UpdateArtistaRequest request, CancellationToken cancellationToken);
    Task ExcluirAsync(int id, CancellationToken cancellationToken);
}
