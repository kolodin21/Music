using Microsoft.AspNetCore.Mvc;
using Music.Application.Artists;
using Music.Application.ModelsDto.Artist;
using Music.Views.Artist;

namespace Music.Controllers;

public class ArtistController : Controller
{
    private readonly IArtistsService _artistsService;

    public ArtistController(IArtistsService artistsService)
    {
        _artistsService = artistsService;
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var artist = await _artistsService.GetById(id);

        if (artist.IsSuccess)
            return View(artist.Value);
        TempData["ErrorMessage"] = string.Join(", ", artist.Errors);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var resultId = await _artistsService.Delete(id);
        if (resultId.IsSuccess)
            return RedirectToAction("Index");
        TempData["ErrorMessage"] = string.Join(", ", resultId.Errors);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArtistCreateDto artist)
    {
        var resultId = await _artistsService.Create(artist);

        if (resultId.IsSuccess)
            return RedirectToAction("Details", "Artist", new { id = resultId.Value });

        TempData["ErrorMessage"] = string.Join(", ", resultId.Errors);
        return RedirectToAction("Create");
    }

    [HttpPost]
    public async Task<IActionResult> Update(ArtistReadDto artist)
    {
        var resultArtist = await _artistsService.Update(artist);
        return RedirectToAction("Index");
    }

    //Навигация
    public async Task<IActionResult> Index()
    {
        var artist = await _artistsService.GetAll();

        return View(new ArtistIndexViewModel
        {
            Artists = artist.Value
        });
    }

    public async Task<IActionResult> Update(int id)
    {
        var artist = await _artistsService.GetById(id);
        if (artist.IsSuccess)
            return View(artist.Value);

        TempData["ErrorMessage"] = string.Join(", ", artist.Errors);
        return RedirectToAction("Index");
    }

    public IActionResult Create() => View();

}