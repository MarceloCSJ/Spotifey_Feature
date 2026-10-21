using ArtistConnect.Business.Interfaces;
using ArtistConnect.Data.Context;
using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Data.DAO;

public class ArtistaDAO : IArtistaDAO
{
    private readonly ArtistConnectDbContext _context;

    public ArtistaDAO(ArtistConnectDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Artista> Listar() =>
        _context.Artistas.AsNoTracking().OrderBy(artista => artista.ID).ToList();

    public Artista? BuscarPorNome(string nome) =>
        Listar().FirstOrDefault(artista => artista.Nome == nome);

    public void Inserir(Artista artista)
    {
        _context.Artistas.Add(artista);
        _context.SaveChanges();
    }

    public void Atualizar(Artista artista)
    {
        _context.Artistas.Update(artista);
        _context.SaveChanges();
    }

    public void Excluir(Artista artista)
    {
        _context.Artistas.Remove(artista);
        _context.SaveChanges();
    }

    public async Task<IReadOnlyList<Artista>> ListarAsync(CancellationToken cancellationToken) =>
        await _context.Artistas.AsNoTracking()
            .OrderBy(artista => artista.ID)
            .ToListAsync(cancellationToken);

    public Task<Artista?> BuscarPorIdAsync(int id, CancellationToken cancellationToken) =>
        _context.Artistas.AsNoTracking()
            .FirstOrDefaultAsync(artista => artista.ID == id, cancellationToken);

    public Task<Artista?> BuscarPorNomeAsync(string nome, CancellationToken cancellationToken) =>
        _context.Artistas.AsNoTracking()
            .FirstOrDefaultAsync(artista => artista.Nome == nome, cancellationToken);

    public async Task InserirAsync(Artista artista, CancellationToken cancellationToken)
    {
        await _context.Artistas.AddAsync(artista, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Artista artista, CancellationToken cancellationToken)
    {
        _context.Artistas.Update(artista);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ExcluirAsync(Artista artista, CancellationToken cancellationToken)
    {
        _context.Artistas.Remove(artista);
        await _context.SaveChangesAsync(cancellationToken);
    }
}