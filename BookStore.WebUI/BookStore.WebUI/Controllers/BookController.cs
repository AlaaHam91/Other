using BookStore.Domain.Entity;
using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Domain.Concerate;
using BookStore.WebUI.Models;

namespace BookStore.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 3;
        public BookController(IBookRepository bookrep)
        {
            repository = bookrep;
        }

        public ViewResult List(string specilization,int pagenum=1)
        {

            BookListViewModel model = new BookListViewModel {
                Books =
                   repository.Books
                   .Where(b => specilization == null || b.Specizailation == specilization)
                       .OrderBy(b => b.ISBN)
                       .Skip((pagenum - 1) * PageSize)
                       .Take(PageSize)
                   ,
                paginginfo = new PagingInfo
                { CurrentPage = pagenum,
                    ItemPerPage = PageSize,
                    TotalItems = specilization == null ?  repository.Books.Count() :
                   repository.Books.Where(b => b.Specizailation == specilization).Count()

                },

                CurrentSpecilization = specilization
                

            };

            return View(model);

            //return View(repository.Books
            //    .OrderBy(b =>b.ISBN)
            //    .Skip((pagenum-1)*PageSize)
            //   .Take(PageSize)
                
            //    );

        }
        // GET: Book
        public ViewResult ListAll()
        {
           
            return View( repository.Books);
        }
        //public FileContentResult GetImage(int ISBN)
        //{
        //    Book book = repository.Books.FirstOrDefault(p => p.ISBN == ISBN);
        //    if (book != null)
        //    {
        //        return File(book.ImageData, book.ImageMimeType);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        [HttpPost]
        public FileContentResult GetImage(int ISBN)
        {
            Book book = repository.Books.FirstOrDefault(p => p.ISBN == ISBN);
            if (book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}