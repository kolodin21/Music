using Microsoft.AspNetCore.Mvc;
using Music.Application.Entity.Artists;
using Music.Application.ModelsDto.Artist;
using Music.ViewModels.Artist;
using Music.Views.Shared.Components.SearchPaginationArtist;

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
        {
            var model = new ArtistDetailsViewModel
            {
                Artist = artist.Value
            };
            return View(model);
        }

        TempData["ErrorMessage"] = string.Join(", ", artist.Errors);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var resultId = await _artistsService.Delete(id);
        if (resultId.IsSuccess)
        {
            return RedirectToAction("Index");
        }
        TempData["ErrorMessage"] = string.Join(", ", resultId.Errors);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArtistCreateUpdateDto artist)
    {
        var resultId = await _artistsService.Create(artist);

        if (resultId.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

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
    [HttpGet]
    public async Task<IActionResult> Index(
        string? searchName = null,
        int pageNumber = 1,
        int pageSize = 3,
        bool isAjax = false)
    {
        var result = string.IsNullOrWhiteSpace(searchName)
            ? await _artistsService.GetAll(pageNumber, pageSize)
            : await _artistsService.FindArtist(searchName, pageNumber, pageSize);

        if (!result.IsSuccess)
        {
            return isAjax
                ? Content("<p class='text-danger'>Ошибка при загрузке данных</p>")
                : RedirectToAction(nameof(Index));
        }

        var searchModel = new SearchPaginationArtist
        {
            Artists = result.Value,
            SearchName = searchName,
        };

        if (isAjax)
        {
            return ViewComponent("SearchPaginationArtist", searchModel);
        }

        var viewModel = new ArtistIndexViewModel
        {
            Artist = searchModel,
            SearchName = searchName,
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Update(int id)
    {
        var artist = await _artistsService.GetById(id);
        if (artist.IsSuccess)
            return View(artist.Value);

        TempData["ErrorMessage"] = string.Join(", ", artist.Errors);
        return RedirectToAction(nameof(Index));
    }

    //Навигация
    public IActionResult Create() => View();

}