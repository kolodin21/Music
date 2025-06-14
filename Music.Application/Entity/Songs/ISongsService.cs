using Music.Domain.Models;

namespace Music.Application.Entity.Songs;

public interface ISongsService
{
    Task<Album> GetById(int id);
}