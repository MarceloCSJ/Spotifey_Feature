using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;

namespace ArtistConnect.Classes;
 
public class MusicaServicos(IMusicaService musicaService)
{
    public void CadastrarMusica()
    {
        Console.Write("Digite o título da música: ");
        string titulo = Console.ReadLine() ?? string.Empty;
 
        Console.Write("Digite o nome do artista responsável: ");
        string artista = Console.ReadLine() ?? string.Empty;
 
        Console.Write("Digite a duração da música (ex: 3:45): ");
        string duracao = Console.ReadLine() ?? string.Empty;

        Executar(() =>
        {
            musicaService.CadastrarAsync(
                new CreateMusicaRequest { Titulo = titulo, ArtistaResponsavel = artista, Duracao = duracao },
                CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine("Música cadastrada com sucesso!");
        });
    }
 
    public void ListarMusicas() => Executar(() =>
    {
        IReadOnlyList<MusicaResponse> musicas = Listar();
        Console.WriteLine("Lista de Músicas Cadastradas:");
        if (musicas.Count == 0)
        {
            Console.WriteLine("Nenhuma música cadastrada.");
            return;
        }
 
        foreach (MusicaResponse musica in musicas)
        {
            Console.WriteLine($"Título: {musica.Titulo}");
            Console.WriteLine($"Artista: {musica.ArtistaResponsavel}");
            Console.WriteLine($"Duração: {musica.Duracao}");
            Console.WriteLine("-");
        }
    });
 
    public void AlterarMusica()
    {
        Console.Write("Digite o título da música que deseja alterar: ");
        string tituloBusca = Console.ReadLine() ?? string.Empty;

        Executar(() =>
        {
            MusicaResponse? musica = BuscarPorTitulo(tituloBusca);
            if (musica is null)
            {
                Console.WriteLine("Música não encontrada.");
                return;
            }

            Console.Write($"Digite o novo título para '{musica.Titulo}': ");
            string novoTitulo = Console.ReadLine() ?? string.Empty;

            musicaService.AtualizarAsync(
                musica.Id,
                new UpdateMusicaRequest
                {
                    Titulo = novoTitulo,
                    ArtistaResponsavel = musica.ArtistaResponsavel ?? string.Empty,
                    Duracao = musica.Duracao ?? string.Empty
                },
                CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine("Música alterada com sucesso!");
        });
    }
 
    public void RemoverMusica()
    {
        Console.Write("Digite o título da música que deseja remover: ");
        string tituloBusca = Console.ReadLine() ?? string.Empty;

        Executar(() =>
        {
            MusicaResponse? musica = BuscarPorTitulo(tituloBusca);
            if (musica is null)
            {
                Console.WriteLine("Música não encontrada.");
                return;
            }

            musicaService.ExcluirAsync(musica.Id, CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine($"A música '{musica.Titulo}' foi removida com sucesso.");
        });
    }

    private IReadOnlyList<MusicaResponse> Listar() =>
        musicaService.ListarAsync(CancellationToken.None).GetAwaiter().GetResult();

    private MusicaResponse? BuscarPorTitulo(string titulo) =>
        Listar().FirstOrDefault(m => string.Equals(m.Titulo, titulo.Trim(), StringComparison.OrdinalIgnoreCase));

    private static void Executar(Action acao)
    {
        try
        {
            acao();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}
