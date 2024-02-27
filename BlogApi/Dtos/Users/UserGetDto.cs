using System.ComponentModel.DataAnnotations;

namespace BlogApi.Dtos.Users
{
    public class UserGetDto
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string Telephone { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
    }
}
