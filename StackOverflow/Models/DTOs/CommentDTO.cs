using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models.DTOs
{
    public class CommentDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty.")]
        [MinLength(32, ErrorMessage = "The {0} comment must be at least {1} long.")]
        [MaxLength(8192, ErrorMessage = "The {0} comment must be at most {1} long.")]
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
