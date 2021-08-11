using BookApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookApplication.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class CoverApprovalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET Post
        public ActionResult Index()
        {
            return View(db.Files.ToList());
        }

        //GET Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: FileApproval/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "FileID, FileName, ContentType, Content, FileType, ID, Approved, ApprovedBy")] File file)
        {
            if (ModelState.IsValid)
            {
                File fileDb = db.Files.Include(c => c.Book).SingleOrDefault(c => c.FileID == file.FileID);
                fileDb.Approved = file.Approved;
                fileDb.ApprovedBy = "Ethan Reinsborough";
                db.Entry(fileDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }
    }
}