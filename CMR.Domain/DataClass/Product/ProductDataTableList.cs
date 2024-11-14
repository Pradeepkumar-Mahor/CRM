﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMR.Domain.DataClass
{
    public class ProductDataTableList
    {
        public decimal? TotalRow { get; set; }

        public decimal RowNumber { get; set; }

        public int Id { get; set; }
        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? Img { get; set; }

        public bool ProductIsActive { get; set; } = true;

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}