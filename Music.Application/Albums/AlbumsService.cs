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
        throw new NotImplementedException();
    }

    public async Task<QueryResult<Album>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<QueryResult<Album>> Update(AlbumReadDto artist)
    {
        throw new NotImplementedException();
    }
}