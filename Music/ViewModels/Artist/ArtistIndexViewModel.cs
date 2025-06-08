using Music.Application.ModelsDto.Artist;

namespace Music.ViewModels.Artist
{
    public class ArtistIndexViewModel
    {
        public required IEnumerable<ArtistReadDto> Artists { get; set; }
    }
}
