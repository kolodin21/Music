using Music.Application.ModelsDto.Artist;
using Music.Views.Shared.Components.Pagination;

namespace Music.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public required IEnumerable<ArtistReadDto> Artists { get; set; }
        public required Pagination Pagination { get; set; }
    }
}
