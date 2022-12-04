using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiagonsticTestManagement.ViewModels
{
    public class TestViewModel
    {
        [Required]
        public int TestId { get; set; }
        public decimal Fee { get; set; }
    }
}