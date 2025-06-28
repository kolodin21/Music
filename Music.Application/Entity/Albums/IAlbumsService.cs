using Music.Application.HelperModels;
using Music.Application.ModelsDto.Album;
using Music.Domain.Models;

namespace Music.Application.Entity.Albums;

public interface IAlbumsService
{
    Task<QueryResult<PagedResult<AlbumReadDto>>> GetAll(int pageNumber, int pageSize);
    Task<QueryResult<AlbumReadDto>> GetById(int id);
    Task<QueryResult<int>> Create(AlbumCreateDto artist);
    Task<QueryResult<int>> Delete(int id);
    Task<QueryResult<Album>> Update(AlbumReadDto artist);
    Task<QueryResult<AlbumReadDto>> GetDetailsByIdAsync(int id);
}