namespace ArtistConnect.Model.Entities;

public class ArtistaService
{
    private readonly List<Artista> artistas = new();

    public void CadastrarArtista(Artista artista)
    {
        artistas.Add(artista);
    }

    public IReadOnlyList<Artista> ListarArtistas()
    {
        return artistas.AsReadOnly();
    }

    public bool AlterarBioArtista(string nome, string novaBio)
    {
        Artista? artista = artistas.FirstOrDefault(artista =>
            string.Equals(artista.Nome, nome, StringComparison.OrdinalIgnoreCase));

        if (artista is null)
        {
            return false;
        }

        artista.Bio = novaBio;
        return true;
    }

    public bool RemoverArtista(string nome)
    {
        Artista? artista = artistas.FirstOrDefault(artista =>
            string.Equals(artista.Nome, nome, StringComparison.OrdinalIgnoreCase));

        return artista is not null && artistas.Remove(artista);
    }
}

