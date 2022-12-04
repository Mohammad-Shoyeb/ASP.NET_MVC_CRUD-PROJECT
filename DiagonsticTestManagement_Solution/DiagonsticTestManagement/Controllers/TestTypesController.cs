using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DiagonsticTestManagement.Models
{
    [Authorize]
    public class TestTypesController : Controller
    {
        
        DCMDbContext db = new DCMDbContext();
        // GET: TestType
        public ActionResult Index()
        {
            return View(db.TestTypes.Include(tt=>tt.Tests).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateTestType()
        {

            return PartialView("_CreateTestType");
        }
        [HttpPost]
        public PartialViewResult CreateTestType(TestType b)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.TestTypes.Add(b);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditTestType(int id)
        {
            var b = db.TestTypes.First(x => x.TestTypeId == id);
            return PartialView("_EditTestType", b);
        }
        [HttpPost]
        public PartialViewResult EditTestType(TestType b)
        {
            Thread.Sleep(4000);
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.TestTypes.First(x => x.TestTypeId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int TestTypeId)
        {
            var b = new TestType { TestTypeId = TestTypeId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}