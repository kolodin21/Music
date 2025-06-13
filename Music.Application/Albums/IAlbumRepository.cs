using Music.Application.Extensions;
using Music.Application.ModelsDto.Album;
using Music.Application.QueryResult;
using Music.Domain.Models;

namespace Music.Application.Albums;

public interface IAlbumRepository
{
    Task<QueryResult<PagedResult<Album>>> GetAllAsync(int pageNumber, int pageSize);
    Task<QueryResult<Album>> GetByIdAsync(int id);
    Task<QueryResult<int>> CreateAsync(AlbumCreateDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
    Task<QueryResult<Album>> UpdateAsync(AlbumReadDto artist);
    Task<QueryResult<Album>> GetDetailsByIdAsync(int id);

}