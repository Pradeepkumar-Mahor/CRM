using System.ComponentModel.DataAnnotations;

namespace CRM.UI.Models
{
    public class UpdateProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        public string? ImgUrl { get; set; }

        [Required]
        public bool ProductIsActive { get; set; } = true;
    }
}