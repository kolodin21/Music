using Microsoft.EntityFrameworkCore;
using Music.Application.Entity.Songs;
using Music.Domain.Models;
using Music.Repository.EfCore.Database;

namespace Music.Repository.EfCore.Repositories;

public class SongRepository : ISongRepository
{
    private readonly IMusicDbContext _dbContext;
    public SongRepository(IMusicDbContext dbContext)
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