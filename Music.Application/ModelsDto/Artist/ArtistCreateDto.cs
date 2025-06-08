namespace Music.Application.ModelsDto.Artist;

public record ArtistCreateDto
{
    public required string Name { get; init; }
    public required string UrlImg { get; init; }
}
