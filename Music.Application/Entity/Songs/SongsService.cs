using Music.Domain.Models;

namespace Music.Application.Entity.Songs
{
    public class SongsService : ISongsService
    {
        private readonly ISongRepository _songRepository;

        public SongsService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<Album> GetById(int id) =>
            await _songRepository.GetByIdAsync(id);
    }
}
