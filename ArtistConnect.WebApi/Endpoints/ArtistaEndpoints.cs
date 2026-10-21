using ArtistConnect.Service.Contracts;
using ArtistConnect.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ArtistConnect.WebApi.Endpoints;

public static class ArtistaEndpoints
{
    public static RouteGroupBuilder MapArtistaEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/artistas")
            .WithTags("Artistas")
            .RequireAuthorization();

        group.MapGet("/", async Task<Ok<IReadOnlyList<ArtistaResponse>>> (
            IArtistaService service,
            CancellationToken cancellationToken) =>
                TypedResults.Ok(await service.ListarAsync(cancellationToken)))
            .WithName("ListarArtistas")
            .WithSummary("Lista artistas")
            .WithDescription("Retorna todos os artistas cadastrados.")
            .Produces<IReadOnlyList<ArtistaResponse>>();

        group.MapGet("/{id:int}", async Task<Results<Ok<ArtistaResponse>, NotFound>> (
            int id,
            IArtistaService service,
            CancellationToken cancellationToken) =>
        {
            ArtistaResponse? artista = await service.BuscarPorIdAsync(id, cancellationToken);
            return artista is null ? TypedResults.NotFound() : TypedResults.Ok(artista);
        })
            .WithName("BuscarArtistaPorId")
            .WithSummary("Busca um artista")
            .WithDescription("Retorna um artista pelo identificador.")
            .Produces<ArtistaResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async Task<Created<ArtistaResponse>> (
            CreateArtistaRequest request,
            IArtistaService service,
            CancellationToken cancellationToken) =>
        {
            ArtistaResponse artista = await service.CadastrarAsync(request, cancellationToken);
            return TypedResults.Created($"/api/artistas/{artista.Id}", artista);
        })
            .WithName("CadastrarArtista")
            .WithSummary("Cadastra um artista")
            .WithDescription("Cria um artista e retorna sua localização.")
            .Produces<ArtistaResponse>(StatusCodes.Status201Created);

        group.MapPut("/{id:int}", async Task<Results<Ok<ArtistaResponse>, NotFound>> (
            int id,
            UpdateArtistaRequest request,
            IArtistaService service,
            CancellationToken cancellationToken) =>
        {
            ArtistaResponse? artista = await service.AtualizarAsync(id, request, cancellationToken);
            return artista is null ? TypedResults.NotFound() : TypedResults.Ok(artista);
        })
            .WithName("AtualizarArtista")
            .WithSummary("Atualiza um artista")
            .WithDescription("Atualiza os dados de um artista existente.")
            .Produces<ArtistaResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:int}", async Task<NoContent> (
            int id,
            IArtistaService service,
            CancellationToken cancellationToken) =>
        {
            await service.ExcluirAsync(id, cancellationToken);
            return TypedResults.NoContent();
        })
            .WithName("ExcluirArtista")
            .WithSummary("Exclui um artista")
            .WithDescription("Remove um artista pelo identificador.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }
}
