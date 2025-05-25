using Microsoft.EntityFrameworkCore;
using Music.Application.Songs;
using Music.Domain.Models;
using Music.Infrastructure.SQLite.Configurations;

namespace Music.Infrastructure.SQLite.Repositories;

public class SongRepository : ISongRepository
{
    private readonly MusicDbContext _dbContext;
    public SongRepository(MusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Album> GetByIdAsync(int id)
    {
        var album = await _dbContext.Albums
            .AsNoTracking()
            .Include(album => album.Songs)
            .FirstAsync(x => x.Id == id);
        return album;
    }
}