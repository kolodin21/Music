namespace Music.Domain.Models;

public class Song
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string UrlSong { get; set; }

    // Ссылка на исполнителя 
    public required int ArtistId { get; set; }
    public Artist? Artist { get; set; }

    // Ссылка на альбом 
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }
}