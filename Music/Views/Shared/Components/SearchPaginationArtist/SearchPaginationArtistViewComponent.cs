using Microsoft.AspNetCore.Mvc;
using Music.Application.HelperModels;
using Music.Application.ModelsDto.Artist;

namespace Music.Views.Shared.Components.SearchPaginationArtist
{
    public class SearchPaginationArtistViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(SearchPaginationArtist model)
        {
            return View(model);
        }
    }

    public class SearchPaginationArtist
    {
        public required PagedResult<ArtistReadDto> Artists { get; set; }
        public string? SearchName { get; set; } = string.Empty;

    }
}
