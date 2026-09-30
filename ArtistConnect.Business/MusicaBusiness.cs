using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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

        if (!_artistaDAO.Listar().Any(artista =>
                string.Equals(artista.Nome.Trim(), musica.ArtistaResponsavel.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            throw new ValidationException("O artista informado não está cadastrado.");
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

    public void Cadastrar(Musica musica)
    {
        Validar(musica);

        if (ExisteTituloDuplicado(musica.Titulo))
        {
            throw new ValidationException("Já existe uma música cadastrada com esse título.");
        }

        _musicaDAO.Inserir(musica);
    }

    public void Atualizar(Musica musica)
    {
        ArgumentNullException.ThrowIfNull(musica);

        if (!_musicaDAO.Listar().Any(item => item.ID == musica.ID))
        {
            throw new ValidationException("Não é possível alterar uma música que não existe.");
        }

        Validar(musica);

        if (ExisteTituloDuplicado(musica.Titulo, musica.ID))
        {
            throw new ValidationException("Já existe outra música cadastrada com esse título.");
        }

        _musicaDAO.Atualizar(musica);
    }

    public void Excluir(int musicaId)
    {
        Musica? musica = _musicaDAO.Listar().FirstOrDefault(item => item.ID == musicaId);

        if (musica is null)
        {
            throw new ValidationException("Não é possível excluir uma música que não existe.");
        }

        _musicaDAO.Excluir(musica);
    }

    private bool ExisteTituloDuplicado(string titulo, int? idIgnorado = null) =>
        _musicaDAO.Listar().Any(item =>
            item.ID != idIgnorado &&
            string.Equals(item.Titulo.Trim(), titulo.Trim(), StringComparison.OrdinalIgnoreCase));
} 