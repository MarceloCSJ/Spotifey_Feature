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
}