using Music.Application.ModelsDto.Artist;
using Music.Domain.Models;

namespace Music.Application.Artists
{
    public static class ArtistDtoFactory
    {
        public static ArtistReadDto Create(Artist artist)
        {
            return new ArtistReadDto
            {
                Id = artist.Id,
                Name = artist.Name,
                UrlImg = artist.UrlImg
            };
        }
    }
}
