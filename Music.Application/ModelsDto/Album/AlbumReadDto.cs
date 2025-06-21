using Music.Application.ModelsDto.Song;

namespace Music.Application.ModelsDto.Album;

public record AlbumReadDto
{
    public required int Id { get; set; }
    public required int ArtistId { get; init; }
    public required string Name { get; init; }
    public required string ArtistName { get; init; }
    public required int YearOfIssue { get; init; }
    public required string UrlImg { get; init; }
    public required List<SongReadDto> Songs { get; init; }
}