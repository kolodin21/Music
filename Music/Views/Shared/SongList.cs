using Music.Application.ModelsDto.Song;

namespace Music.Views.Shared;

public class SongList
{
    public required List<SongReadDto> Songs { get; set; }
    public required string NameList { get; set; }
}