using ArtistConnect.Model;

namespace ArtistConnect.Service;

public class MusicaService
{
    private readonly List<Musica> musicas = new();

    public void CadastrarMusica(Musica musica)
    {
        musicas.Add(musica);
    }

    public IReadOnlyList<Musica> ListarMusicas()
    {
        return musicas.AsReadOnly();
    }

    public bool AlterarMusica(string tituloBusca, string novoTitulo)
    {
        Musica? musicaEncontrada = musicas.FirstOrDefault(
            m => m.Titulo == tituloBusca);

        if (musicaEncontrada == null)
        {
            return false;
        }

        musicaEncontrada.Titulo = novoTitulo;
        return true;
    }

    public bool RemoverMusica(string tituloBusca)
    {
        Musica? musicaEncontrada = musicas.FirstOrDefault(
            m => m.Titulo == tituloBusca);

        if (musicaEncontrada == null)
        {
            return false;
        }

        musicas.Remove(musicaEncontrada);
        return true;
    }
}