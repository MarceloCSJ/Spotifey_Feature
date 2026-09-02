namespace ArtistConnect.Classes;
 
public class MenuMusica
{
    private MusicaServicos musicaServicos = new MusicaServicos();
 
    public void Menu()
    {
        int escolha;
        do
        {
            Console.WriteLine("--- MENU MÚSICA ---");
            Console.WriteLine("1 - Cadastrar Música");
            Console.WriteLine("2 - Listar Músicas");
            Console.WriteLine("3 - Alterar Música");
            Console.WriteLine("4 - Remover Música");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
 
            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("");
                switch (escolha)
                {
                    case 1:
                        Console.WriteLine("[ Cadastrar Música ]");
                        musicaServicos.CadastrarMusica();
                        break;
                    case 2:
                        Console.WriteLine("[ Listar Músicas ]");
                        musicaServicos.ListarMusicas();
                        break;
                    case 3:
                        Console.WriteLine("[ Alterar Música ]");
                        musicaServicos.AlterarMusica();
                        break;
                    case 4:
                        Console.WriteLine("[ Remover Música ]");
                        musicaServicos.RemoverMusica();
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