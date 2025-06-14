using Microsoft.AspNetCore.Mvc;
using Music.Application.Entity.Songs;

namespace Music.Controllers;

public class SongController : Controller
{
    private readonly ISongsService _songsService;

    public SongController(ISongsService songsService)
    {
        _songsService = songsService;
    }

    public async Task<IActionResult> Index(int id)
    {
        var album = await _songsService.GetById(id);
        return View(album);
    }
}