using Music.Application.HelperModels;
using Music.Application.ModelsDto.Song;

namespace Music.Application.Entity.Songs;

public interface ISongsService
{
    Task<QueryResult<PagedResult<SongReadDto>>> GetAll(int pageNumber, int pageSize);
    Task<QueryResult<SongReadDto>> GetById(int id);
}