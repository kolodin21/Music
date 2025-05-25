using Music.Domain.Models;

namespace Music.Application.Albums;

public class AlbumsService : IAlbumsService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumsService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<List<Album>> GetAll() =>
        await _albumRepository.GetAllAsync();

    public async Task<Album> GetDetailsById(int id)
        => await _albumRepository.GetDetailsByIdAsync(id);
}