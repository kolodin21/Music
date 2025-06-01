namespace Music.Domain.Models;

public class Artist
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required string UrlImg { get; set; }

    public List<Album>? Albums { get; set; }

    public Artist(string name, string urlImg)
    {
        Name = name;
        UrlImg = urlImg;
    }

    public Artist()
    {
    }
}