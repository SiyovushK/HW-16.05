namespace Domain.Filters;

public class StudentFilter
{
    public string? Name { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}