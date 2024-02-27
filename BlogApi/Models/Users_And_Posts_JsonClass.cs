using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class UserCredentials
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public Address? Address { get; set; }
        public string? Phone { get; set; }
        public Company? Company { get; set; }

    }


    public class Address
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

    }

    public class Company
    {
        public string? Name { get; set; }
    }


    public class PostCredentials
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string? Body { get; set; }
        public int UserId { get; set; }
    }

}
