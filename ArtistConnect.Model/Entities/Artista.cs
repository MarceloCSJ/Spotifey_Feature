namespace ArtistConnect.Model.Entities;

public class Artista
{
    public int ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? GeneroMusical { get; set; }
}