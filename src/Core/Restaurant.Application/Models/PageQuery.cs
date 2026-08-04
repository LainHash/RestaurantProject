using Restaurant.Application.Enums;

namespace Restaurant.Application.Models
{
    public abstract record PageQuery
    {
        public string Keyword { get; init; } = "";
        public SortField SortField { get; set; } = SortField.Name;
        public SortDirection Direction { get; set; } = SortDirection.Asc;
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 12;
    }
}
