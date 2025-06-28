using Microsoft.AspNetCore.Mvc;
using Music.Application.Entity.Artists;
using Music.Extensions;
using Music.ViewModels.Home;
using Music.Views.Shared.Components.Pagination;

namespace Music.Controllers;

public class HomeController : Controller
{
    private readonly IArtistsService _artistsService;
    public HomeController(IArtistsService artistsService)
    {
        _artistsService = artistsService;
    }
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
    {
        var artists = await _artistsService.GetAll(pageNumber, pageSize);

        var model = new HomeIndexViewModel
        {
            Artists = artists.Value.Items,
            Pagination = artists.Value.ToPagination(this.GetName(), nameof(Index))
        };

        return View(model);
    }
}
