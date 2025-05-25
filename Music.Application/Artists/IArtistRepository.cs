using Music.Domain.Models;

namespace Music.Application.Artists;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllAsync();
}