
using System.ComponentModel.DataAnnotations;

namespace Radicitus.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "You must provide a username.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "You must provide a password."), 
         MinLength(8, ErrorMessage = "Your password must be a minimum of 8 characters long."),
        MaxLength(32, ErrorMessage = "Your password cannot exceed 32 characters.")]
        public string Password { get; set; }
    }
}
