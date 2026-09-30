using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business.Interfaces;

public interface IMusicaDAO
{
    IReadOnlyList<Musica> Listar();
    Musica? BuscarPorTitulo(string titulo);
    void Inserir(Musica musica);
    void Atualizar(Musica musica);
    void Excluir(Musica musica);
}
