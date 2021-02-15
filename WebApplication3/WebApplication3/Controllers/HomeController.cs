using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int t = DateTime.Now.Hour ;
            ViewBag.spec= t> 12 ? "Good" : "Bad";
            return View();
        }
        [HttpGet]
        public ActionResult Regestiration()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Regestiration(Class1 guestresponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestresponse);
            }
            else
                return View();

        }
    }
}