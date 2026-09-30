using ArtistConnect.Model.Entities;

namespace ArtistConnect.Business.Interfaces;

public interface IArtistaDAO
{
    IReadOnlyList<Artista> Listar();
    Artista? BuscarPorNome(string nome);
    void Inserir(Artista artista);
    void Atualizar(Artista artista);
    void Excluir(Artista artista);
}
