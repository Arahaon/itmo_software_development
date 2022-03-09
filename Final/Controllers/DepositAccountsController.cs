using Final.Data;
using Final.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Final.Controllers
{
    [Authorize]
    public class DepositAccountsController : Controller
    {
        private ApplicationUserManager _userManager;
        // GET: DepositAccounts
        private AppDbContext db = new AppDbContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        // GET: DepositAccounts
        public ActionResult Index()
        {
            ViewBag.Role = UserManager.GetRoles(User.Identity.GetUserId()).First();
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            return View(db.DepositAccounts.Where(x => x.Owner == currentUser.UserName).ToList());
        }

        [Authorize(Roles = "admin")]
        // GET: DepositAccounts
        public ActionResult SeeAll()
        {
            return View("Index", db.DepositAccounts.ToList());
        }

        // GET: DepositAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepositAccount DepositAccount = db.DepositAccounts.Find(id);
            if (DepositAccount == null)
            {
                return HttpNotFound();
            }
            return View(DepositAccount);
        }

        // GET: DepositAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepositAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Owner,Amount,Currency")] DepositAccount DepositAccount)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            DepositAccount.Owner = currentUser.UserName;

            var validCurrency = db.Currencies.Where(x => x.Name == DepositAccount.Currency).Count() == 1;
            if (ModelState.IsValid && validCurrency)
            {
                db.DepositAccounts.Add(DepositAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (!validCurrency)
                ViewBag.InvalidCurrencyMessage = "Select existing currency";
            return View(DepositAccount);
        }

        // GET: DepositAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepositAccount DepositAccount = db.DepositAccounts.Find(id);
            if (DepositAccount == null)
            {
                return HttpNotFound();
            }
            return View(DepositAccount);
        }

        // POST: DepositAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Owner,Amount,Currency")] DepositAccount DepositAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(DepositAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DepositAccount);
        }

        // GET: DepositAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepositAccount DepositAccount = db.DepositAccounts.Find(id);
            if (DepositAccount == null)
            {
                return HttpNotFound();
            }
            return View(DepositAccount);
        }

        // POST: DepositAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepositAccount DepositAccount = db.DepositAccounts.Find(id);
            db.DepositAccounts.Remove(DepositAccount);
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
