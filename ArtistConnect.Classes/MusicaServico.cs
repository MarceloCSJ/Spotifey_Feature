using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Classes;

public class MusicaServicos
{
    public void CadastrarMusica()
    {
        Musica musica = new Musica();
 
        Console.Write("Digite o título da música: ");
        musica.Titulo = Console.ReadLine()!;
 
        Console.Write("Digite o nome do artista responsável: ");
        musica.ArtistaResponsavel = Console.ReadLine()!;
 
        Console.Write("Digite a duração da música (ex: 3:45): ");
        musica.Duracao = Console.ReadLine()!;
 
        using ArtistConnectContext context = new();
        context.Musicas.Add(musica);
        context.SaveChanges();
 
        Console.WriteLine($"Música cadastrada com sucesso! Id: {musica.Id}");
    }
 
    public void ListarMusicas()
    {
        using ArtistConnectContext context = new();
        List<Musica> musicas = context.Musicas.AsNoTracking()
            .OrderBy(musica => musica.Titulo)
            .ToList();

        Console.WriteLine("Lista de Músicas Cadastradas:");
        if (musicas.Count == 0)
        {
            Console.WriteLine("Nenhuma música cadastrada.");
            return;
        }
 
        foreach (Musica musica in musicas)
        {
            Console.WriteLine($"Id: {musica.Id}");
            musica.ExibirDados();
        }
    }
 
    public void AlterarMusica()
    {
        int id = LerId("Digite o Id da música: ");
        if (id <= 0) return;

        using ArtistConnectContext context = new();
        Musica? musicaEncontrada = context.Musicas.Find(id);
 
        if (musicaEncontrada == null)
        {
            Console.WriteLine("Música não encontrada.");
            return;
        }
 
        Console.Write($"Digite o novo título para '{musicaEncontrada.Titulo}': ");
        musicaEncontrada.Titulo = Console.ReadLine()?.Trim() ?? string.Empty;
        context.SaveChanges();
 
        Console.WriteLine("Música alterada com sucesso!");
    }
 
    public void RemoverMusica()
    {
        int id = LerId("Digite o Id da música que deseja remover: ");
        if (id <= 0) return;

        using ArtistConnectContext context = new();
        Musica? musicaEncontrada = context.Musicas.Find(id);
 
        if (musicaEncontrada == null)
        {
            Console.WriteLine("Música não encontrada.");
            return;
        }
 
        context.Musicas.Remove(musicaEncontrada);
        context.SaveChanges();
        Console.WriteLine($"A música '{musicaEncontrada.Titulo}' foi removida com sucesso.");
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