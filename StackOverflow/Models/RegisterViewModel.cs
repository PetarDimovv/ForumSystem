using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Models
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
