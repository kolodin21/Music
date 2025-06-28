using Microsoft.EntityFrameworkCore;
using Music.Application.Entity.Artists;
using Music.Application.HelperModels;
using Music.Application.ModelsDto.Artist;
using Music.Domain.Models;
using Music.Repository.EfCore.Database;
using Music.Repository.EfCore.Extensions;

namespace Music.Repository.EfCore.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly IMusicDbContext _dbContext;
    public ArtistRepository(IMusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<QueryResult<PagedResult<ArtistReadDto>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            var resultArtists = await _dbContext.Artists
                .AsNoTracking()
                .ToPagedResultAsync(pageNumber: pageNumber, pageSize: pageSize);

            var result = resultArtists.Select(ArtistDtoFactory.CreateRead);

            return QueryResult<PagedResult<ArtistReadDto>>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<PagedResult<ArtistReadDto>>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<ArtistReadDetailsDto>> GetByIdAsync(int id)
    {
        try
        {
            var resultArtist = await _dbContext.Artists
                .Include(x => x.Albums)
                .ThenInclude(s => s.Songs)
                .Include(x => x.Singles)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

            if (resultArtist == null)
                QueryResult<ArtistReadDetailsDto>.Failure(new[] { "Такой артист не существует" });

            var result = ArtistDtoFactory.CreateReadDetails(resultArtist!);

            return QueryResult<ArtistReadDetailsDto>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<ArtistReadDetailsDto>.Failure(new[] { exp.Message });
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

            var result = ArtistDtoFactory.CreateRead(resultArtist);
            _dbContext.Artists.Update(resultArtist);
            await _dbContext.SaveChangesAsync();
            return QueryResult<ArtistReadDto>.Success(result);

        }
        catch (Exception exp)
        {
            return QueryResult<ArtistReadDto>.Failure(new[] { exp.Message });
        }
    }
    public async Task<QueryResult<PagedResult<ArtistReadDto>>> FindArtistAsync(string name, int pageNumber, int pageSize)
    {
        try
        {
            var resultArtists = await _dbContext.Artists
                .AsNoTracking()
                .ApplyIf(!string.IsNullOrEmpty(name), x => x.Name.StartsWith(name))
                .ToPagedResultAsync(pageNumber, pageSize);

            var result = resultArtists.Select(ArtistDtoFactory.CreateRead);

            return QueryResult<PagedResult<ArtistReadDto>>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<PagedResult<ArtistReadDto>>.Failure(new[] { exp.Message });
        }
    }
}