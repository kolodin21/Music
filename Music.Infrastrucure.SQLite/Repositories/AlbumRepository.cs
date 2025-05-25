using Microsoft.EntityFrameworkCore;
using Music.Application.Albums;
using Music.Domain.Models;
using Music.Infrastructure.SQLite.Configurations;

namespace Music.Infrastructure.SQLite.Repositories;

public class AlbumRepository : IAlbumRepository
{
    private readonly MusicDbContext _dbContext;
    public AlbumRepository(MusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Album>> GetAllAsync()
    {
        var albums = await _dbContext.Albums.AsNoTracking().ToListAsync();

        return albums;
    }

    public async Task<Album> GetDetailsByIdAsync(int id)
    {
        var album = await _dbContext.Albums
                            .AsNoTracking()
                            .Include(album => album.Songs)
                            .FirstAsync(x => x.Id == id);

        return album;
    }
}