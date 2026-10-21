using System.ComponentModel.DataAnnotations;

namespace ArtistConnect.Service.Contracts;

/// <summary>Dados necessários para cadastrar uma música.</summary>
public sealed record CreateMusicaRequest
{
    [Required, StringLength(100)]
    public required string Titulo { get; init; }

    [Required, StringLength(100)]
    public required string ArtistaResponsavel { get; init; }

    [Required, StringLength(10), RegularExpression(@"^\d{1,7}:[0-5]\d$")]
    public required string Duracao { get; init; }
}

/// <summary>Dados necessários para atualizar uma música.</summary>
public sealed record UpdateMusicaRequest
{
    [Required, StringLength(100)]
    public required string Titulo { get; init; }

    [Required, StringLength(100)]
    public required string ArtistaResponsavel { get; init; }

    [Required, StringLength(10), RegularExpression(@"^\d{1,7}:[0-5]\d$")]
    public required string Duracao { get; init; }
}

/// <summary>Representação pública de uma música.</summary>
public sealed record MusicaResponse(int Id, string Titulo, string? ArtistaResponsavel, string? Duracao);
