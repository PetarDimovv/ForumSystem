using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required andd must not be empty.")]
        [MaxLength(32, ErrorMessage = "The {0} field must be at most {1} characters long.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} characters long")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required andd must not be empty.")]
        [MaxLength(32, ErrorMessage = "The {0} field must be at most {1} characters long.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} characters long")]
        public string LastName { get; set; }

        public int Id { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Column("email", TypeName = "varchar(254)")]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
