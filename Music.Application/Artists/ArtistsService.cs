using Music.Application.ModelsDto.Artist;
using Music.Application.QueryResult;
using Music.Domain.Models;

namespace Music.Application.Artists
{
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistsService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<QueryResult<List<Artist>>> GetAll()
        {
            try
            {
                return await _artistRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                return QueryResult<List<Artist>>.Failure(new[] { "Ошибка получения всех артистов" });
            }
        }
        public async Task<QueryResult<Artist>> GetById(int id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id не может быть равно 0");

                return await _artistRepository.GetByIdAsync(id);
            }
            catch (Exception exp)
            {
                return QueryResult<Artist>.Failure(new[] { exp.Message });
            }
        }
        public async Task<QueryResult<int>> Create(ArtistCreateDto artist)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(artist.Name))
                    throw new ArgumentNullException(nameof(artist.Name), "Имя исполнителя не может быть пустым");
                if (string.IsNullOrWhiteSpace(artist.UrlImg))
                    throw new ArgumentNullException(nameof(artist.UrlImg), "Отсутствует карточка артиста");

                return await _artistRepository.CreateAsync(artist);
            }
            catch (Exception exp)
            {
                return QueryResult<int>.Failure(new[] { exp.Message });
            }
        }
        public async Task<QueryResult<int>> Delete(int id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id не может быть равно 0");

                return await _artistRepository.DeleteAsync(id);
            }
            catch (Exception exp)
            {
                return QueryResult<int>.Failure(new[] { exp.Message });
            }
        }
        public async Task<QueryResult<Artist>> Update(ArtistReadDto? artist)
        {
            try
            {
                if (artist == null)
                    return QueryResult<Artist>.Failure(new[] { "Ошибка обновления артиста" });

                return await _artistRepository.UpdateAsync(artist);
            }
            catch (Exception exp)
            {
                return QueryResult<Artist>.Failure(new[] { exp.Message });
            }
        }
    }
}
