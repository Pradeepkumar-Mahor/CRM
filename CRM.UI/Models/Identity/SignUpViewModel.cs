using System.ComponentModel.DataAnnotations;

namespace CRM.UI.Models.Identity
{
    public class SignUpViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is missing or invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Incorrect or missing password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Incorrect or missing password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool? RememberMe { get; set; }

        public string? ReturnUrl { get; set; } = string.Empty.ToString();
    }
}