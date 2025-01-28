using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMR.Domain.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "About Course")]
        public string AboutCourse { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Course Content")]
        public string CourseContent { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string LongDesc { get; set; } = string.Empty;

        [Required]
        public string Categories { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        public string ProjectBy { get; set; } = string.Empty;

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public Guid FrequencyId { get; set; }

        [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }
    }
}