using Music.Domain.Models;

namespace Music.Application.Entity.Songs;

public interface ISongRepository
{
    Task<Album> GetByIdAsync(int id);
}