using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
       private Product[] prods = {
            new Product {Name="Prod1", Price=10M },
            new Product {Name="Prod2", Price=50.5M },
            new Product {Name="Prod3", Price=60M },
            new Product {Name="Prod4", Price=70M }

        };
        private ICalculator lc;
       // = new LinqValueCalculator();
        public HomeController(ICalculator c, ICalculator c2)
        {
            lc = c;
        }

        // GET: Home

        public ActionResult Index()
        {


        //IKernel Ninjectkernal = new StandardKernel();
        //Ninjectkernal.Bind<ICalculator>().To<LinqValueCalculator>();
        //ICalculator l = Ninjectkernal.Get<ICalculator>();

        decimal total = 0;
            // LinqValueCalculator l = new LinqValueCalculator();
            //ICalculator l = new LinqValueCalculator();

            ShopingCart c = new ShopingCart(lc) {p=prods };
            total=c.TotalSum();
            return View(total);
        }
    }
}