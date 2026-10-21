using ArtistConnect.Business;
using ArtistConnect.Business.Interfaces;
using ArtistConnect.Model.Entities;
using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;

namespace ArtistConnect.Service.Services;

public sealed class MusicaService(
    IMusicaDAO musicaDAO,
    MusicaBusiness musicaBusiness) : IMusicaService
{
    public async Task<IReadOnlyList<MusicaResponse>> ListarAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Musica> musicas = await musicaDAO.ListarAsync(cancellationToken);
        return musicas.Select(CriarResposta).ToArray();
    }

    public async Task<MusicaResponse?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        Musica? musica = await musicaDAO.BuscarPorIdAsync(id, cancellationToken);
        return musica is null ? null : CriarResposta(musica);
    }

    public async Task<MusicaResponse> CadastrarAsync(
        CreateMusicaRequest request,
        CancellationToken cancellationToken)
    {
        var musica = new Musica
        {
            Titulo = request.Titulo,
            ArtistaResponsavel = request.ArtistaResponsavel,
            Duracao = request.Duracao
        };

        await musicaBusiness.CadastrarAsync(musica, cancellationToken);
        return CriarResposta(musica);
    }

    public async Task<MusicaResponse?> AtualizarAsync(
        int id,
        UpdateMusicaRequest request,
        CancellationToken cancellationToken)
    {
        if (await musicaDAO.BuscarPorIdAsync(id, cancellationToken) is null)
        {
            return null;
        }

        var musica = new Musica
        {
            ID = id,
            Titulo = request.Titulo,
            ArtistaResponsavel = request.ArtistaResponsavel,
            Duracao = request.Duracao
        };

        await musicaBusiness.AtualizarAsync(musica, cancellationToken);
        return CriarResposta(musica);
    }

    public Task ExcluirAsync(int id, CancellationToken cancellationToken) =>
        musicaBusiness.ExcluirAsync(id, cancellationToken);

    private static MusicaResponse CriarResposta(Musica musica) =>
        new(musica.ID, musica.Titulo, musica.ArtistaResponsavel, musica.Duracao);
}
