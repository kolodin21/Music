using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Music.Application.HelperModels;
using Music.Application.ModelsDto.Artist;

namespace Music.Views.Shared.Components
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ArtistCards(this IHtmlHelper htmlHelper, PagedResult<ArtistReadDto> model)
        {
            return htmlHelper.PartialAsync("Components/_ArtistCardsPartial", model).Result;
        }

        public static IHtmlContent ErrorMessage(this IHtmlHelper htmlHelper, string? message)
        {
            return htmlHelper.PartialAsync("Components/_ErrorMessage", message).Result;
        }
    }
}
