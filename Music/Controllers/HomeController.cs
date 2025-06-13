using Microsoft.AspNetCore.Mvc;
using Music.Application.Artists;
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
            Pagination = new Pagination
            {
                ControllerName = this.GetName(),
                ActionName = nameof(Index),
                PageNumber = artists.Value.PageNumber,
                PageSize = artists.Value.PageSize,
                TotalCount = artists.Value.TotalCount,
                TotalPages = artists.Value.TotalPages
            }
        };

        return View(model);
    }
}
