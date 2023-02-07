using System;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models.DTOs

{
    public class PostDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty.")]
        [MinLength(16, ErrorMessage = "The {0} title must be at least {1} long.")]
        [MaxLength(64, ErrorMessage = "The {0} title must be at most {1} long.")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty.")]
        [MinLength(32, ErrorMessage = "The {0} content must be at least {1} long.")]
        [MaxLength(8192, ErrorMessage = "The {0} content must be at most {1} long.")]
        public string Content { get; set; }

        public int UserId { get; set; }


        //[Required(User != null, ErrorMessage = "The {0} field must have a user.")]
        //public int UserId { get; set; }
        //Not sure how to set it up ach :D
    }
}
