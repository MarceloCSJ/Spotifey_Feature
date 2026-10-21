namespace ArtistConnect.Classes;

public class Sertanejo : Genero
{
    public override void GeneroSelecionado()
    {
        Lista();
    }
    private void Lista()
    {
        Console.WriteLine("");
        Console.WriteLine("Artista: Gusttavo Lima");
        Console.WriteLine("Música: Apelido Carinhoso");
    }
}