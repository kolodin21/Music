using Music.Application.ModelsDto.Song;
using Music.Domain.Models;

namespace Music.Application.Entity.Songs
{
    public static class SongDtoFactory
    {
        public static SongReadDto CreateRead(Song song)
        {
            return new SongReadDto
            {
                Id = song.Id,
                ArtistId = song.ArtistId,
                UrlSong = song.UrlSong,
                Name = song.Name,
                AlbumId = song.AlbumId
            };
        }
    }
}
