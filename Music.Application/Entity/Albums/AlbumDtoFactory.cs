using Music.Application.ModelsDto.Album;
using Music.Application.ModelsDto.Song;
using Music.Domain.Models;

namespace Music.Application.Entity.Albums;

public static class AlbumDtoFactory
{
    public static AlbumReadDto Create(Album album)
    {
        var songs = album.Songs.Select(item => new SongReadDto
        {
            Id = item.Id,
            Name = item.Name,
            UrlSong = item.UrlSong,
            ArtistId = item.ArtistId,
            AlbumId = item.AlbumId
        })
            .ToList();

        return new AlbumReadDto
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Name = album.Name,
            YearOfIssue = album.YearOfIssue,
            UrlImg = album.UrlImg,
            Songs = songs
        };
    }
}