using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entity;
using BookStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.UnitTest
{
    [TestClass]
    public class Admin_Test
    {
        [TestMethod]
        public void Index_Contain_all_products()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]

            {
                new Book {ISBN=1,Title="Book1" },
                new Book {ISBN=2,Title="Book2" },
                new Book {ISBN=3,Title="Book3" },

                                      });
            AdminController target = new AdminController(mock.Object);

            //Act
            Book[] result =((IEnumerable<Book>) target.Index().ViewData.Model).ToArray();

            //Asert
            Assert.AreEqual(result.Length,3);
            Assert.AreEqual("Book1",result[0].Title);

        }
        [TestMethod]
        public void Can_Edit_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[] {
                new Book {ISBN=1,Title="Book1" },
                new Book {ISBN=2,Title="Book2" },
                new Book {ISBN=3,Title="Book3" },

            });

            AdminController target = new AdminController(mock.Object);
            Book b1=(target.Edit(1)).ViewData.Model as Book;
            Book b2 = (target.Edit(2)).ViewData.Model as Book;
            Book b3 = (target.Edit(3)).ViewData.Model as Book;

            Assert.AreEqual("Book1",b1.Title);
            Assert.AreEqual(1, b1.ISBN);

        }
        [TestMethod]
        public void Cannot_Edit_Nonexist_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[] {
                new Book {ISBN=1,Title="Book1" },
                new Book {ISBN=2,Title="Book2" },
                new Book {ISBN=3,Title="Book3" },

            });

            AdminController target = new AdminController(mock.Object);
            Book b4 = (target.Edit(4)).ViewData.Model as Book;

            //   Assert.AreEqual("Book1", b1.Title);
            Assert.IsNull(b4);

        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController target = new AdminController(mock.Object);
            Book book = new Book { Title = "New Book" };
            ActionResult result = target.Edit(book);
            //   Assert.AreEqual("Book1", b1.Title);
            mock.Verify(b=>b.SaveBook(book));
            Assert.IsNotInstanceOfType(result,typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Valid_Changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController target = new AdminController(mock.Object);
            Book book = new Book { Title = "New Book" };
            target.ModelState.AddModelError("","error");
            ActionResult result = target.Edit(book);
            //   Assert.AreEqual("Book1", b1.Title);
            mock.Verify(b => b.SaveBook(It.IsAny<Book>()),Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
