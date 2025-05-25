using Music.Domain.Models;

namespace Music.Application.Songs;

public interface ISongsService
{
    Task<Album> GetById(int id);
}