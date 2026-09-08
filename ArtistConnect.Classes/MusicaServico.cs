using ArtistConnect.Classes.DAO;

namespace ArtistConnect.Classes;
 
public class MusicaServicos
{
    private readonly MusicaDAO _musicaDAO = new();
 
    public void CadastrarMusica()
    {
        Musica musica = new Musica();
 
        Console.Write("Digite o título da música: ");
        musica.Titulo = Console.ReadLine()!;
 
        Console.Write("Digite o nome do artista responsável: ");
        musica.ArtistaResponsavel = Console.ReadLine()!;
 
        Console.Write("Digite a duração da música (ex: 3:45): ");
        musica.Duracao = Console.ReadLine()!;
 
        _musicaDAO.Adicionar(musica);
 
        Console.WriteLine("Música cadastrada com sucesso!");
    }
 
    public List<Musica> ObterMusicas()
    {
        return _musicaDAO.ObterTodos();
    }

    public void ListarMusicas()
    {
        Console.WriteLine("Lista de Músicas Cadastradas:");
        var musicas = ObterMusicas();
        if (musicas.Count == 0)
        {
            Console.WriteLine("Nenhuma música cadastrada.");
            return;
        }
 
        foreach (Musica musica in musicas)
        {
            musica.ExibirDados();
        }
    }
 
    public void AlterarMusica()
    {
        Console.Write("Digite o título da música que deseja alterar: ");
        string tituloBusca = Console.ReadLine()!;
 
        Musica? musicaEncontrada = _musicaDAO.BuscarPorTitulo(tituloBusca);
 
        if (musicaEncontrada == null)
        {
            Console.WriteLine("Música não encontrada.");
            return;
        }
 
        Console.Write($"Digite o novo título para '{tituloBusca}': ");
        musicaEncontrada.Titulo = Console.ReadLine()!;
 
        Console.WriteLine("Música alterada com sucesso!");
    }
 
    public void RemoverMusica()
    {
        Console.Write("Digite o título da música que deseja remover: ");
        string tituloBusca = Console.ReadLine()!;
 
        Musica? musicaEncontrada = _musicaDAO.BuscarPorTitulo(tituloBusca);
 
        if (musicaEncontrada == null)
        {
            Console.WriteLine("Música não encontrada.");
            return;
        }
 
        Console.WriteLine($"A música '{musicaEncontrada.Titulo}' foi removida com sucesso.");
        _musicaDAO.Remover(musicaEncontrada);
    }
}