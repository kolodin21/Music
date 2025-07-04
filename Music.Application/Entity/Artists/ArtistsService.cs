﻿using Music.Application.HelperModels;
using Music.Application.ModelsDto.Artist;
using static Music.Application.HelperModels.Validate;

namespace Music.Application.Entity.Artists
{
    public class ArtistsService : IArtistsService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistsService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<QueryResult<PagedResult<ArtistReadDto>>> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return await _artistRepository.GetAllAsync(pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                return QueryResult<PagedResult<ArtistReadDto>>.Failure(new[] { exp.Message, "Ошибка получения всех артистов" });
            }
        }
        public async Task<QueryResult<ArtistReadDetailsDto>> GetById(int id)
        {
            try
            {
                ThrowIfZero(id, "Id не может быть равно 0");

                return await _artistRepository.GetByIdAsync(id);
            }
            catch (Exception exp)
            {
                return QueryResult<ArtistReadDetailsDto>.Failure(new[] { exp.Message });
            }
        }
        public async Task<QueryResult<int>> Create(ArtistCreateUpdateDto artist)
        {
            try
            {
                ThrowIfNull(artist.Name, "Имя исполнителя не может быть пустым");
                ThrowIfNull(artist.UrlImg, "Отсутствует карточка артиста");

                return await _artistRepository.CreateAsync(artist);
            }
            catch (Exception exp)
            {
                return QueryResult<int>.Failure(new[] { exp.Message });
            }
        }
        public async Task<QueryResult<int>> Delete(int id)
        {
            try
            {
                if (id == 0)
                    throw new ArgumentNullException(nameof(id), "Id не может быть равно 0");

                return await _artistRepository.DeleteAsync(id);
            }
            catch (Exception exp)
            {
                return QueryResult<int>.Failure(new[] { exp.Message });
            }
        }
        public async Task<QueryResult<ArtistReadDto>> Update(ArtistReadDto? artist)
        {
            try
            {
                if (artist == null)
                    return QueryResult<ArtistReadDto>.Failure(new[] { "Ошибка обновления артиста" });

                return await _artistRepository.UpdateAsync(artist);
            }
            catch (Exception exp)
            {
                return QueryResult<ArtistReadDto>.Failure(new[] { exp.Message });
            }
        }

        public async Task<QueryResult<PagedResult<ArtistReadDto>>> FindArtist(string name, int pageNumber, int pageSize)
        {
            try
            {
                return await _artistRepository.FindArtistAsync(name, pageNumber, pageSize);
            }
            catch (Exception exp)
            {
                return QueryResult<PagedResult<ArtistReadDto>>.Failure(new[] { exp.Message });
            }
        }
    }
}
