using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RasorExamples.Models;
namespace RasorExamples.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Product p = new Product { ProductID = 1, Name = "FirstP", Description = "For one person", Price = 125M, Category = "CAR" };

        public ActionResult Index()
        {
            Product p = new Product {ProductID=1, Name="FirstP",Description="For one person",Price=125M ,Category="CAR"};


            return View(p);
        }

        public ActionResult Demo ()
        {

            ViewBag.Productcount =1;
            ViewBag.ExpressShop = true;
            ViewBag.Discounted = false;
            ViewBag.Suplier = null;
        
            return View(p);
        }
        public ActionResult DemoArray()
        {
            Product[] prods = { new Product {Name="P1",Price=20M },new Product { Name = "P2", Price = 25M }, new Product { Name = "P3", Price = 30M } };

            return View(prods);
        }
    }
}