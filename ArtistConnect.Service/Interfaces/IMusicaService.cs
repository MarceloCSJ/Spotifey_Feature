using ArtistConnect.Service.Contracts;

namespace ArtistConnect.Service.Interfaces;

public interface IMusicaService
{
    Task<IReadOnlyList<MusicaResponse>> ListarAsync(CancellationToken cancellationToken);
    Task<MusicaResponse?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<MusicaResponse> CadastrarAsync(CreateMusicaRequest request, CancellationToken cancellationToken);
    Task<MusicaResponse?> AtualizarAsync(int id, UpdateMusicaRequest request, CancellationToken cancellationToken);
    Task ExcluirAsync(int id, CancellationToken cancellationToken);
}
