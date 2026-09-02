namespace ArtistConnect.Classes;

public class Indie : Genero
{
    public override void GeneroSelecionado()
    {
       Lista();
    }
    private void Lista()
    {
        Console.WriteLine("");
        Console.WriteLine("Artista: John Doe");
        Console.WriteLine("Música: Música Exótica");
    }
}