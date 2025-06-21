namespace Music.Views.Shared.Components.Pagination;

public class Pagination
{
    public required string ControllerName { get; set; }
    public required string ActionName { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public string? SearchName { get; set; }

}