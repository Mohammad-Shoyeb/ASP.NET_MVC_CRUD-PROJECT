using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DiagonsticTestManagement.Models
{
    public class TestType
    {
        [Display(Name = "Type Id")]
        public int TestTypeId { get; set; }
        [Required, StringLength(50), Display(Name = "Type Name")]
        public string TypeName { get; set; }
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
    public class Test
    {
        [Display(Name = "TestId")]
        public int TestId { get; set; }
        [Required, StringLength(50), Display(Name = "Test Name")]
        public string TestName { get; set; }
        [Required,Column(TypeName ="money"), Display(Name = "Fee")]
        public decimal Fee { get; set; }
        [Required, Display(Name = "Available")]
        public bool Available { get; set; }
        [Required]
        public int TestTypeId { get; set; }
        [ForeignKey("TestTypeId")]
        public TestType TestType { get; set; }
        public ICollection<EntryTest> EntryTests { get; set; } = new List<EntryTest>();
    }
    public class TestEntry
    {
        [Display(Name = "Entry Id")]
        public int TestEntryId { get; set; }
        [Required, StringLength(50), Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Required, StringLength(150), Display(Name = "Patient Picture")]
        public string Picture { get; set; }
        [Required,  Display(Name = "Age"), ]
        public int Age { get; set; }

        [Required, StringLength(20), Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Test Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TestDate { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Due Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public ICollection<EntryTest> EntryTests { get; set; } = new List<EntryTest>();
    }
    public class EntryTest
    {
        [Key, Column(Order = 0), ForeignKey("TestEntry")]
        public int TestEntryId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Test")]
        public int TestId { get; set; }
        public TestEntry TestEntry { get; set; }
        public Test Test { get; set; }

    }
    public class DCMDbContext : DbContext
    {
        public DCMDbContext()
        {
            Database.SetInitializer(new DCMDbInitializer());
        }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestEntry> TestEntries { get; set; }
        public DbSet<EntryTest> EntryTests { get; set; }

    }
    public class DCMDbInitializer : DropCreateDatabaseIfModelChanges<DCMDbContext>
    {
        protected override void Seed(DCMDbContext db)
        {
            TestType tt = new TestType { TypeName = "Blood Tes" };
            Test t1 = new Test { TestName = "CSR", TestType = tt, Available = true, Fee = 700.00M };
            TestEntry te = new TestEntry { PatientName = "P1", Age = 20, TestDate = DateTime.Parse("2022-09-01"), DueDate = DateTime.Parse("2022-09-01"), MobileNo = "0189765567", Picture = "1.jpg" };
            te.EntryTests.Add(new EntryTest { Test = t1, TestEntry = te });
            db.TestTypes.Add(tt);
            db.Tests.Add(t1);
            db.TestEntries.Add(te);
            db.SaveChanges();
        }
    }

}