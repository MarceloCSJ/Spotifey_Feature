namespace ArtistConnect.Classes.DAO;

public class MusicaDAO : IMusicaDAO
{
    private readonly List<Musica> _musicas = new();

    public void Adicionar(Musica musica)
    {
        _musicas.Add(musica);
    }

    public List<Musica> ObterTodos()
    {
        return _musicas.ToList();
    }

    public Musica? BuscarPorTitulo(string titulo)
    {
        return _musicas.FirstOrDefault(m => string.Equals(m.Titulo, titulo, StringComparison.OrdinalIgnoreCase));
    }

    public void Remover(Musica musica)
    {
        _musicas.Remove(musica);
    }
}
