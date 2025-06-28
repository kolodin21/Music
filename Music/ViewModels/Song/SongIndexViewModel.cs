using Music.Application.ModelsDto.Song;
using Music.Views.Shared.Components.Pagination;

namespace Music.ViewModels.Song;

public class SongIndexViewModel
{
    public required List<SongReadDto> Songs { get; set; }
    public required Pagination Pagination { get; set; }
}