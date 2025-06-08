using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;

namespace Music.Application.Artists;

public interface IArtistRepository
{
    Task<QueryResult<IEnumerable<ArtistReadDto>>> GetAllAsync();
    Task<QueryResult<ArtistReadDto>> GetByIdAsync(int id);
    Task<QueryResult<IEnumerable<ArtistReadDto>>> FindArtistAsync(string title);
    Task<QueryResult<int>> CreateAsync(ArtistCreateUpdateDto artist);
    Task<QueryResult<ArtistReadDto>> UpdateAsync(ArtistReadDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
}