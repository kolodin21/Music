using Music.Application.Entity.Albums;
using Music.Application.Entity.Songs;
using Music.Application.ModelsDto.Artist;
using Music.Domain.Models;

namespace Music.Application.Entity.Artists
{
    public static class ArtistDtoFactory
    {
        public static ArtistReadDto CreateRead(Artist artist)
        {
            return new ArtistReadDto
            {
                Id = artist.Id,
                Name = artist.Name,
                UrlImg = artist.UrlImg
            };
        }
        public static ArtistReadDetailsDto CreateReadDetails(Artist artist)
        {
            var albums = artist.Albums?.Select(AlbumDtoFactory.CreateRead);
            var songs = artist.Singles?.Select(SongDtoFactory.CreateRead);

            return new ArtistReadDetailsDto
            {
                Id = artist.Id,
                Name = artist.Name,
                UrlImg = artist.UrlImg,
                Albums = albums?.ToList(),
                Songs = songs?.ToList()
            };
        }
    }
}
