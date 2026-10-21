namespace ArtistConnect.Classes;
 
public class MenuGenero
{
    public void Menu()
    {
        int escolha;
        do
        {
            Console.WriteLine("--- MENU GÊNEROS ---");
            Console.WriteLine("1 - Gênero: Gospel");
            Console.WriteLine("2 - Gênero: Sertanejo");
            Console.WriteLine("3 - Gênero: Indie");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
           
            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                switch (escolha)
                {
                    case 1:
                        Genero gospel = new Gospel();
                        gospel.GeneroSelecionado();
                        break;
                    case 2:
                        Genero sertanejo = new Sertanejo();
                        sertanejo.GeneroSelecionado();
                        break;
                    case 3:
                        Genero indie = new Indie();
                        indie.GeneroSelecionado();
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