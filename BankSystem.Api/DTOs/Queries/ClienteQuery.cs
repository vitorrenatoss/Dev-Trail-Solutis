namespace BankSystem.Api.DTOs.Queries;

public class ClienteQuery
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public string OrderBy { get; set; } = "Nome";

    public string SortDirection { get; set; } = "asc";
}
