﻿using Music.Application.Extensions;
using Music.Application.ModelsDto.Album;
using Music.Application.QueryResult;
using Music.Domain.Models;

namespace Music.Application.Albums;

public interface IAlbumsService
{
    Task<QueryResult<PagedResult<Album>>> GetAll(int pageNumber, int pageSize);
    Task<QueryResult<Album>> GetById(int id);
    Task<QueryResult<int>> Create(AlbumCreateDto artist);
    Task<QueryResult<int>> Delete(int id);
    Task<QueryResult<Album>> Update(AlbumReadDto artist);
    Task<QueryResult<Album>> GetDetailsByIdAsync(int id);
}