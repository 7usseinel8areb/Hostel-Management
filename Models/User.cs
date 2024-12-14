using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First Name can only contain letters and spaces.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last Name can only contain letters and spaces.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must have at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role must not exceed 50 characters.")]
        [RegularExpression(@"^(User|Admin)$", ErrorMessage = "Role must be either 'User' or 'Admin'.")]
        public string Role { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
