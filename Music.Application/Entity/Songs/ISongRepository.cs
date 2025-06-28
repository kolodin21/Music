using Music.Application.HelperModels;
using Music.Application.ModelsDto.Song;

namespace Music.Application.Entity.Songs;

public interface ISongRepository
{
    Task<QueryResult<PagedResult<SongReadDto>>> GetAllAsync(int pageNumber, int pageSize);
    Task<QueryResult<SongReadDto>> GetByIdAsync(int id);
}