using Music.Application.ModelsDto.Song;

namespace Music.Application.ModelsDto.Album;

public record AlbumCreateDto
{
    public required int ArtistId { get; set; }
    public required string Name { get; init; }
    public required int YearOfIssue { get; init; }
    public required string UrlImg { get; init; }
    public required List<SongCreateUpdate> Songs { get; init; }
}