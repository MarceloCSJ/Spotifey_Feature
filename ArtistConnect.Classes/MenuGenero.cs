namespace ArtistConnect.Classes;

public sealed class MenuGenero
{
    public void Executar()
    {
        var escolha = -1;
        while (escolha != 0)
        {
            Console.WriteLine("--- MENU GÊNEROS ---");
            Console.WriteLine("1 - Gênero: Gospel");
            Console.WriteLine("2 - Gênero: Sertanejo");
            Console.WriteLine("3 - Gênero: Indie");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
            if (!int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("Por favor, digite um número válido.");
                continue;
            }
            if (escolha == 0) continue;
            try
            {
                Genero genero = escolha switch
                {
                    1 => new Gospel(),
                    2 => new Sertanejo(),
                    3 => new Indie(),
                    _ => throw new ArgumentException("Opção inválida. Tente novamente.")
                };
                genero.GeneroSelecionado();
            }
            catch (ArgumentException exception) { Console.WriteLine(exception.Message); }
            Console.WriteLine();
        }
    }
}