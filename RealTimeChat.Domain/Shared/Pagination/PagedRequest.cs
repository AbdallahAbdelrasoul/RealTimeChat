namespace RealTimeChat.Domain.Shared.Pagination;
public class PagedRequest
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Column { get; set; }
    public bool IsAscending { get; set; } = true;
}
