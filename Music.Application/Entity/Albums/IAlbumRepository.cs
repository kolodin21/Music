using Music.Application.HelperModels;
using Music.Application.ModelsDto.Album;
using Music.Domain.Models;

namespace Music.Application.Entity.Albums;

public interface IAlbumRepository
{
    Task<QueryResult<PagedResult<AlbumReadDto>>> GetAllAsync(int pageNumber, int pageSize);
    Task<QueryResult<AlbumReadDto>> GetByIdAsync(int id);
    Task<QueryResult<int>> CreateAsync(AlbumCreateDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
    Task<QueryResult<Album>> UpdateAsync(AlbumReadDto artist);
    Task<QueryResult<AlbumReadDto>> GetDetailsByIdAsync(int id);

}