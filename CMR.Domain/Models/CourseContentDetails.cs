using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMR.Domain.Models
{
    public class CourseContentDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public TimeOnly Duration { get; set; }

        public bool IsLock { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Required]
        public Guid CourseContentId { get; set; }

        [ForeignKey("CourseContentId")]
        public CourseContent? CourseContent { get; set; }
    }
}