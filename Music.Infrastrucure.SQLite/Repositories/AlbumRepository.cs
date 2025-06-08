using Microsoft.EntityFrameworkCore;
using Music.Application.Albums;
using Music.Application.ModelsDto.Album;
using Music.Application.QueryResult;
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

    public async Task<QueryResult<List<Album>>> GetAllAsync()
    {
        try
        {
            var albums = await _dbContext.Albums.AsNoTracking().ToListAsync();
            return QueryResult<List<Album>>.Success(albums);
        }
        catch (Exception exp)
        {
            return QueryResult<List<Album>>.Failure(new[] { exp.Message });
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

            _dbContext.Update(resultAlbum);
            await _dbContext.SaveChangesAsync();
            return QueryResult<Album>.Success(resultAlbum);

        }
        catch (Exception exp)
        {
            return QueryResult<Album>.Failure(new[] { exp.Message });
        }
    }

}