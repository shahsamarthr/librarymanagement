using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementProject2.Models;
using System.Diagnostics;

namespace LibraryManagementProject2.Controllers
{
    [Authorize]
    public class BorrowHistoriesController : Controller
    {
        private BooksDb2 db = new BooksDb2();

        // GET: BorrowHistories
        public ActionResult Index(int? id,int ? id1)
        {
            bool flag = true;
            ViewBag.f = flag;
            //var s = from p in db.BorrowHistories
            //        join b ib.Users on
            //        p.UserId equals b.UserId
            //        select new { D = p, B = b };
            (from p in db.Mybooks
             where p.BookId == id
             select p).ToList().ForEach(x => x.qty = x.qty + 1);
            db.SaveChanges();


            var students = db.BorrowHistories.FirstOrDefault(s => s.BorrowHistoryId == id1);
            if (students != null)
            {
                db.BorrowHistories.Remove(students);
                db.SaveChanges();
            }


            var bh = db.BorrowHistories.Where(x=>x.User.Name==User.Identity.Name);

            //var borrowHistories = db.BorrowHistories.Include(b => b.Books).Include(b => b.User);
            return View(bh.ToList());
        }

        // GET: BorrowHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Create
        public ActionResult Create(int ?id)
        {

            string name = User.Identity.Name;//asp-net user table
            BooksDb2 db2 = new BooksDb2();
            var xx = from i in db2.Users
                     where i.Name == name
                     select i.UserId;
            

            int userid = xx.ToList().FirstOrDefault();
            ViewBag.userid1 = userid;
            Debug.WriteLine("userid is " + userid);
            ViewBag.BookId = new SelectList(db.Mybooks.Where(n => n.BookId == id), "BookId", "BookId");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserId");
            //ViewBag.BookId = new SelectList(db.Mybooks.Where(n => n.BookId == id), "BookId", "BookId");
            
            (from p in db2.Mybooks
             where p.BookId == id
             select p).ToList().ForEach(x=>x.qty = x.qty -1);
            db2.SaveChanges();
            return View();
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowHistoryId,BookId,UserId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
               

            if (ModelState.IsValid)
            {
                string name = User.Identity.Name;
                BooksDb2 db2 = new BooksDb2();
                var xx = from i in db2.Users
                         where i.Name == name
                         select i.UserId;


                int userid = xx.ToList().FirstOrDefault();
                BorrowHistory bh = new BorrowHistory();
                bh.UserId = userid;
                bh.BookId = borrowHistory.BookId;
                bh.BorrowDate = borrowHistory.BorrowDate;
                bh.ReturnDate = borrowHistory.ReturnDate;

                db.BorrowHistories.Add(bh);
                db.SaveChanges();
                //  db.BorrowHistories.Add(borrowHistory);
                // db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "BookId", borrowHistory.BookId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserId", borrowHistory.UserId);
            return View(borrowHistory);
        }
        [Authorize(Roles ="Admin")]
        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "Title", borrowHistory.BookId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", borrowHistory.UserId);
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BorrowHistoryId,BookId,UserId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Mybooks, "BookId", "Title", borrowHistory.BookId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", borrowHistory.UserId);
            return View(borrowHistory);
        }

        [Authorize(Roles = "Admin")]
        // GET: BorrowHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowHistory borrowHistory = db.BorrowHistories.Find(id);
            db.BorrowHistories.Remove(borrowHistory);
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
