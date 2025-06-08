using Music.Application.ModelsDto.Album;
using Music.Application.QueryResult;
using Music.Domain.Models;

namespace Music.Application.Albums;

public class AlbumsService : IAlbumsService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumsService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<QueryResult<int>> Create(AlbumCreateDto artist)
    {
        throw new NotImplementedException();
    }

    public async Task<QueryResult<int>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<QueryResult<List<Album>>> GetAll()
    {
        try
        {
            return await _albumRepository.GetAllAsync();
        }
        catch (Exception exp)
        {
            return QueryResult<List<Album>>.Failure(new[] { "Ошибка получения всех альбомов" });
        }
    }

    public async Task<QueryResult<Album>> GetById(int id)
    {
        try
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id альбома не может быть 0 ");

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
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id альбома не может быть 0 ");
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