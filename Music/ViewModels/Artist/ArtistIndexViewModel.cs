using Music.Views.Shared.Components.SearchPaginationArtist;

namespace Music.ViewModels.Artist
{
    public class ArtistIndexViewModel
    {
        public required SearchPaginationArtist Artist { get; set; }
        public string? SearchName { get; set; }
    }
}