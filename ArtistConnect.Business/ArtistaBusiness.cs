using System.ComponentModel.DataAnnotations;
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

    public void Cadastrar(Artista artista)
    {
        Validar(artista);

        if (ExisteNomeDuplicado(artista.Nome))
        {
            throw new ValidationException("Já existe um artista cadastrado com esse nome.");
        }

        _artistaDAO.Inserir(artista);
    }

    public void Atualizar(Artista artista)
    {
        ArgumentNullException.ThrowIfNull(artista);

        if (!_artistaDAO.Listar().Any(item => item.ID == artista.ID))
        {
            throw new ValidationException("Não é possível alterar um artista que não existe.");
        }

        Validar(artista);

        if (ExisteNomeDuplicado(artista.Nome, artista.ID))
        {
            throw new ValidationException("Já existe outro artista cadastrado com esse nome.");
        }

        _artistaDAO.Atualizar(artista);
    }

    public void Excluir(int artistaId)
    {
        Artista? artista = _artistaDAO.Listar().FirstOrDefault(item => item.ID == artistaId);

        if (artista is null)
        {
            throw new ValidationException("Não é possível excluir um artista que não existe.");
        }

        _artistaDAO.Excluir(artista);
    }

    private bool ExisteNomeDuplicado(string nome, int? idIgnorado = null) =>
        _artistaDAO.Listar().Any(item =>
            item.ID != idIgnorado &&
            string.Equals(item.Nome.Trim(), nome.Trim(), StringComparison.OrdinalIgnoreCase));
}