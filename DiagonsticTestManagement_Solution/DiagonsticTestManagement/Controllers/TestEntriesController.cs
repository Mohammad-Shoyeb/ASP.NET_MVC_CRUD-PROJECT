using DiagonsticTestManagement.Models;
using DiagonsticTestManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace DiagonsticTestManagement.Controllers
{
    [Authorize]
    public class TestEntriesController : Controller
    {
        private readonly DCMDbContext db = new DCMDbContext();
        // GET: TestEntries
        public ActionResult Index()
        {
            var data = db.TestEntries
                .Include(x => x.EntryTests.Select(y => y.Test))
                .ToList();
              
            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Tests = db.Tests.ToList();
            var data = new TestEntryInputModel();
            data.Tests.Add(new TestViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(TestEntryInputModel model, int[] TestId)
        {
            if (ModelState.IsValid)
            {
                var et = new TestEntry
                {
                    PatientName = model.PatientName,
                    Age= model.Age,
                    MobileNo = model.MobileNo,
                    TestDate = model.TestDate,
                    DueDate = model.DueDate
                };
                foreach(var x in TestId)
                {
                    et.EntryTests.Add(new EntryTest { TestId = x });
                }
                if(model.Picture.ContentLength> 0)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Guid.NewGuid() + ext;
                    model.Picture.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));
                    et.Picture = fileName;
                }
                db.TestEntries.Add(et);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tests = db.Tests.ToList();
            
            return View(model);
        }
        public JsonResult GetFee(int id)
        {
            var t =db.Tests.FirstOrDefault(x => x.TestId == id);
            return Json(t == null ? 0: t.Fee);
        }
        public ActionResult CreateNewField(TestEntryInputModel data)
        {
            ViewBag.Tests = db.Tests.ToList();
            data.Tests.Add(new TestViewModel());
            return PartialView(data);
        }
        public ActionResult CreateV1()
        {
            
            return View();
        }
        public PartialViewResult CreateTestEntry()
        {
            ViewBag.Tests = db.Tests.ToList();
            
            return PartialView("_TestEntry");
        }
    }
}