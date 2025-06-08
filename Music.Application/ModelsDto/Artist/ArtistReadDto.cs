namespace Music.Application.ModelsDto.Artist
{
    public record ArtistReadDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string UrlImg { get; init; }
    }
}
