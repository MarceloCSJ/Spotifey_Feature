using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Classes;

public sealed class MenuArtista
{
    public async Task ExecutarAsync()
    {
        var escolha = -1;
        while (escolha != 0)
        {
            Console.WriteLine("--- MENU ARTISTA ---");
            Console.WriteLine("1 - Cadastrar Artista");
            Console.WriteLine("2 - Listar Artistas");
            Console.WriteLine("3 - Alterar Bio de Artista");
            Console.WriteLine("4 - Remover Artista");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
            if (!int.TryParse(Console.ReadLine(), out escolha))
            {
                Console.WriteLine("Por favor, digite um número válido.");
                continue;
            }

            try
            {
                switch (escolha)
                {
                    case 1: await CadastrarAsync(); break;
                    case 2: await ListarAsync(); break;
                    case 3: await AlterarAsync(); break;
                    case 4: await RemoverAsync(); break;
                    case 0: Console.WriteLine("Voltando..."); break;
                    default: Console.WriteLine("Opção inválida. Tente novamente."); break;
                }
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine($"Não foi possível salvar o artista: {exception.InnerException?.Message ?? exception.Message}");
            }
            Console.WriteLine();
        }
    }

    private async Task CadastrarAsync()
    {
        var artista = new Artista
        {
            Nome = Ler("Cadastre seu nome artístico: "),
            Bio = Ler("Digite sua bio: "),
            GeneroMusical = Ler("Digite seu gênero musical: ")
        };
        await using var context = new ArtistConnectContext();
        context.Artistas.Add(artista);
        await context.SaveChangesAsync();
        Console.WriteLine("Artista cadastrado com sucesso!");
    }

    private async Task ListarAsync()
    {
        await using var context = new ArtistConnectContext();
        var artistas = await context.Artistas
            .AsNoTracking()
            .OrderBy(artista => artista.Nome)
            .ToListAsync();
        Console.WriteLine("Lista de Artistas Cadastrados:");
        if (artistas.Count == 0) { Console.WriteLine("Nenhum artista cadastrado."); return; }
        foreach (var artista in artistas)
            Console.WriteLine($"Artista: {artista.Nome}\nBio: {artista.Bio}\nGênero Musical: {artista.GeneroMusical}\n-");
    }

    private async Task AlterarAsync()
    {
        var id = LerId("Digite o Id do artista: ");
        if (id <= 0) return;

        await using var context = new ArtistConnectContext();
        var artista = await context.Artistas.FindAsync(id);
        if (artista is null) { Console.WriteLine("Artista não encontrado."); return; }

        artista.Bio = Ler($"Digite a nova bio de {artista.Nome}: ");
        await context.SaveChangesAsync();
        Console.WriteLine("Bio alterada com sucesso!");
    }

    private async Task RemoverAsync()
    {
        var id = LerId("Digite o Id do artista que deseja remover: ");
        if (id <= 0) return;

        await using var context = new ArtistConnectContext();
        var artista = await context.Artistas.FindAsync(id);
        if (artista is null) { Console.WriteLine("Artista não encontrado."); return; }

        context.Artistas.Remove(artista);
        await context.SaveChangesAsync();
        Console.WriteLine($"O artista {artista.Nome} foi removido com sucesso.");
    }

    private static int LerId(string mensagem)
    {
        Console.Write(mensagem);
        return int.TryParse(Console.ReadLine(), out var id) && id > 0 ? id : 0;
    }

    private static string Ler(string mensagem) { Console.Write(mensagem); return Console.ReadLine() ?? string.Empty; }
}