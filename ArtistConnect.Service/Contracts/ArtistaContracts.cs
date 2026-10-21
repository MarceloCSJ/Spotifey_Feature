using System.ComponentModel.DataAnnotations;

namespace ArtistConnect.Service.Contracts;

/// <summary>Dados necessários para cadastrar um artista.</summary>
public sealed record CreateArtistaRequest
{
    [Required, StringLength(100)]
    public required string Nome { get; init; }

    [Required, StringLength(2000)]
    public required string Bio { get; init; }

    [Required, StringLength(50)]
    public required string GeneroMusical { get; init; }
}

/// <summary>Dados necessários para atualizar um artista.</summary>
public sealed record UpdateArtistaRequest
{
    [Required, StringLength(100)]
    public required string Nome { get; init; }

    [Required, StringLength(2000)]
    public required string Bio { get; init; }

    [Required, StringLength(50)]
    public required string GeneroMusical { get; init; }
}

/// <summary>Representação pública de um artista.</summary>
public sealed record ArtistaResponse(int Id, string Nome, string? Bio, string? GeneroMusical);
