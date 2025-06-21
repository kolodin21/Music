using Music.Application.HelperModels;
using Music.Application.ModelsDto.Song;
using static Music.Application.HelperModels.Validate;

namespace Music.Application.Entity.Songs
{
    public class SongsService : ISongsService
    {
        private readonly ISongRepository _songRepository;

        public SongsService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<QueryResult<PagedResult<SongReadDto>>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _songRepository.GetAllAsync(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                return QueryResult<PagedResult<SongReadDto>>.Failure(new[] { exp.Message, "Ошибка получения всех песен" });
            }
        }


        public async Task<QueryResult<SongReadDto>> GetById(int id)
        {
            try
            {
                ThrowIfZero(id, "Id не может быть равно 0");
                return await _songRepository.GetByIdAsync(id);
            }
            catch (Exception exp)
            {
                return QueryResult<SongReadDto>.Failure(new[] { exp.Message });
            }
        }
    }
}
