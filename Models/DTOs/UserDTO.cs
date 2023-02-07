using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models.DTOs
{
    public class UserDTO
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
        [EmailAddress(ErrorMessage = "Invalid Email Address")] //Added the once above - Pavel
        [Column("email", TypeName = "varchar(254)")] //Rado: Not sure for the email validation, for now i will leave it like that
        public string Email { get; set; }
        public string Username { get; set; }

        //TODO We should put Encoding to the password
        public string Password { get; set; }
    }
}
