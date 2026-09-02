namespace ArtistConnect.Classes;
 
public class Musica
{
    public string? Titulo { get; set; }
    public string? ArtistaResponsavel { get; set; }
    public string? Duracao { get; set; }
 
    public Musica() { }
 
    public void ExibirDados()
    {
        Console.WriteLine($"Música: {Titulo} | Artista: {ArtistaResponsavel} | Duração: {Duracao}");
    }
}