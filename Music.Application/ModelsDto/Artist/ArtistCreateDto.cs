namespace Music.Application.ModelsDto.Artist;

public record ArtistCreateDto
{
    public string Name { get; init; } = string.Empty;
    public string UrlImg { get; init; } = string.Empty;
}
