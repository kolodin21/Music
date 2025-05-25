using Microsoft.AspNetCore.Mvc;
using Music.Application.Artists;

namespace Music.Controllers;

public class HomeController : Controller
{
    private readonly IArtistsService _artistsService;
    public HomeController(IArtistsService artistsService)
    {
        _artistsService = artistsService;
    }
    public async Task<IActionResult> Index()
    {
        var artists = await _artistsService.GetAll();
        return View(artists);
    }
}
