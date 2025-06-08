using Microsoft.AspNetCore.Mvc;
using Music.Application.Artists;
using Music.Application.ModelsDto.Artist;
using Music.ViewModels.Artist;

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
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Search(string name)
    {
        var artist = await _artistsService.FindArtist(name);

        if (artist.IsSuccess)
        {
            return PartialView("_ArtistCardsPartial", artist.Value.ToList());

        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var resultId = await _artistsService.Delete(id);
        if (resultId.IsSuccess)
            return RedirectToAction("Index");
        TempData["ErrorMessage"] = string.Join(", ", resultId.Errors);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArtistCreateUpdateDto artist)
    {
        var resultId = await _artistsService.Create(artist);

        if (resultId.IsSuccess)
            return RedirectToAction(nameof(Index));

        TempData["ErrorMessage"] = string.Join(", ", resultId.Errors);
        return RedirectToAction(nameof(Create));
    }

    [HttpPost]
    public async Task<IActionResult> Update(ArtistReadDto artist)
    {
        var resultArtist = await _artistsService.Update(artist);
        return RedirectToAction(nameof(Index));
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
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create() => View();

}