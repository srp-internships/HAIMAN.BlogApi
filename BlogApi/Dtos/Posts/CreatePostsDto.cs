
namespace BlogApi.Dtos.Posts
{
    public class CreatePostsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
