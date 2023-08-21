namespace Pagination.Models
{
    public class UserQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
