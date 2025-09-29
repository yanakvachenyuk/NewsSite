using System.ComponentModel.DataAnnotations;

namespace NewsSite.Models
{
    public class AdminUser
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}