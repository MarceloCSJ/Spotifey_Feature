using System.IO;
using ArtistConnect.Classes;
using Xunit;

namespace ArtistConnect.Tests;

public class ArtistConnectTests
{
    [Fact]
    public void ArtistaServicos_CadastrarEListar_DeveManterArtistaEmMemoria()
    {
        var servicos = new ArtistaServicos();
        var input = new StringReader("Aline\nBio Aline\nGospel\n");
        Console.SetIn(input);

        servicos.CadastrarArtista();

        var artistas = servicos.ListarArtistas();

        Assert.Single(artistas);
        Assert.Equal("Aline", artistas[0].Nome);
        Assert.Equal("Bio Aline", artistas[0].Bio);
        Assert.Equal("Gospel", artistas[0].GeneroMusical);
    }

    [Fact]
    public void MusicaServicos_CadastrarEListar_DeveManterMusicaEmMemoria()
    {
        var servicos = new MusicaServicos();
        var input = new StringReader("Raridade\nAline Barros\n4:12\n");
        Console.SetIn(input);

        servicos.CadastrarMusica();

        var musicas = servicos.ObterMusicas();

        Assert.Single(musicas);
        Assert.Equal("Raridade", musicas[0].Titulo);
        Assert.Equal("Aline Barros", musicas[0].ArtistaResponsavel);
        Assert.Equal("4:12", musicas[0].Duracao);
    }
}
