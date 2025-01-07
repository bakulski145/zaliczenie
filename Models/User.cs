using System.ComponentModel.DataAnnotations;

namespace zaliczenie.Models
{
    public class User
    {
        [Key]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        public string Role { get; set; } = "User"; // Domyślnie zwykły użytkownik
    }
}