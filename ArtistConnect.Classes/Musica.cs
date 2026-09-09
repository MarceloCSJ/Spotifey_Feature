namespace ArtistConnect.Classes;
 
public class Musica
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? ArtistaResponsavel { get; set; }
    public string? Duracao { get; set; }
 
    public Musica() { }
 
    public void ExibirDados()
    {
        Console.WriteLine($"Música: {Titulo} | Artista: {ArtistaResponsavel} | Duração: {Duracao}");
    }
}