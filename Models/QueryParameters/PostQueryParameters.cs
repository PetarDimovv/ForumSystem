namespace StackOverflow.Models.QueryParameters
{
    public class PostQueryParameters
    {
        public string Title { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}
