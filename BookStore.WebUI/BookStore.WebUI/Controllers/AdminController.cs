using BookStore.Domain.Abstract;
using BookStore.Domain.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        private IBookRepository repository;
        public AdminController(IBookRepository rep)
        {
            repository=rep;
        }
        public ViewResult Index()//including the search
        {
            return View(repository.Books);
            }
        [HttpPost]
        public ViewResult Index(string SearchValue)//including the search
        {

            IEnumerable<Book> books;
            if (SearchValue != null)
            {
                books = from b in repository.Books
                        where b.Description.Contains(SearchValue) ||
                        b.Title.Contains(SearchValue) ||
                         b.Specizailation.Contains(SearchValue)
                        select b;
            }
            else
            {
                books = from b in repository.Books
                        select b;

            }
            return View("Index", books);
                         
        }

        public ViewResult Edit(int ISBN)
        {
            Book book = repository.Books.FirstOrDefault(b => b.ISBN == ISBN);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase image = null)
        {
            List<Book> books=new List<Book> { 
         

                new Book { ISBN = 1, Title = "Book1", Price = 250, Description = "This book for beginer", Specizailation = "IT" },
                    new Book { ISBN = 2, Title = "Book2", Price = 20, Description = "This book for IT", Specizailation = "IR" },
                    new Book { ISBN = 3, Title = "Book3", Price = 400, Description = "This book for SW", Specizailation = "IT" },
                    new Book { ISBN = 4, Title = "Book4", Price = 255, Description = "This book for RT", Specizailation = "DER" },
                    new Book { ISBN = 5, Title = "Book5", Price = 275, Description = "This book for FN", Specizailation = "SS" }
                };
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                //repository.SaveBook(book);
                books.Add(book);
                TempData["message"] = book.Title + "has been saved";
                return RedirectToAction("Index");
            }
            else {
                //
                return View(book);
            }
        }

     
        public ViewResult Create()
        {

            return View("Edit",new Book());
        }
        [HttpGet]

        public ActionResult Delete(int ISBN)
        { 
            
          Book Deletedbook= repository.DeleteBook(ISBN);
            if (Deletedbook != null)
                TempData["message"] = Deletedbook.Title + "has been deleted";
            return RedirectToAction("Index");

        }
        //[HttpPost]
        //public ViewResult Search(string SearchValue)
        //{
        //    IEnumerable<Book> books;
        //    if (SearchValue != null)
        //    {
        //        books = from b in repository.Books
        //                                  where b.Description.Contains(SearchValue) ||
        //                                  b.Title.Contains(SearchValue) ||
        //                                   b.Specizailation.Contains(SearchValue)
        //                                  select b;
        //    }
        //    else {
        //        books = from b in repository.Books
        //                select b;

        //    }
        //    return View("Index",books);
        //}

    }
}