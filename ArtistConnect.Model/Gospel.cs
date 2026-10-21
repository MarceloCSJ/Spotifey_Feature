namespace ArtistConnect.Classes;

public class Gospel : Genero
{
    public override void GeneroSelecionado()
    {
        Lista();
    }
    private void Lista()
    {
        Console.WriteLine("");
        Console.WriteLine("Artista: Aline Barros");
        Console.WriteLine("Música: Raridade");
    }
}