namespace StackOverflow.Models.QueryParameters
{
    public class CommentQueryParameters
    {
        public string Username { get; set; }
        public string CreatedBy { get; set; }
        public int? Id { get; set; }
        public int? Likes { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}
