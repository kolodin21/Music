namespace Music.Application.ModelsDto.Song
{
    public record SongCreateUpdate
    {
        public required string Name { get; set; }

        public required string Url { get; set; }
    }
}
