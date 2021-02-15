using BookStore.Domain.Abstract;
using BookStore.Domain.Entity;

using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        public IBookRepository repository;
        public IOrderProcessor orderprocessor;
        public CartController(IBookRepository rep, IOrderProcessor oprocessor)
        {

            repository = rep;
            orderprocessor = oprocessor;
        }

        public RedirectToRouteResult AddToCart(Cart cart,int isbn,string returnUrl)
        {
          Book book = repository.Books.FirstOrDefault(b => b.ISBN == isbn);//test case
         //  Book book = books.FirstOrDefault(b => b.ISBN == isbn);

            if (book != null)
            {
                //Add the book
                //  GetCart().Additem(book);
                cart.Additem(book);

            }
            return RedirectToAction("Index",new {returnUrl});//to Index

        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int isbn, string returnUrl)
        {
            Book book = repository.Books.FirstOrDefault(b => b.ISBN == isbn);
           // Book book = books.FirstOrDefault(b => b.ISBN == isbn);

            if (book != null)
            {
                //Add the book
              //  GetCart().RemoveLine(book);
              cart.RemoveLine(book);

            }
            return RedirectToAction("Index", new { returnUrl });//to Index

        }

        private Cart GetCart()
        {
            //Get user session
            Cart cart =(Cart) Session["Cart"];
            if (cart == null)
            {
                //new one
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, returnUrl = returnUrl });

        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);

        }

        public ViewResult Checkout()
        {
              

            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart,ShippingDetails shippingdetails)
        {

            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty");

            if (ModelState.IsValid)
            {
                orderprocessor.ProcessOrder(cart,shippingdetails);
                cart.Clear();
                return View("Complete");

            }
            else
                return View(shippingdetails);


        }
    }
}