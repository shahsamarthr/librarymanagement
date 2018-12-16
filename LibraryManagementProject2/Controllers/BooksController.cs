using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementProject2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;



namespace LibraryManagementProject2.Controllers
{
    [Authorize]
   
    //[Authorize(Roles = "Administrator,Users")]
    public class BooksController : Controller
    {
        ApplicationDbContext db1 = new ApplicationDbContext();
        private BooksDb2 db = new BooksDb2();

        // GET: Books
        public ActionResult Index2(string bkname, string authname, string sortOrder, int? id)
        {
            //Session["userid"] = db.Users.Where(n=>n.UserId);
            ViewBag.Auth = isAdminUser();
            //Session["Userid"]= Convert.ToInt32(from p in db.Users
            //                    where p.Name == User.Identity.Name
            //                    select p.UserId);
           // var bh = db.Users.Where(x => x.Name == User.Identity.Name).Select(x=> x.UserId).ToString();
            //int bh2 = int.Parse(bh);
            //Session["Userid"] = bh2;
            //System.Diagnostics.Debug.WriteLine(Session["Userid"]);
            BooksDb2 Db2 = new BooksDb2();
            //(from p in Db2.Mybooks
            // where p.BookId == id
            // select p).ToList().ForEach(x => x.qty = x.qty + 1);

            //var userId = User.Identity.GetUserId();
            //Session["Userid"] = userId;
            //System.Diagnostics.Debug.WriteLine(Session["Userid"]);
            //BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            //db.BorrowHistories.Remove(borrowHistory);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            Db2.SaveChanges();

            if (!ViewBag.Auth)//For user
            {
                var books = from b in db.Mybooks
                            where b.qty > 0
                            select b;


                if (!string.IsNullOrEmpty(bkname))
                {
                    books = books.Where(x => x.Title.Contains(bkname));

                }

                if (!string.IsNullOrEmpty(authname))
                {
                    books = books.Where(x => x.Author.Contains(authname));

                }

                ViewBag.TitleSortParam = sortOrder == "Title" ? "Title_desc" : "Title";
                ViewBag.AuthorParam = sortOrder == "Author" ? "Author_desc" : "Author";
                switch (sortOrder)
                {
                    case "Title_desc":
                        books = books.OrderByDescending(x => x.Title);
                        break;

                    case "Title":
                        books = books.OrderBy(x => x.Title);
                        break;

                    case "Author":
                        books = books.OrderBy(x => x.Author);
                        break;
                    case "Author_desc":
                        books = books.OrderByDescending(x => x.Author);
                        break;

                }

                return View(books);
            }
            else//For admin
            {
                var books = from b in db.Mybooks
                            select b;


                if (!string.IsNullOrEmpty(bkname))
                {
                    books = books.Where(x => x.Title.Contains(bkname));

                }

                if (!string.IsNullOrEmpty(authname))
                {
                    books = books.Where(x => x.Author.Contains(authname));

                }

                ViewBag.TitleSortParam = sortOrder == "Title" ? "Title_desc" : "Title";
                ViewBag.AuthorParam = sortOrder == "Author" ? "Author_desc" : "Author";
                switch (sortOrder)
                {
                    case "Title_desc":
                        books = books.OrderByDescending(x => x.Title);
                        break;

                    case "Title":
                        books = books.OrderBy(x => x.Title);
                        break;

                    case "Author":
                        books = books.OrderBy(x => x.Author);
                        break;
                    case "Author_desc":
                        books = books.OrderByDescending(x => x.Author);
                        break;

                }

                return View(books);
            }
        }
      
    
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db1));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                    return (true);
                else
                    return (false);
            }
            return (false);
         }
        
        public ActionResult Index()
        {
            //var num = "1";
            //var users = db1.Roles.Where(x=>x.Id == num);
            ViewBag.Auth = isAdminUser(); 
            return View(db.Mybooks.ToList());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Mybooks.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }
        [Authorize(Roles ="Admin")]
        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "BookId,Title,SerialNumber,Author,Publisher,qty,IsAvailable")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Mybooks.Add(books);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }

            return View(books);
        }
        //[Authorize(Roles = "Administrator")]
        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Mybooks.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BookId,Title,SerialNumber,Author,Publisher,qty,IsAvailable")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Mybooks.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Books books = db.Mybooks.Find(id);
            db.Mybooks.Remove(books);
            db.SaveChanges();
            return RedirectToAction("Index2");
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
