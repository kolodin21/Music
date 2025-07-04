﻿using Microsoft.AspNetCore.Mvc;
using Music.Application.Entity.Albums;
using Music.Application.Entity.Artists;
using Music.Application.ModelsDto.Album;
using Music.Extensions;
using Music.ViewModels.Album;
using Music.Views.Shared.Components.Pagination;

namespace Music.Controllers;

public class AlbumController : Controller
{
    private readonly IAlbumsService _albumsService;
    private readonly IArtistsService _artistsService;

    public AlbumController(IAlbumsService albumsService, IArtistsService artistsService)
    {
        _albumsService = albumsService;
        _artistsService = artistsService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
    {
        var albums = await _albumsService.GetAll(pageNumber, pageSize);

        if (albums.IsSuccess)
        {
            var model = new AlbumIndexViewModel
            {
                Albums = albums.Value.Items,
                Pagination = albums.Value.ToPagination(this.GetName(), nameof(Index))
            };

            return View(model);
        }

        TempData["ErrorMessage"] = string.Join(", ", albums.Errors);
        return View(null);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var album = await _albumsService.GetDetailsByIdAsync(id);

        if (album.IsSuccess)
            return View(album.Value);
        TempData["ErrorMessage"] = string.Join(", ", album.Errors);
        return RedirectToAction(nameof(Index));

    }

    [HttpPost]
    public async Task<IActionResult> Create(AlbumCreateDto album)
    {
        var resultId = await _albumsService.Create(album);

        if (resultId.IsSuccess)
            return RedirectToAction(nameof(Index));

        TempData["ErrorMessage"] = string.Join(", ", resultId.Errors);
        return RedirectToAction(nameof(Create));
    }

    //Навигация
    public IActionResult Create(int id)
    {
        var artist = _artistsService.GetById(id).Result.Value;

        var model = new AlbumCreateViewModel
        {
            Artist = artist
        };

        return View(model);
    }
}