namespace Music.Application.ModelsDto.Song;

public record SongReadDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string UrlSong { get; set; }
    public required int ArtistId { get; set; }
    public int? AlbumId { get; set; }
}