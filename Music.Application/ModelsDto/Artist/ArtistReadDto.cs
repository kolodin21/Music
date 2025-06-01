namespace Music.Application.ModelsDto.Artist
{
    public record ArtistReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlImg { get; set; }
    }
}
