namespace ERP.SharedKernel.Common;

public class PagedRequest
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? SortBy { get; init; }
    public SortDirection SortDirection { get; init; } = SortDirection.Ascending;
    public string? Search { get; init; }
}
