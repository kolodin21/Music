using Music.Application.Entity.Songs;
using Music.Application.ModelsDto.Album;
using Music.Domain.Models;

namespace Music.Application.Entity.Albums;

public static class AlbumDtoFactory
{
    public static AlbumReadDto CreateRead(Album album)
    {
        var songs = album.Songs.Select(SongDtoFactory.CreateRead);

        return new AlbumReadDto
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Name = album.Name,
            ArtistName = album.Artist!.Name,
            YearOfIssue = album.YearOfIssue.Year,
            UrlImg = album.UrlImg,
            Songs = songs.ToList()
        };
    }
}