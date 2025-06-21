namespace Music.Domain.Models;

public class Album
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime YearOfIssue { get; set; }
    public required string UrlImg { get; set; }

    // Ссылка на исполнителя 
    public int ArtistId { get; set; }
    public Artist? Artist { get; set; }

    public required List<Song> Songs { get; set; }
}