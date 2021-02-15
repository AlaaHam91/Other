using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStore.Domain.Entity;
using System.Linq;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using BookStore.WebUI.Models;
using BookStore.WebUI.HtmlHelper;

namespace BookStore.UnitTest
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]

        public void Can_Add_New_Line()
        {

            Book b1 = new Book { ISBN = 1, Title = "BookA" };
            Book b2 = new Book { ISBN = 2, Title = "BookB" };

            Cart target = new Cart();
            target.Additem(b1);
            target.Additem(b2, 3);

            CartLine[] result = target.Lines.ToArray();

            Assert.AreEqual(result[0].Book, b1);
            Assert.AreEqual(result[1].Book, b2);

        }
        [TestMethod]
        public void Can_Add_Qty_For_Existing_Lines()
        {

            Book b1 = new Book { ISBN = 1, Title = "BookA" };
            Book b2 = new Book { ISBN = 2, Title = "BookB" };

            Cart target = new Cart();
            target.Additem(b1);
            target.Additem(b2, 3);
            target.Additem(b1,5);

            CartLine[] result = target.Lines.OrderBy(b=>b.Book.ISBN).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 6);
            Assert.AreEqual(result[1].Quantity, 3);

        }
        [TestMethod]
        public void Can_Remove_Lines()
        {

            Book b1 = new Book { ISBN = 1, Title = "BookA" };
            Book b2 = new Book { ISBN = 2, Title = "BookB" };
            Book b3 = new Book { ISBN = 2, Title = "BookB" };

            Cart target = new Cart();
            target.Additem(b1);
            target.Additem(b2, 3);
            target.Additem(b1, 5);
            target.Additem(b2, 1);
            target.RemoveLine(b2);
          //  CartLine[] result = target.Lines.OrderBy(b => b.Book.ISBN).ToArray();

            Assert.AreEqual(target.Lines.Where(b=>b.Book == b2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 1);

        }
        [TestMethod]

        public void CalculateCartTotal()
        {
            Book b1 = new Book { ISBN = 1, Title = "BookA" ,Price=100};
            Book b2 = new Book { ISBN = 2, Title = "BookB", Price = 50 };
            Book b3 = new Book { ISBN = 3, Title = "BookB", Price = 25 };

            Cart target = new Cart();
            target.Additem(b1,1);
            target.Additem(b2, 3);
            target.Additem(b3,2);
            decimal total = target.ComputeTotalValue(); 
            Assert.AreEqual(total, 300);

        }

        [TestMethod]

        public void Can_Clear()
        {
            Book b1 = new Book { ISBN = 1, Title = "BookA", Price = 100 };
            Book b2 = new Book { ISBN = 2, Title = "BookB", Price = 50 };
            Book b3 = new Book { ISBN = 3, Title = "BookB", Price = 25 };

            Cart target = new Cart();
          
            target.Additem(b3, 2);
            target.Clear();
            Assert.AreEqual(target.Lines.Count(), 0);

        }

        [TestMethod]

        public void Can_Add_To_Cart()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books)
                .Returns(new Book[] 
                { new Book { ISBN = 10, Title = "ASP.NET MVC", Specizailation = "Programing" }
                }.AsQueryable());
            Cart cart = new Cart();

            CartController target = new CartController(mock.Object,null);
            target.AddToCart(cart, 10, null);
            Assert.AreEqual(cart.Lines.Count(),1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Book.Title, "ASP.NET MVC");

            //RedirectToRouteResult result= target.AddToCart(cart, 2, "MyUrl");

            //Assert.AreEqual(result.RouteValues["action"],"Index");//from rout config
            // Assert.AreEqual(result.RouteValues["action"], "MyUrl");//from rout config
        }
        [TestMethod]

        public void Adding_Book_To_Cart_Goes_To_Cart_Screen()
        {

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books)
                .Returns(new Book[]
                { new Book { ISBN = 10, Title = "ASP.NET MVC", Specizailation = "Programing" }
                }.AsQueryable());
            Cart cart = new Cart();

            CartController target = new CartController(mock.Object,null
                
                );
            RedirectToRouteResult result= target.AddToCart(cart, 2, "MyUrl");
            Assert.AreEqual(result.RouteValues["action"],"Index");//from rout config
            Assert.AreEqual(result.RouteValues["returnUrl"], "MyUrl");//from rout config

        }
        [TestMethod]

        public void Can_View_Cart_Content()
         {

            Cart cart = new Cart();
            CartController target = new CartController(null,null);
            CartIndexViewModel result =(CartIndexViewModel) target.Index(cart,"myUrl").Model;//ViewResult Method
            Assert.AreEqual(result.Cart,cart);
            Assert.AreEqual(result.returnUrl, "myUrl");

        }

        [TestMethod]

        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mymock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shipping = new ShippingDetails();
            CartController target = new CartController(null,mymock.Object);

            ViewResult result = target.Checkout(cart,shipping);

           Assert.AreEqual("",result.ViewName);
           Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }
        [TestMethod]

        public void Cannot_Checkout_Invalid_Shippingdetails()
        {
            Mock<IOrderProcessor> mymock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.Additem(new Book(),1);
            ShippingDetails shipping = new ShippingDetails();
            CartController target = new CartController(null, mymock.Object);
            target.ModelState.AddModelError("error","error");
            ViewResult result = target.Checkout(cart, shipping);


            mymock.Verify(b => b.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),Times.Never);
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }
        [TestMethod]

        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mymock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.Additem(new Book(), 1);
            ShippingDetails shipping = new ShippingDetails();
            CartController target = new CartController(null, mymock.Object);

            ViewResult result = target.Checkout(cart, shipping);
            mymock.Verify(b => b.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }
    }
}
