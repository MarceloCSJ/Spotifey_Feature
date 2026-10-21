using ArtistConnect.Business;
using ArtistConnect.Business.Interfaces;
using ArtistConnect.Model.Entities;
using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;

namespace ArtistConnect.Service.Services;

public sealed class ArtistaService(
    IArtistaDAO artistaDAO,
    ArtistaBusiness artistaBusiness) : IArtistaService
{
    public async Task<IReadOnlyList<ArtistaResponse>> ListarAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Artista> artistas = await artistaDAO.ListarAsync(cancellationToken);
        return artistas.Select(CriarResposta).ToArray();
    }

    public async Task<ArtistaResponse?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        Artista? artista = await artistaDAO.BuscarPorIdAsync(id, cancellationToken);
        return artista is null ? null : CriarResposta(artista);
    }

    public async Task<ArtistaResponse> CadastrarAsync(
        CreateArtistaRequest request,
        CancellationToken cancellationToken)
    {
        var artista = new Artista
        {
            Nome = request.Nome,
            Bio = request.Bio,
            GeneroMusical = request.GeneroMusical
        };

        await artistaBusiness.CadastrarAsync(artista, cancellationToken);
        return CriarResposta(artista);
    }

    public async Task<ArtistaResponse?> AtualizarAsync(
        int id,
        UpdateArtistaRequest request,
        CancellationToken cancellationToken)
    {
        if (await artistaDAO.BuscarPorIdAsync(id, cancellationToken) is null)
        {
            return null;
        }

        var artista = new Artista
        {
            ID = id,
            Nome = request.Nome,
            Bio = request.Bio,
            GeneroMusical = request.GeneroMusical
        };

        await artistaBusiness.AtualizarAsync(artista, cancellationToken);
        return CriarResposta(artista);
    }

    public Task ExcluirAsync(int id, CancellationToken cancellationToken) =>
        artistaBusiness.ExcluirAsync(id, cancellationToken);

    private static ArtistaResponse CriarResposta(Artista artista) =>
        new(artista.ID, artista.Nome, artista.Bio, artista.GeneroMusical);
}
