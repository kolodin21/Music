using Music.Application.HelperModels;
using Music.Application.ModelsDto.Album;
using Music.Domain.Models;
using static Music.Application.HelperModels.Validate;

namespace Music.Application.Entity.Albums;

public class AlbumsService : IAlbumsService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumsService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<QueryResult<int>> Create(AlbumCreateDto artist)
    {
        try
        {

            ThrowIfNull(artist.Name, "Название альбома не должно быть пустым");
            ThrowIfNull(artist.UrlImg, "Обложка альбома не должна быть пустая");
            ThrowIfNull(artist.Songs, "Альбом должен содержать хотя бы 1 песню");

            return await _albumRepository.CreateAsync(artist);
        }
        catch (Exception exp)
        {
            return QueryResult<int>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<int>> Delete(int id)
    {
        try
        {
            ThrowIfZero(id, "Id альбома не может быть 0");

            return await _albumRepository.DeleteAsync(id);
        }
        catch (Exception e)
        {
            return QueryResult<int>.Failure(new[] { e.Message });
        }
    }

    public async Task<QueryResult<PagedResult<Album>>> GetAll(int pageNumber, int pageSize)
    {
        try
        {
            return await _albumRepository.GetAllAsync(pageNumber, pageSize);
        }
        catch (Exception exp)
        {
            return QueryResult<PagedResult<Album>>.Failure(new[] { "Ошибка получения всех альбомов" });
        }
    }

    public async Task<QueryResult<Album>> GetById(int id)
    {
        try
        {
            ThrowIfZero(id, "Id альбома не может быть 0");

            return await _albumRepository.GetByIdAsync(id);
        }
        catch (Exception exp)
        {
            return QueryResult<Album>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<Album>> GetDetailsByIdAsync(int id)
    {
        try
        {
            ThrowIfZero(id, "Id альбома не может быть 0");

            return await _albumRepository.GetDetailsByIdAsync(id);
        }
        catch (Exception exp)
        {
            return QueryResult<Album>.Failure(new[] { exp.Message });
        }
    }

    public async Task<QueryResult<Album>> Update(AlbumReadDto artist)
    {
        throw new NotImplementedException();
    }
}