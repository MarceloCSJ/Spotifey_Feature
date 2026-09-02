namespace ArtistConnect.Classes;
 
public class MenuArtista
{
    private ArtistaServicos artistaServicos = new ArtistaServicos();
 
    public void Menu()
    {
        int escolha;
        do
        {
            Console.WriteLine("--- MENU ARTISTA ---");
            Console.WriteLine("1 - Cadastrar Artista");
            Console.WriteLine("2 - Listar Artistas");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
            
            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("");
                switch (escolha)
                {
                    case 1:
                        Console.WriteLine("[ Cadastrar Artista ]");
                        artistaServicos.CadastrarArtista();
                        break;
                    case 2:
                        Console.WriteLine("[ Listar Artistas ]");
                        artistaServicos.ListarArtista();
                        break;
                    case 3:
                        Console.WriteLine("[ Alterar Bio de Artista ]");
                        artistaServicos.AlterarArtista();
                        break;
                    case 4:
                        Console.WriteLine("[ Remover Artista ]");
                        artistaServicos.RemoverArtista();
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
}