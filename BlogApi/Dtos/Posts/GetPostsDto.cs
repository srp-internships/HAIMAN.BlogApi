using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos.Posts
{
    public class GetPostsDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string UserFullName { get; set; } = string.Empty;
    }
}
