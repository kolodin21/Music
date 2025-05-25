using Music.Domain.Models;

namespace Music.Application.Artists;

public interface IArtistsService
{
    Task<List<Artist>> GetAll();
}