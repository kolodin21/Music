using Microsoft.EntityFrameworkCore;
using Music.Application.Entity.Albums;
using Music.Application.HelperModels;
using Music.Application.ModelsDto.Album;
using Music.Domain.Models;
using Music.Repository.EfCore.Database;
using Music.Repository.EfCore.Extensions;

namespace Music.Repository.EfCore.Repositories;

public class AlbumRepository : IAlbumRepository
{
    private readonly IMusicDbContext _dbContext;
    public AlbumRepository(IMusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<QueryResult<PagedResult<Album>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            var albumsQuery = await _dbContext.Albums
                .AsNoTracking()
                .ToPagedResultAsync(pageNumber, pageSize);

            return QueryResult<PagedResult<Album>>.Success(albumsQuery);
        }
        catch (Exception exp)
        {
            return QueryResult<PagedResult<Album>>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<Album>> GetDetailsByIdAsync(int id)
    {
        try
        {
            var album = await _dbContext.Albums
                                .AsNoTracking()
                                .Include(album => album.Songs)
                                .FirstAsync(x => x.Id == id);

            return QueryResult<Album>.Success(album);

        }
        catch (Exception exp)
        {
            return QueryResult<Album>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<Album>> GetByIdAsync(int id)
    {
        try
        {
            var resultAlbum = await _dbContext.Albums.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return resultAlbum == null ?
                QueryResult<Album>.Failure(new[] { "Такой альбом не существует" })
                : QueryResult<Album>.Success(resultAlbum);
        }
        catch (Exception exp)
        {
            return QueryResult<Album>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<int>> CreateAsync(AlbumCreateDto album)
    {
        try
        {
            var existingAlbum = await _dbContext.Artists
                .FirstOrDefaultAsync(x => x.Name == album.Name);

            if (existingAlbum != null)
                return QueryResult<int>.FailureWithValue(existingAlbum.Id, new[] { "Альбом с таким названием уже существует" });

            var newAlbum = new Album()
            {
                Name = album.Name,
                YearOfIssue = album.YearOfIssue,
                UrlImg = album.UrlImg,
                Songs = album.Songs
            };

            _dbContext.Albums.Add(newAlbum);
            await _dbContext.SaveChangesAsync();

            return QueryResult<int>.Success(newAlbum.Id);
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
            var resultAlbum = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == id);

            if (resultAlbum == null)
                return QueryResult<int>.FailureWithValue(id, new[] { "Такого альбома не существует" });

            _dbContext.Albums.Remove(resultAlbum);
            await _dbContext.SaveChangesAsync();
            return QueryResult<int>.Success(id);
        }
        catch (Exception exp)
        {
            return QueryResult<int>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<Album>> UpdateAsync(AlbumReadDto album)
    {
        try
        {
            var resultAlbum = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == album.Id);

            if (resultAlbum == null)
                return QueryResult<Album>.Failure(new[] { "Такого альбома не существует" });

            // Проверка на уникальность имени (исключая текущего артиста)
            var isNameTaken = await _dbContext.Artists
                .AnyAsync(x => x.Name == album.Name && x.Id != album.Id);

            if (isNameTaken)
                return QueryResult<Album>.Failure(new[] { "Альбом с таким названием уже существует" });

            resultAlbum.Name = album.Name;
            resultAlbum.YearOfIssue = album.YearOfIssue;
            resultAlbum.UrlImg = album.UrlImg;
            resultAlbum.Songs = album.Songs;

            _dbContext.Albums.Update(resultAlbum);
            await _dbContext.SaveChangesAsync();
            return QueryResult<Album>.Success(resultAlbum);

        }
        catch (Exception exp)
        {
            return QueryResult<Album>.Failure(new[] { exp.Message });
        }
    }

}