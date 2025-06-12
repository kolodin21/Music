using Music.Application.Extensions;
using Music.Application.ModelsDto.Artist;

namespace Music.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public required PagedResult<ArtistReadDto> Artists { get; set; }
    }
}
