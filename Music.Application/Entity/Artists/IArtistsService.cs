using Music.Application.HelperModels;
using Music.Application.ModelsDto.Artist;
namespace Music.Application.Entity.Artists;

public interface IArtistsService
{
    Task<QueryResult<PagedResult<ArtistReadDto>>> GetAll(int pageNumber, int pageSize);
    Task<QueryResult<ArtistReadDetailsDto>> GetById(int id);
    Task<QueryResult<PagedResult<ArtistReadDto>>> FindArtist(string name, int pageNumber, int pageSize);
    Task<QueryResult<int>> Create(ArtistCreateUpdateDto artist);
    Task<QueryResult<int>> Delete(int id);
    Task<QueryResult<ArtistReadDto>> Update(ArtistReadDto artist);
}