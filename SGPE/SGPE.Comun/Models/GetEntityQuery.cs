namespace SGPE.Comun.Models;

public class GetEntityQuery
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchText { get; set; }
    public string? SortProperty { get; set; }
    public string? SortDir { get; set; }
}
