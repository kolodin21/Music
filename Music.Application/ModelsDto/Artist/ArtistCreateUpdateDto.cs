namespace Music.Application.ModelsDto.Artist;

public record ArtistCreateUpdateDto
{
    public required string Name { get; init; }
    public required string UrlImg { get; init; }
}
