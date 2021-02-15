using BookStore.Domain.Abstract;
using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        //

        IBookRepository repository;
        public NavController(IBookRepository rep)
        {
            repository = rep;
            
        }
        public PartialViewResult Menu(string specilization = null/*,bool mobilelayout=false*/)
        {
            ViewBag.SelectedSpec = specilization;
             IEnumerable<string> spec = repository.Books.Select(b => b.Specizailation).Distinct();
            Debug.Print(spec.ToString());
         // IEnumerable<string> spec = books.Select(b => b.Specizailation).Distinct();
         //   string viewName = mobilelayout ? "Menu -Horezintl" : "Menu";
            return PartialView("FinalMenu",spec);
        }
    }
}