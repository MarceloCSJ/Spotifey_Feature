namespace ArtistConnect.Classes;
 
public class ArtistaServicos
{
    private List<Artista> artistas = new List<Artista>();
     
    public void CadastrarArtista()
    {
        Artista artista = new Artista();
 
        Console.Write("Cadastre seu nome artistico: ");
        artista.Nome = Console.ReadLine()!;
 
        Console.Write("Digite sua bio: ");
        artista.Bio = Console.ReadLine()!;
 
        Console.Write("Digite seu gênero musical: ");
        artista.GeneroMusical = Console.ReadLine()!;
        
        artistas.Add(artista);
 
        Console.WriteLine("Artista cadastrado com sucesso!");
    }
 
    public void ListarArtista()
    {
        Console.WriteLine("Lista de Artistas Cadastrados:");
        if (artistas.Count == 0)
        {
            Console.WriteLine("Nenhum artista cadastrado.");
            return;
        }
 
        foreach (Artista artista in artistas)
        {
            artista.ExibirDados();
            Console.WriteLine("-");
        }
    }
   
    public void AlterarArtista()
    {
        Console.Write("Digite o nome do Artista: ");
        string nomeBusca = Console.ReadLine()!;
 
        Artista? artistaEncontrado = null;
 
        foreach (Artista artista in artistas)
        {
            if (artista.Nome == nomeBusca)
            {
                artistaEncontrado = artista;
                break;
            }
        }
        if (artistaEncontrado == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }
        Console.WriteLine("Alteração Bio: ");
        Console.Write($"Digite a nova bio de {nomeBusca}: ");
        artistaEncontrado.Bio = Console.ReadLine()!;
 
        Console.WriteLine($"Bio de {nomeBusca} alterada com sucesso!");
    }
 
    public void RemoverArtista()
    {
        Console.Write("Digite o nome do Artista que deseja remover: ");
        string nomeBusca = Console.ReadLine()!;
 
        Artista? artistaEncontrado = null;
 
        foreach (Artista artista in artistas)
        {
            if (artista.Nome == nomeBusca)
            {
                artistaEncontrado = artista;
                break;
            }
        }
        if (artistaEncontrado == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }
 
        Console.WriteLine($"O artista {artistaEncontrado.Nome} foi removido com sucesso.");
        artistas.Remove(artistaEncontrado);
    }
}