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
}