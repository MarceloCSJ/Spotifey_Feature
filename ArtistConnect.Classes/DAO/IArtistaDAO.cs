namespace ArtistConnect.Classes.DAO;

public interface IArtistaDAO
{
    void Adicionar(Artista artista);
    List<Artista> ObterTodos();
    Artista? BuscarPorNome(string nome);
    void Remover(Artista artista);
}
