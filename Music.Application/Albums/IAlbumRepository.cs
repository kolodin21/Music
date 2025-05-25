using Music.Domain.Models;

namespace Music.Application.Albums;

public interface IAlbumRepository
{
    Task<List<Album>> GetAllAsync();
    Task<Album> GetDetailsByIdAsync(int id);
}