using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ArtistConnect.WebApi.Endpoints;

public static class MusicaEndpoints
{
    public static RouteGroupBuilder MapMusicaEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/musicas")
            .WithTags("Músicas")
            .RequireAuthorization();

        group.MapGet("/", async Task<Ok<IReadOnlyList<MusicaResponse>>> (
            IMusicaService service,
            CancellationToken cancellationToken) =>
                TypedResults.Ok(await service.ListarAsync(cancellationToken)))
            .WithName("ListarMusicas")
            .WithSummary("Lista músicas")
            .WithDescription("Retorna todas as músicas cadastradas.")
            .Produces<IReadOnlyList<MusicaResponse>>();

        group.MapGet("/{id:int}", async Task<Results<Ok<MusicaResponse>, NotFound>> (
            int id,
            IMusicaService service,
            CancellationToken cancellationToken) =>
        {
            MusicaResponse? musica = await service.BuscarPorIdAsync(id, cancellationToken);
            return musica is null ? TypedResults.NotFound() : TypedResults.Ok(musica);
        })
            .WithName("BuscarMusicaPorId")
            .WithSummary("Busca uma música")
            .WithDescription("Retorna uma música pelo identificador.")
            .Produces<MusicaResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async Task<Created<MusicaResponse>> (
            CreateMusicaRequest request,
            IMusicaService service,
            CancellationToken cancellationToken) =>
        {
            MusicaResponse musica = await service.CadastrarAsync(request, cancellationToken);
            return TypedResults.Created($"/api/musicas/{musica.Id}", musica);
        })
            .WithName("CadastrarMusica")
            .WithSummary("Cadastra uma música")
            .WithDescription("Cria uma música e retorna sua localização.")
            .Produces<MusicaResponse>(StatusCodes.Status201Created);

        group.MapPut("/{id:int}", async Task<Results<Ok<MusicaResponse>, NotFound>> (
            int id,
            UpdateMusicaRequest request,
            IMusicaService service,
            CancellationToken cancellationToken) =>
        {
            MusicaResponse? musica = await service.AtualizarAsync(id, request, cancellationToken);
            return musica is null ? TypedResults.NotFound() : TypedResults.Ok(musica);
        })
            .WithName("AtualizarMusica")
            .WithSummary("Atualiza uma música")
            .WithDescription("Atualiza os dados de uma música existente.")
            .Produces<MusicaResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:int}", async Task<NoContent> (
            int id,
            IMusicaService service,
            CancellationToken cancellationToken) =>
        {
            await service.ExcluirAsync(id, cancellationToken);
            return TypedResults.NoContent();
        })
            .WithName("ExcluirMusica")
            .WithSummary("Exclui uma música")
            .WithDescription("Remove uma música pelo identificador.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }
}
