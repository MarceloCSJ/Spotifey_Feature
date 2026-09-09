using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Classes;

public class ArtistaServicos
{
    public void CadastrarArtista()
    {
        Artista artista = new Artista();
 
        Console.Write("Cadastre seu nome artistico: ");
        artista.Nome = Console.ReadLine()!;
 
        Console.Write("Digite sua bio: ");
        artista.Bio = Console.ReadLine()!;
 
        Console.Write("Digite seu gênero musical: ");
        artista.GeneroMusical = Console.ReadLine()!;
        
        using ArtistConnectContext context = new();
        context.Artistas.Add(artista);
        context.SaveChanges();
 
        Console.WriteLine($"Artista cadastrado com sucesso! Id: {artista.Id}");
    }
 
    public void ListarArtista()
    {
        using ArtistConnectContext context = new();
        List<Artista> artistas = context.Artistas.AsNoTracking()
            .OrderBy(artista => artista.Nome)
            .ToList();

        Console.WriteLine("Lista de Artistas Cadastrados:");
        if (artistas.Count == 0)
        {
            Console.WriteLine("Nenhum artista cadastrado.");
            return;
        }
 
        foreach (Artista artista in artistas)
        {
            Console.WriteLine($"Id: {artista.Id}");
            artista.ExibirDados();
            Console.WriteLine("-");
        }
    }
   
    public void AlterarArtista()
    {
        int id = LerId("Digite o Id do artista: ");
        if (id <= 0) return;

        using ArtistConnectContext context = new();
        Artista? artistaEncontrado = context.Artistas.Find(id);
        if (artistaEncontrado == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }
        Console.Write($"Digite a nova bio de {artistaEncontrado.Nome}: ");
        artistaEncontrado.Bio = Console.ReadLine()?.Trim();
        context.SaveChanges();
 
        Console.WriteLine("Bio alterada com sucesso!");
    }
 
    public void RemoverArtista()
    {
        int id = LerId("Digite o Id do artista que deseja remover: ");
        if (id <= 0) return;

        using ArtistConnectContext context = new();
        Artista? artistaEncontrado = context.Artistas.Find(id);
        if (artistaEncontrado == null)
        {
            Console.WriteLine("Artista não encontrado.");
            return;
        }
 
        context.Artistas.Remove(artistaEncontrado);
        context.SaveChanges();
        Console.WriteLine($"O artista {artistaEncontrado.Nome} foi removido com sucesso.");
    }

    private static int LerId(string mensagem)
    {
        Console.Write(mensagem);
        if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
        {
            return id;
        }

        Console.WriteLine("Informe um Id válido.");
        return 0;
    }
}