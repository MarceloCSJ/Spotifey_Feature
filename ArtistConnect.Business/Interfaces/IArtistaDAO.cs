using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business.Interfaces;

public interface IArtistaDAO
{
    IReadOnlyList<Artista> Listar();
    Artista? BuscarPorNome(string nome);
    void Inserir(Artista artista);
    void Atualizar(Artista artista);
    void Excluir(Artista artista);
    Task<IReadOnlyList<Artista>> ListarAsync(CancellationToken cancellationToken);
    Task<Artista?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Artista?> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken);
    Task InserirAsync(Artista artista, CancellationToken cancellationToken);
    Task AtualizarAsync(Artista artista, CancellationToken cancellationToken);
    Task ExcluirAsync(Artista artista, CancellationToken cancellationToken);
}
