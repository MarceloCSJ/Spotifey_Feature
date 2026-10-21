using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business.Interfaces;

public interface IMusicaDAO
{
    IReadOnlyList<Musica> Listar();
    Musica? BuscarPorTitulo(string titulo);
    void Inserir(Musica musica);
    void Atualizar(Musica musica);
    void Excluir(Musica musica);
    Task<IReadOnlyList<Musica>> ListarAsync(CancellationToken cancellationToken);
    Task<Musica?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Musica?> BuscarPorTituloAsync(string titulo, CancellationToken cancellationToken);
    Task InserirAsync(Musica musica, CancellationToken cancellationToken);
    Task AtualizarAsync(Musica musica, CancellationToken cancellationToken);
    Task ExcluirAsync(Musica musica, CancellationToken cancellationToken);
}
