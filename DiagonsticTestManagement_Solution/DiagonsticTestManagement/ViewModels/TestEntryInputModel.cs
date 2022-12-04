using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using DiagonsticTestManagement.Models;

namespace DiagonsticTestManagement.ViewModels
{
    public class TestEntryInputModel
    {
        [Display(Name = "Entry Id")]
        public int TestEntryId { get; set; }
        [Required, StringLength(50), Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Required, Display(Name = "Patient Picture")]
        public HttpPostedFileBase Picture { get; set; }
        [Required, Display(Name = "Age"),]
        public int Age { get; set; }

        [Required, StringLength(20), Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Required,DataType(DataType.Date), Display(Name = "Test Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TestDate { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Due Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public List<TestViewModel> Tests { get; set; } = new List<TestViewModel>();
        
    }
}