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

    public async Task<QueryResult<PagedResult<AlbumReadDto>>> GetAllAsync(int pageNumber, int pageSize)
    {
        try
        {
            var albumsQuery = await _dbContext.Albums
                .Include(x => x.Songs)
                .AsNoTracking()
                .ToPagedResultAsync(pageNumber, pageSize);

            var result = albumsQuery.Select(AlbumDtoFactory.Create);

            return QueryResult<PagedResult<AlbumReadDto>>.Success(result);
        }
        catch (Exception exp)
        {
            return QueryResult<PagedResult<AlbumReadDto>>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<AlbumReadDto>> GetDetailsByIdAsync(int id)
    {
        try
        {
            var album = await _dbContext.Albums
                                .AsNoTracking()
                                .Include(x => x.Songs)
                                .FirstAsync(x => x.Id == id);

            var result = AlbumDtoFactory.Create(album);

            return QueryResult<AlbumReadDto>.Success(result);

        }
        catch (Exception exp)
        {
            return QueryResult<AlbumReadDto>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<AlbumReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var resultAlbum = await _dbContext.Albums.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return resultAlbum == null ?
                QueryResult<AlbumReadDto>.Failure(new[] { "Такой альбом не существует" })
                : QueryResult<AlbumReadDto>.Success(AlbumDtoFactory.Create(resultAlbum));
        }
        catch (Exception exp)
        {
            return QueryResult<AlbumReadDto>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<int>> CreateAsync(AlbumCreateDto model)
    {
        await using var transaction = await _dbContext.BeginTransactionAsync();

        try
        {
            // 1. Проверяем существование альбома
            var existingAlbum = await _dbContext.Albums
                .Where(x => x.ArtistId == model.ArtistId &&
                            x.Name.ToLower() == model.Name.ToLower())
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var songErrors = new List<string>();

            foreach (var songDto in model.Songs)
            {
                var existingSong = await _dbContext.Songs
                    .AnyAsync(s => s.Name.ToLower() == songDto.Name.ToLower() && s.ArtistId == model.ArtistId);

                if (existingSong)
                    songErrors.Add($"Песня '{songDto.Name}' уже существует у этого исполнителя");
            }

            if (songErrors.Any())
                return QueryResult<int>.Failure(songErrors);

            // 3. Создаем альбом и песни
            var newAlbum = new Album
            {
                ArtistId = model.ArtistId,
                Name = model.Name,
                YearOfIssue = model.YearOfIssue,
                UrlImg = model.UrlImg,
                Songs = model.Songs.Select(s => new Song
                {
                    ArtistId = model.ArtistId,
                    Name = s.Name,
                    UrlSong = s.Url
                }).ToList()
            };

            _dbContext.Albums.Add(newAlbum);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return QueryResult<int>.Success(newAlbum.Id);
        }
        catch (Exception exp)
        {
            await transaction.RollbackAsync();
            return QueryResult<int>.Failure(new[] { $"Ошибка при создании альбома: {exp.Message}" });
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
            //resultAlbum.Songs = album.Songs;

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