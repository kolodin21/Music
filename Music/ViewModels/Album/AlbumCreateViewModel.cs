using Music.Application.ModelsDto.Album;
using Music.Application.ModelsDto.Artist;

namespace Music.ViewModels.Album
{
    public class AlbumCreateViewModel
    {
        public required ArtistReadDto Artist { get; set; }
        public AlbumCreateDto Album { get; set; }

    }
}
