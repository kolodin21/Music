using Music.Domain.Models;

namespace Music.Application.Albums;

public interface IAlbumsService
{
    Task<List<Album>> GetAll();
    Task<Album> GetDetailsById(int id);
}