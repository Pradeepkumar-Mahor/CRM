using System.ComponentModel.DataAnnotations;

namespace CRM.UI.Models
{
    public class AddProductViewModel
    {
        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        public string? ImgUrl { get; set; }

        [Required]
        public bool ProductIsActive { get; set; } = true;
    }
}