using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = string.Empty;

        public string FullName { get => $"{FirstName} {LastName}"; }

        [Required]
        public string? UserName { get; set; } = null!;

        [Required]
        public string? Email { get; set; } = null!;

        public string? Addres { get; set; }
        public string? Telephone { get; set; }
        public string? CompanyName { get; set; }

        public List<Post>? Posts { get; set; }

    }
}
