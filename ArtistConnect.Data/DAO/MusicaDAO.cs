using ArtistConnect.Business.Interfaces;
using ArtistConnect.Data.Context;
using ArtistConnect.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtistConnect.Data.DAO;

public class MusicaDAO : IMusicaDAO
{
    private readonly ArtistConnectDbContext _context;

    public MusicaDAO(ArtistConnectDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<Musica> Listar() =>
        _context.Musicas.AsNoTracking().OrderBy(musica => musica.ID).ToList();

    public Musica? BuscarPorTitulo(string titulo) =>
        Listar().FirstOrDefault(musica => musica.Titulo == titulo);

    public void Inserir(Musica musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }

    public void Atualizar(Musica musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();
    }

    public void Excluir(Musica musica)
    {
        _context.Musicas.Remove(musica);
        _context.SaveChanges();
    }

    public async Task<IReadOnlyList<Musica>> ListarAsync(CancellationToken cancellationToken) =>
        await _context.Musicas.AsNoTracking()
            .OrderBy(musica => musica.ID)
            .ToListAsync(cancellationToken);

    public Task<Musica?> BuscarPorIdAsync(int id, CancellationToken cancellationToken) =>
        _context.Musicas.AsNoTracking()
            .FirstOrDefaultAsync(musica => musica.ID == id, cancellationToken);

    public Task<Musica?> BuscarPorTituloAsync(string titulo, CancellationToken cancellationToken) =>
        _context.Musicas.AsNoTracking()
            .FirstOrDefaultAsync(musica => musica.Titulo == titulo, cancellationToken);

    public async Task InserirAsync(Musica musica, CancellationToken cancellationToken)
    {
        await _context.Musicas.AddAsync(musica, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Musica musica, CancellationToken cancellationToken)
    {
        _context.Musicas.Update(musica);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ExcluirAsync(Musica musica, CancellationToken cancellationToken)
    {
        _context.Musicas.Remove(musica);
        await _context.SaveChangesAsync(cancellationToken);
    }
}