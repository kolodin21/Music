using Microsoft.EntityFrameworkCore;
using Music.Application.Artists;
using Music.Application.Extensions;
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
    public async Task<QueryResult<IEnumerable<ArtistReadDto>>> GetAllAsync()
    {
        try
        {
            var artistsQuery = _dbContext.Artists.AsNoTracking();

            var artists = await artistsQuery.ToListAsync();

            var result = artists.Select(ArtistDtoFactory.Create);

            return QueryResult<IEnumerable<ArtistReadDto>>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<IEnumerable<ArtistReadDto>>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<ArtistReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var resultArtist = await _dbContext.Artists.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (resultArtist == null)
                QueryResult<ArtistReadDto>.Failure(new[] { "Такой артист не существует" });

            var result = ArtistDtoFactory.Create(resultArtist);

            return QueryResult<ArtistReadDto>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<ArtistReadDto>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<int>> CreateAsync(ArtistCreateUpdateDto artist)
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
    public async Task<QueryResult<ArtistReadDto>> UpdateAsync(ArtistReadDto artist)
    {
        try
        {
            var resultArtist = await _dbContext.Artists.FirstOrDefaultAsync(x => x.Id == artist.Id);

            if (resultArtist == null)
                return QueryResult<ArtistReadDto>.Failure(new[] { "Такого артиста не существует" });

            // Проверка на уникальность имени (исключая текущего артиста)
            var isNameTaken = await _dbContext.Artists
                .AnyAsync(x => x.Name == artist.Name && x.Id != artist.Id);

            if (isNameTaken)
                return QueryResult<ArtistReadDto>.Failure(new[] { "Артист с таким именем уже существует" });

            resultArtist.Name = artist.Name;
            resultArtist.UrlImg = artist.UrlImg;

            var result = ArtistDtoFactory.Create(resultArtist);
            _dbContext.Update(resultArtist);
            await _dbContext.SaveChangesAsync();
            return QueryResult<ArtistReadDto>.Success(result);

        }
        catch (Exception exp)
        {
            return QueryResult<ArtistReadDto>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<IEnumerable<ArtistReadDto>>> FindArtistAsync(string name)
    {
        try
        {
            var artistsQuery = _dbContext.Artists.AsNoTracking()
                .ApplyIf(!string.IsNullOrEmpty(name), x => x.Name.StartsWith(name));

            var artists = await artistsQuery.ToListAsync();

            var result = artists.Select(ArtistDtoFactory.Create);

            return QueryResult<IEnumerable<ArtistReadDto>>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<IEnumerable<ArtistReadDto>>.Failure(new[] { exp.Message });
        }
    }
}