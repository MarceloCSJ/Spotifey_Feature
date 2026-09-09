namespace ArtistConnect.Classes;

public sealed class MenuPrincipal(MenuArtista menuArtista, MenuMusica menuMusica, MenuGenero menuGenero)
{
    public async Task ExecutarAsync()
    {
        Console.Clear();
        var escolha = -1;
        while (escolha != 0)
        {
            Console.WriteLine("============================");
            Console.WriteLine("   BEM-VINDO AO ARTIST CONNECT");
            Console.WriteLine("============================");
            Console.WriteLine("1 - Gerenciar Artistas");
            Console.WriteLine("2 - Gerenciar Músicas");
            Console.WriteLine("3 - Ver Gêneros Recomendados");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite a opção desejada: ");

            if (!int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("Por favor, digite um número válido.");
                continue;
            }

            switch (escolha)
            {
                case 1: await menuArtista.ExecutarAsync(); break;
                case 2: await menuMusica.ExecutarAsync(); break;
                case 3: menuGenero.Executar(); break;
                case 0: Console.WriteLine("Fechando o Artist Connect. Até logo!"); break;
                default: Console.WriteLine("Opção inválida. Tente novamente."); break;
            }
            Console.WriteLine();
        }
    }
}