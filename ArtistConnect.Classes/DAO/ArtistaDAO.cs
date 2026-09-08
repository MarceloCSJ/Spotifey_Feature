namespace ArtistConnect.Classes.DAO;

public class ArtistaDAO : IArtistaDAO
{
    private readonly List<Artista> _artistas = new();

    public void Adicionar(Artista artista)
    {
        _artistas.Add(artista);
    }

    public List<Artista> ObterTodos()
    {
        return _artistas.ToList();
    }

    public Artista? BuscarPorNome(string nome)
    {
        return _artistas.FirstOrDefault(a => string.Equals(a.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    public void Remover(Artista artista)
    {
        _artistas.Remove(artista);
    }
}
