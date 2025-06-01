using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;
using Music.Domain.Models;
namespace Music.Application.Artists;

public interface IArtistsService
{
    Task<QueryResult<List<Artist>>> GetAll();
    Task<QueryResult<Artist>> GetById(int id);
    Task<QueryResult<int>> Create(ArtistCreateDto artist);
    Task<QueryResult<int>> Delete(int id);
    Task<QueryResult<Artist>> Update(ArtistReadDto artist);

}