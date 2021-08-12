using BookApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookApplication.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index(string sortOrder)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewBag.DateSortParm = sortOrder == "PublicationDate" ? "Publication Date" : "PublicationDate";
            var Books = from c in db.Books select c;
            switch (sortOrder)
            {
                case "Title":
                    Books = Books.OrderBy(c => c.Title);
                    break;

                case "Title_Desc":
                    Books = Books.OrderByDescending(c => c.Title);
                    break;

                case "PublicationDate":
                    Books = Books.OrderBy(c => c.PublicationDate);
                    break;

                case "PublicationDate_Desc":
                    Books = Books.OrderByDescending(c => c.PublicationDate);
                    break;
            }
            return View(Books.ToList());
        }

        // GET: Books/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Include(c => c.Cover).SingleOrDefault(c => c.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = RoleName.Admin)]       
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [Authorize(Roles = RoleName.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,PublicationDate")] Book book, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var bookCover = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.BookCover,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        bookCover.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    book.Cover = new List<File> { bookCover };
                }
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit
        [Authorize(Roles = RoleName.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,PublicationDate")] Book book, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                if (upload != null && upload.ContentLength > 0)
                {
                    var bookCover = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.BookCover,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        bookCover.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    book.Cover = new List<File> { bookCover };
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete
        [Authorize(Roles = RoleName.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Books/Add
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Title")] Book book)
        {
            //Simple logic which will either instantiate a cart entry for the book, or if it already exists,
            //it will increase the quantity by 1.
            string checkUser = User.Identity.GetUserId();
            Cart checkItem = db.Carts.Where(c => c.bookID == book.ID && c.userID == checkUser).FirstOrDefault();
            if (checkItem != null)
            {
                db.Entry(checkItem).State = EntityState.Modified;
                checkItem.quantity++;
            }
            else
            {
                Cart cart = new Cart();
                cart.bookTitle = book.Title;
                cart.bookID = book.ID;
                cart.quantity = 1;
                cart.userID = checkUser;
                db.Carts.Add(cart);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
