namespace ArtistConnect.Classes.DAO;

public interface IMusicaDAO
{
    void Adicionar(Musica musica);
    List<Musica> ObterTodos();
    Musica? BuscarPorTitulo(string titulo);
    void Remover(Musica musica);
}
