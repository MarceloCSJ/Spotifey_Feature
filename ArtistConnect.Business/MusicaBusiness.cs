using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ArtistConnect.Business.Exceptions;
using ArtistConnect.Business.Interfaces;
using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business;

public sealed class MusicaBusiness
{
    private static readonly Regex FormatoDuracao = new(@"^\d{1,7}:[0-5]\d$", RegexOptions.Compiled);

    private readonly IMusicaDAO _musicaDAO;
    private readonly IArtistaDAO _artistaDAO;

    public MusicaBusiness(IMusicaDAO musicaDAO, IArtistaDAO artistaDAO)
    {
        _musicaDAO = musicaDAO;
        _artistaDAO = artistaDAO;
    }

    public void Validar(Musica musica)
    {
        ValidarCampos(musica);

        if (!_artistaDAO.Listar().Any(artista =>
                string.Equals(
                    artista.Nome.Trim(),
                    musica.ArtistaResponsavel!.Trim(),
                    StringComparison.OrdinalIgnoreCase)))
        {
            throw new ValidationException("O artista informado não está cadastrado.");
        }
    }

    private static void ValidarCampos(Musica musica)
    {
        ArgumentNullException.ThrowIfNull(musica);

        if (string.IsNullOrWhiteSpace(musica.Titulo))
        {
            throw new ValidationException("O título da música é obrigatório.");
        }

        if (musica.Titulo.Length > 100)
        {
            throw new ValidationException("O título da música deve ter no máximo 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(musica.ArtistaResponsavel))
        {
            throw new ValidationException("O artista responsável é obrigatório.");
        }

        if (musica.ArtistaResponsavel.Length > 100)
        {
            throw new ValidationException("O artista responsável deve ter no máximo 100 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(musica.Duracao))
        {
            throw new ValidationException("A duração da música é obrigatória.");
        }

        if (musica.Duracao.Length > 10)
        {
            throw new ValidationException("A duração deve ter no máximo 10 caracteres.");
        }

        if (!FormatoDuracao.IsMatch(musica.Duracao))
        {
            throw new ValidationException("A duração deve estar no formato minutos:segundos, por exemplo 03:53.");
        }
    }

    public async Task ValidarAsync(Musica musica, CancellationToken cancellationToken)
    {
        ValidarCampos(musica);

        IReadOnlyList<Artista> artistas = await _artistaDAO.ListarAsync(cancellationToken);
        if (!artistas.Any(artista =>
                string.Equals(
                    artista.Nome.Trim(),
                    musica.ArtistaResponsavel!.Trim(),
                    StringComparison.OrdinalIgnoreCase)))
        {
            throw new ValidationException("O artista informado não está cadastrado.");
        }
    }

    public async Task CadastrarAsync(Musica musica, CancellationToken cancellationToken)
    {
        await ValidarAsync(musica, cancellationToken);

        if (await ExisteTituloDuplicadoAsync(musica.Titulo, null, cancellationToken))
        {
            throw new DuplicateEntityException("Já existe uma música cadastrada com esse título.");
        }

        await _musicaDAO.InserirAsync(musica, cancellationToken);
    }

    public async Task AtualizarAsync(Musica musica, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(musica);

        if (await _musicaDAO.BuscarPorIdAsync(musica.ID, cancellationToken) is null)
        {
            throw new KeyNotFoundException("A música solicitada não foi encontrada.");
        }

        await ValidarAsync(musica, cancellationToken);

        if (await ExisteTituloDuplicadoAsync(musica.Titulo, musica.ID, cancellationToken))
        {
            throw new DuplicateEntityException("Já existe outra música cadastrada com esse título.");
        }

        await _musicaDAO.AtualizarAsync(musica, cancellationToken);
    }

    public async Task ExcluirAsync(int musicaId, CancellationToken cancellationToken)
    {
        Musica? musica = await _musicaDAO.BuscarPorIdAsync(musicaId, cancellationToken);

        if (musica is null)
        {
            throw new KeyNotFoundException("A música solicitada não foi encontrada.");
        }

        await _musicaDAO.ExcluirAsync(musica, cancellationToken);
    }

    private async Task<bool> ExisteTituloDuplicadoAsync(
        string titulo,
        int? idIgnorado,
        CancellationToken cancellationToken) =>
        (await _musicaDAO.ListarAsync(cancellationToken)).Any(item =>
            item.ID != idIgnorado &&
            string.Equals(item.Titulo.Trim(), titulo.Trim(), StringComparison.OrdinalIgnoreCase));
}