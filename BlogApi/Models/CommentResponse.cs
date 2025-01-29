namespace BLOGAPI.Models
{
    public class CommentResponse
    {
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public long TotalComments { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
