using ArtistConnect.Model;
using ArtistConnect.Service;

namespace ArtistConnect.Classes;
 
public class MenuArtista
{
    private readonly ArtistaService artistaService = new();
 
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
        Artista artista = new();

        Console.Write("Cadastre seu nome artístico: ");
        artista.Nome = Console.ReadLine();

        Console.Write("Digite sua bio: ");
        artista.Bio = Console.ReadLine();

        Console.Write("Digite seu gênero musical: ");
        artista.GeneroMusical = Console.ReadLine();

        artistaService.CadastrarArtista(artista);
        Console.WriteLine("Artista cadastrado com sucesso!");
    }

    private void ListarArtistas()
    {
        IReadOnlyList<Artista> artistas = artistaService.ListarArtistas();
        Console.WriteLine("Lista de Artistas Cadastrados:");

        if (artistas.Count == 0)
        {
            Console.WriteLine("Nenhum artista cadastrado.");
            return;
        }

        foreach (Artista artista in artistas)
        {
            Console.WriteLine($"Artista: {artista.Nome}");
            Console.WriteLine($"Bio: {artista.Bio}");
            Console.WriteLine($"Gênero Musical: {artista.GeneroMusical}");
            Console.WriteLine("-");
        }
    }

    private void AlterarBioArtista()
    {
        Console.Write("Digite o nome do artista: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite a nova bio: ");
        string novaBio = Console.ReadLine() ?? string.Empty;

        Console.WriteLine(artistaService.AlterarBioArtista(nome, novaBio)
            ? "Bio do artista alterada com sucesso!"
            : "Artista não encontrado.");
    }

    private void RemoverArtista()
    {
        Console.Write("Digite o nome do artista que deseja remover: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine(artistaService.RemoverArtista(nome)
            ? $"O artista {nome} foi removido com sucesso."
            : "Artista não encontrado.");
    }
}