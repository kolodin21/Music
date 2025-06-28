using Music.Application.ModelsDto.Album;
using Music.Application.ModelsDto.Song;

namespace Music.Application.ModelsDto.Artist;

public record ArtistReadDetailsDto : ArtistReadDto
{
    public List<AlbumReadDto>? Albums { get; set; }
    public List<SongReadDto>? Songs { get; set; }
}