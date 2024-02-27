using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Body { get; set; }

        public User? User { get; set; }
    }
}
