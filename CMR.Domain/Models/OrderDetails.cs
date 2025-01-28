using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMR.Domain.Models
{
    public class OrderDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Course Service { get; set; }

        [Required]
        public string ServiceName { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }
    }
}