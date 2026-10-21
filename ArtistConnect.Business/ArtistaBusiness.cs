using System.ComponentModel.DataAnnotations;
using ArtistConnect.Business.Exceptions;
using ArtistConnect.Business.Interfaces;
using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business;

public sealed class ArtistaBusiness
{
    private readonly IArtistaDAO _artistaDAO;

    public ArtistaBusiness(IArtistaDAO artistaDAO)
    {
        _artistaDAO = artistaDAO;
    }

    public void Validar(Artista artista)
    {
        ArgumentNullException.ThrowIfNull(artista);

        if (string.IsNullOrWhiteSpace(artista.Nome))
        {
            throw new ValidationException("O nome do artista é obrigatório.");
        }

        if (artista.Nome.Length > 100)
        {
            throw new ValidationException("O nome do artista deve ter no máximo 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(artista.Bio))
        {
            throw new ValidationException("A biografia do artista é obrigatória.");
        }

        if (artista.Bio.Length > 2000)
        {
            throw new ValidationException("A biografia do artista deve ter no máximo 2000 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(artista.GeneroMusical))
        {
            throw new ValidationException("O gênero musical do artista é obrigatório.");
        }

        if (artista.GeneroMusical?.Length > 50)
        {
            throw new ValidationException("O gênero musical deve ter no máximo 50 caracteres.");
        }
    }

    public async Task CadastrarAsync(Artista artista, CancellationToken cancellationToken)
    {
        Validar(artista);

        if (await ExisteNomeDuplicadoAsync(artista.Nome, null, cancellationToken))
        {
            throw new DuplicateEntityException("Já existe um artista cadastrado com esse nome.");
        }

        await _artistaDAO.InserirAsync(artista, cancellationToken);
    }

    public async Task AtualizarAsync(Artista artista, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(artista);

        if (await _artistaDAO.BuscarPorIdAsync(artista.ID, cancellationToken) is null)
        {
            throw new KeyNotFoundException("O artista solicitado não foi encontrado.");
        }

        Validar(artista);

        if (await ExisteNomeDuplicadoAsync(artista.Nome, artista.ID, cancellationToken))
        {
            throw new DuplicateEntityException("Já existe outro artista cadastrado com esse nome.");
        }

        await _artistaDAO.AtualizarAsync(artista, cancellationToken);
    }

    public async Task ExcluirAsync(int artistaId, CancellationToken cancellationToken)
    {
        Artista? artista = await _artistaDAO.BuscarPorIdAsync(artistaId, cancellationToken);

        if (artista is null)
        {
            throw new KeyNotFoundException("O artista solicitado não foi encontrado.");
        }

        await _artistaDAO.ExcluirAsync(artista, cancellationToken);
    }

    private async Task<bool> ExisteNomeDuplicadoAsync(
        string nome,
        int? idIgnorado,
        CancellationToken cancellationToken) =>
        (await _artistaDAO.ListarAsync(cancellationToken)).Any(item =>
            item.ID != idIgnorado &&
            string.Equals(item.Nome.Trim(), nome.Trim(), StringComparison.OrdinalIgnoreCase));
}