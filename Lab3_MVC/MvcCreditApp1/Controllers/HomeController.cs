using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MvcCreditApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCreditApp1.Controllers
{
    public class HomeController : Controller
    {
        private CreditContext db = new CreditContext();

        public ActionResult Index()
        {
            GiveCredits();

            return View();
        }

        private void GiveCredits()
        {
            var allCredits = db.Credits.ToList<Credit>();
            ViewBag.Credits = allCredits;
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateBid()
        {
            GiveCredits(); 
            var allBids = db.Bids.ToList<Bid>(); 
            ViewBag.Bids = allBids; 
            return View();
        }

        [Authorize]
        [HttpPost]
        public string CreateBid(Bid newBid)
        {
            newBid.bidDate = DateTime.Now;
            db.Bids.Add(newBid);
            db.SaveChanges();
            return "Спасибо, <b>" + newBid.Name + "</b>, за выбор нашего банка. Ваша заявка будет рассмотрена в течении 10 дней.";
        }

        public ActionResult BidSearch(string name)
        {
            var allBids = db.Bids.Where(a => a.CreditHead.Contains(name)).ToList();
            if (allBids.Count == 0)
            {
                return Content("Указанный кредит " + name + " не найден");
                //return HttpNotFound();
            }
            return PartialView(allBids);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            ViewBag.rol = roles;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}