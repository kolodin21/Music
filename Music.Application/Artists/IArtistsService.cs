using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;
namespace Music.Application.Artists;

public interface IArtistsService
{
    Task<QueryResult<IEnumerable<ArtistReadDto>>> GetAll();
    Task<QueryResult<ArtistReadDto>> GetById(int id);
    Task<QueryResult<IEnumerable<ArtistReadDto>>> FindArtist(string title);
    Task<QueryResult<int>> Create(ArtistCreateUpdateDto artist);
    Task<QueryResult<int>> Delete(int id);
    Task<QueryResult<ArtistReadDto>> Update(ArtistReadDto artist);
}