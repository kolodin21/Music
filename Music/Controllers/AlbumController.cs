using Microsoft.AspNetCore.Mvc;
using Music.Application.Albums;

namespace Music.Controllers;

public class AlbumController : Controller
{
    private readonly IAlbumsService _albumsService;

    public AlbumController(IAlbumsService albumsService)
    {
        _albumsService = albumsService;
    }

    public async Task<IActionResult> Index()
    {
        var albums = await _albumsService.GetAll();

        return View(albums.Value);
    }

    public async Task<IActionResult> Details(int id)
    {
        var album = await _albumsService.GetDetailsByIdAsync(id);

        return View(album.Value);
    }
}