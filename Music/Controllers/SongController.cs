using Microsoft.AspNetCore.Mvc;
using Music.Application.Entity.Songs;
using Music.Extensions;
using Music.ViewModels.Song;
using Music.Views.Shared.Components.Pagination;

namespace Music.Controllers;

public class SongController : Controller
{
    private readonly ISongsService _songsService;

    public SongController(ISongsService songsService)
    {
        _songsService = songsService;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
    {
        var songs = await _songsService.GetAll(pageNumber, pageSize);

        if (songs.IsSuccess)
        {
            var model = new SongIndexViewModel
            {
                Songs = songs.Value.Items.ToList(),
                Pagination = songs.Value.ToPagination(this.GetName(), nameof(Index))
            };
            return View(model);
        }
        TempData["ErrorMessage"] = string.Join(", ", songs.Errors);
        return View(null);
    }
}