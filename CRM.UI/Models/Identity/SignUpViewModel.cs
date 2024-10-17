using System.ComponentModel.DataAnnotations;

namespace CRM.UI.Models.Identity
{
    public class SignUpViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is missing or invalid.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Incorrect or missing password")]
        public string Password { get; set; }
    }
}