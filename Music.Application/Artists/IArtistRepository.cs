using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;
using Music.Domain.Models;

namespace Music.Application.Artists;

public interface IArtistRepository
{
    Task<QueryResult<List<Artist>>> GetAllAsync();
    Task<QueryResult<Artist>> GetByIdAsync(int id);
    Task<QueryResult<int>> CreateAsync(ArtistCreateDto artist);
    Task<QueryResult<Artist>> UpdateAsync(ArtistReadDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
}