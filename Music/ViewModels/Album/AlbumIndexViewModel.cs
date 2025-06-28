using Music.Application.ModelsDto.Album;
using Music.Views.Shared.Components.Pagination;

namespace Music.ViewModels.Album;

public class AlbumIndexViewModel
{
    public required IEnumerable<AlbumReadDto> Albums { get; set; }
    public required Pagination Pagination { get; set; }
}