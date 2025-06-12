using Music.Application.Extensions;
using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;

namespace Music.Application.Artists;

public interface IArtistRepository
{
    Task<QueryResult<PagedResult<ArtistReadDto>>> GetAllAsync(int pageNumber, int pageSize);
    Task<QueryResult<ArtistReadDto>> GetByIdAsync(int id);
    Task<QueryResult<PagedResult<ArtistReadDto>>> FindArtistAsync(string name, int pageNumber, int pageSize);
    Task<QueryResult<int>> CreateAsync(ArtistCreateUpdateDto artist);
    Task<QueryResult<ArtistReadDto>> UpdateAsync(ArtistReadDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
}