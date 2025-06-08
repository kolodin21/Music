using Music.Domain.Models;

namespace Music.Application.ModelsDto.Album;

public record AlbumReadDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required int YearOfIssue { get; init; }
    public required string UrlImg { get; init; }
    public required List<Song> Songs { get; init; }
}