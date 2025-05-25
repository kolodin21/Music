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

        public async Task<List<Artist>> GetAll() =>
            await _artistRepository.GetAllAsync();
    }
}
