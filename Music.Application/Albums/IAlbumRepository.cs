using Music.Application.ModelsDto.Album;
using Music.Application.QueryResult;
using Music.Domain.Models;

namespace Music.Application.Albums;

public interface IAlbumRepository
{
    Task<QueryResult<List<Album>>> GetAllAsync();
    Task<QueryResult<Album>> GetByIdAsync(int id);
    Task<QueryResult<int>> CreateAsync(AlbumCreateDto artist);
    Task<QueryResult<int>> DeleteAsync(int id);
    Task<QueryResult<Album>> UpdateAsync(AlbumReadDto artist);
    Task<Album> GetDetailsByIdAsync(int id);
}