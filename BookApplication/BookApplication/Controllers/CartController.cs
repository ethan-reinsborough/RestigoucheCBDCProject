using BookApplication.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            //Could filter directly from DB but this method returns the same result.
            List<Cart> items = db.Carts.ToList();
            List<Cart> personalCart = new List<Cart>();
            foreach(var item in items)
            {
                if (item.userID == User.Identity.GetUserId())
                {
                    personalCart.Add(item);
                }             
            }
            return View(personalCart);
        }

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.SingleOrDefault(c => c.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

    }
}