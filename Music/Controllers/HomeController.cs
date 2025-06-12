using Microsoft.AspNetCore.Mvc;
using Music.Application.Artists;
using Music.ViewModels.Home;

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
            Artists = artists.Value
        };
        return View(model);
    }
}
