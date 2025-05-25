using Microsoft.EntityFrameworkCore;
using Music.Application.Artists;
using Music.Domain.Models;
using Music.Infrastructure.SQLite.Configurations;

namespace Music.Infrastructure.SQLite.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly MusicDbContext _dbContext;
    public ArtistRepository(MusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Artist>> GetAllAsync()
    {
        var artists = await _dbContext.Artists.AsNoTracking().ToListAsync();
        return artists;
    }
}