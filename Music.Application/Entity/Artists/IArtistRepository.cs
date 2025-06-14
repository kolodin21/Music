using Music.Application.HelperModels;
using Music.Application.ModelsDto.Artist;

namespace Music.Application.Entity.Artists;

public interface IArtistRepository
{
    Task<QueryResult<PagedResult<ArtistReadDto>>> GetAllAsync(int pageNumber, int pageSize);
    Task<QueryResult<ArtistReadDto>> GetByIdAsync(int id);
    Task<QueryResult<PagedResult<ArtistReadDto>>> FindArtistAsync(string name, int pageNumber, int pageSize);
    Task<QueryResult<int>> CreateAsync(ArtistCreateUpdateDto artist);
    Task<QueryResult<ArtistReadDto>> UpdateAsync(ArtistReadDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
}