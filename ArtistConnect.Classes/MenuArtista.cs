using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;

namespace ArtistConnect.Classes;
 
public class MenuArtista(IArtistaService artistaService)
{

    public void Menu()
    {
        int escolha;
        do
        {
            Console.WriteLine("--- MENU ARTISTA ---");
            Console.WriteLine("1 - Cadastrar Artista");
            Console.WriteLine("2 - Listar Artistas");
            Console.WriteLine("3 - Alterar Bio de Artista");
            Console.WriteLine("4 - Remover Artista");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
            
            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("");
                switch (escolha)
                {
                    case 1:
                        Console.WriteLine("[ Cadastrar Artista ]");
                        CadastrarArtista();
                        break;
                    case 2:
                        Console.WriteLine("[ Listar Artistas ]");
                        ListarArtistas();
                        break;
                    case 3:
                        Console.WriteLine("[ Alterar Bio de Artista ]");
                        AlterarBioArtista();
                        break;
                    case 4:
                        Console.WriteLine("[ Remover Artista ]");
                        RemoverArtista();
                        break;
                    case 0:
                        Console.WriteLine("Voltando...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Por favor, digite um número válido.");
                escolha = -1;
            }
            Console.WriteLine("");
        } while (escolha != 0);
    }

    private void CadastrarArtista()
    {
        Console.Write("Cadastre seu nome artístico: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite sua bio: ");
        string bio = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite seu gênero musical: ");
        string genero = Console.ReadLine() ?? string.Empty;

        Executar(() =>
        {
            artistaService.CadastrarAsync(
                new CreateArtistaRequest { Nome = nome, Bio = bio, GeneroMusical = genero },
                CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine("Artista cadastrado com sucesso!");
        });
    }

    private void ListarArtistas() => Executar(() =>
    {
        IReadOnlyList<ArtistaResponse> artistas =
            artistaService.ListarAsync(CancellationToken.None).GetAwaiter().GetResult();
        Console.WriteLine("Lista de Artistas Cadastrados:");

        if (artistas.Count == 0)
        {
            Console.WriteLine("Nenhum artista cadastrado.");
            return;
        }

        foreach (ArtistaResponse artista in artistas)
        {
            Console.WriteLine($"Artista: {artista.Nome}");
            Console.WriteLine($"Bio: {artista.Bio}");
            Console.WriteLine($"Gênero Musical: {artista.GeneroMusical}");
            Console.WriteLine("-");
        }
    });

    private void AlterarBioArtista()
    {
        Console.Write("Digite o nome do artista: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a nova bio: ");
        string novaBio = Console.ReadLine() ?? string.Empty;

        Executar(() =>
        {
            ArtistaResponse? artista = BuscarPorNome(nome);
            if (artista is null)
            {
                Console.WriteLine("Artista não encontrado.");
                return;
            }

            artistaService.AtualizarAsync(
                artista.Id,
                new UpdateArtistaRequest
                {
                    Nome = artista.Nome,
                    Bio = novaBio,
                    GeneroMusical = artista.GeneroMusical ?? string.Empty
                },
                CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine("Bio do artista alterada com sucesso!");
        });
    }

    private void RemoverArtista()
    {
        Console.Write("Digite o nome do artista que deseja remover: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Executar(() =>
        {
            ArtistaResponse? artista = BuscarPorNome(nome);
            if (artista is null)
            {
                Console.WriteLine("Artista não encontrado.");
                return;
            }

            artistaService.ExcluirAsync(artista.Id, CancellationToken.None).GetAwaiter().GetResult();
            Console.WriteLine($"O artista {nome} foi removido com sucesso.");
        });
    }

    private ArtistaResponse? BuscarPorNome(string nome) =>
        artistaService.ListarAsync(CancellationToken.None).GetAwaiter().GetResult()
            .FirstOrDefault(a => string.Equals(a.Nome, nome.Trim(), StringComparison.OrdinalIgnoreCase));

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