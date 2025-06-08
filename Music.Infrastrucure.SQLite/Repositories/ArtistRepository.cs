using Microsoft.EntityFrameworkCore;
using Music.Application.Artists;
using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;
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
    public async Task<QueryResult<List<Artist>>> GetAllAsync()
    {
        try
        {
            var artists = await _dbContext.Artists.AsNoTracking().ToListAsync();

            return QueryResult<List<Artist>>.Success(artists);
        }
        catch (Exception exp)
        {
            return QueryResult<List<Artist>>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<Artist>> GetByIdAsync(int id)
    {
        try
        {
            var resultArtist = await _dbContext.Artists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return resultArtist == null ?
                QueryResult<Artist>.Failure(new[] { "Такой артист не существует" })
                : QueryResult<Artist>.Success(resultArtist);
        }
        catch (Exception exp)
        {
            return QueryResult<Artist>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<int>> CreateAsync(ArtistCreateDto artist)
    {
        try
        {
            var existingArtist = await _dbContext.Artists
                .FirstOrDefaultAsync(x => x.Name == artist.Name);

            if (existingArtist != null)
                return QueryResult<int>.FailureWithValue(existingArtist.Id, new[] { "Артист уже существует" });

            var newArtist = new Artist
            {
                Name = artist.Name,
                UrlImg = artist.UrlImg
            };

            _dbContext.Artists.Add(newArtist);
            await _dbContext.SaveChangesAsync();

            return QueryResult<int>.Success(newArtist.Id);
        }
        catch (Exception exp)
        {
            return QueryResult<int>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<int>> DeleteAsync(int id)
    {
        try
        {
            var resultArtist = await _dbContext.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (resultArtist == null)
                return QueryResult<int>.FailureWithValue(id, new[] { "Такого артиста не существует" });

            _dbContext.Artists.Remove(resultArtist);
            await _dbContext.SaveChangesAsync();
            return QueryResult<int>.Success(id);
        }
        catch (Exception exp)
        {
            return QueryResult<int>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<Artist>> UpdateAsync(ArtistReadDto artist)
    {
        try
        {
            var resultArtist = await _dbContext.Artists.FirstOrDefaultAsync(x => x.Id == artist.Id);

            if (resultArtist == null)
                return QueryResult<Artist>.Failure(new[] { "Такого артиста не существует" });

            // Проверка на уникальность имени (исключая текущего артиста)
            var isNameTaken = await _dbContext.Artists
                .AnyAsync(x => x.Name == artist.Name && x.Id != artist.Id);

            if (isNameTaken)
                return QueryResult<Artist>.Failure(new[] { "Артист с таким именем уже существует" });

            resultArtist.Name = artist.Name;
            resultArtist.UrlImg = artist.UrlImg;

            _dbContext.Update(resultArtist);
            await _dbContext.SaveChangesAsync();
            return QueryResult<Artist>.Success(resultArtist);

        }
        catch (Exception exp)
        {
            return QueryResult<Artist>.Failure(new[] { exp.Message });
        }
    }
}