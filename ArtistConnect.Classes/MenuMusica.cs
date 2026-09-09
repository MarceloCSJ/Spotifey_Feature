using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Classes;

public sealed class MenuMusica
{
    public async Task ExecutarAsync()
    {
        var escolha = -1;
        while (escolha != 0)
        {
            Console.WriteLine("--- MENU MÚSICA ---");
            Console.WriteLine("1 - Cadastrar Música");
            Console.WriteLine("2 - Listar Músicas");
            Console.WriteLine("3 - Alterar Música");
            Console.WriteLine("4 - Remover Música");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("Digite a opção desejada: ");
            if (!int.TryParse(Console.ReadLine(), out escolha)) { Console.WriteLine("Por favor, digite um número válido."); continue; }
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
            catch (DbUpdateException exception) { Console.WriteLine($"Não foi possível salvar a música: {exception.InnerException?.Message ?? exception.Message}"); }
            Console.WriteLine();
        }
    }

    private async Task CadastrarAsync()
    {
        var musica = new Musica { Titulo = Ler("Digite o título da música: "), ArtistaResponsavel = Ler("Digite o nome do artista responsável: "), Duracao = Ler("Digite a duração da música (ex: 3:45): ") };
        await using var context = new ArtistConnectContext();
        context.Musicas.Add(musica);
        await context.SaveChangesAsync();
        Console.WriteLine("Música cadastrada com sucesso!");
    }

    private async Task ListarAsync()
    {
        await using var context = new ArtistConnectContext();
        var musicas = await context.Musicas
            .AsNoTracking()
            .OrderBy(musica => musica.Titulo)
            .ToListAsync();
        Console.WriteLine("Lista de Músicas Cadastradas:");
        if (musicas.Count == 0) { Console.WriteLine("Nenhuma música cadastrada."); return; }
        foreach (var musica in musicas) Console.WriteLine($"Música: {musica.Titulo} | Artista: {musica.ArtistaResponsavel} | Duração: {musica.Duracao}");
    }

    private async Task AlterarAsync()
    {
        var id = LerId("Digite o Id da música: ");
        if (id <= 0) return;

        await using var context = new ArtistConnectContext();
        var musica = await context.Musicas.FindAsync(id);
        if (musica is null) { Console.WriteLine("Música não encontrada."); return; }

        musica.Titulo = Ler($"Digite o novo título para '{musica.Titulo}': ");
        await context.SaveChangesAsync();
        Console.WriteLine("Música alterada com sucesso!");
    }

    private async Task RemoverAsync()
    {
        var id = LerId("Digite o Id da música que deseja remover: ");
        if (id <= 0) return;

        await using var context = new ArtistConnectContext();
        var musica = await context.Musicas.FindAsync(id);
        if (musica is null) { Console.WriteLine("Música não encontrada."); return; }

        context.Musicas.Remove(musica);
        await context.SaveChangesAsync();
        Console.WriteLine($"A música '{musica.Titulo}' foi removida com sucesso.");
    }

    private static int LerId(string mensagem)
    {
        Console.Write(mensagem);
        return int.TryParse(Console.ReadLine(), out var id) && id > 0 ? id : 0;
    }

    private static string Ler(string mensagem) { Console.Write(mensagem); return Console.ReadLine() ?? string.Empty; }
}