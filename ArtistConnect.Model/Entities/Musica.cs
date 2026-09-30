namespace ArtistConnect.Model.Entities;

public class Musica
{
    public int ID { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? ArtistaResponsavel { get; set; }
    public string? Duracao { get; set; }
}