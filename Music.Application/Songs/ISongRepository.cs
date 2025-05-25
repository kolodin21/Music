using Music.Domain.Models;

namespace Music.Application.Songs;

public interface ISongRepository
{
    Task<Album> GetByIdAsync(int id);
}