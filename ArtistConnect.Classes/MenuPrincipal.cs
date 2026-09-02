namespace ArtistConnect.Classes;
 
public class MenuPrincipal
{
    public void Menu()
    {
        int escolha;
        MenuArtista menuArtista = new MenuArtista();
        MenuMusica menuMusica = new MenuMusica();
        MenuGenero menuGenero = new MenuGenero();
 
        do
        {
            Console.WriteLine("============================");
            Console.WriteLine("   BEM-VINDO AO ARTIST CONNECT    ");
            Console.WriteLine("============================");
            Console.WriteLine("1 - Gerenciar Artistas");
            Console.WriteLine("2 - Gerenciar Músicas");
            Console.WriteLine("3 - Ver Gêneros Recomendados");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("============================");
            Console.Write("Digite a opção desejada: ");
            
            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("");
                switch (escolha)
                {
                    case 1:
                        menuArtista.Menu();
                        break;
                    case 2:
                        menuMusica.Menu();
                        break;
                    case 3:
                        menuGenero.Menu();
                        break;
                    case 0:
                        Console.WriteLine("Fechando o Artist Connect. Até logo!");
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