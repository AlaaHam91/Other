using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entity;
using BookStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookStore.WebUI.Models;
using BookStore.WebUI.HtmlHelper;

namespace BookStore.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(new Book[]

                {new Book { ISBN=1,Title="B1" },
                new Book { ISBN=2,Title="B2" },
                new Book { ISBN=3,Title="B3" },
                new Book { ISBN=4,Title="B4" },
                }

                );
            BookController bcontrol = new BookController(mock.Object);
            BookListViewModel Result = (BookListViewModel)bcontrol.List(null, 1).Model;

            Book[] bookarray = Result.Books.ToArray();
            Assert.IsTrue(bookarray.Length == 1);
            Assert.AreEqual(bookarray[0].Title, "B1");
        }
        [TestMethod]

        public void Can_Generate_Page_Links()
        {

            string ExpectedResult = null;
            HtmlHelper myhelper = null;
            PagingInfo paginginfo = new PagingInfo { TotalItems = 7, CurrentPage = 2, ItemPerPage = 3 };
            Func<int, string> PageUrlDelegate = i => "Page" + i;
            ExpectedResult = "<a class=\"btn btn-default\" href=\"Page1\">1</a>" +
                              "<a class=\"btn btn-default btn-Primary Selected\" href=\"Page2\">2</a>" +
                              "<a class=\"btn btn-default\" href=\"Page3\">3</a>";
            MvcHtmlString result =
                myhelper.PageLinks(paginginfo, PageUrlDelegate);
            Assert.AreEqual(ExpectedResult, result.ToString());

        }

        [TestMethod]

        public void Can_Send_Pagination_View_Model()
        {

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(
                new Book[] {
                new Book { ISBN=  1,Title="B1" },
                new Book { ISBN = 2, Title = "B2" },
                new Book { ISBN = 3, Title = "B3" },
                new Book { ISBN = 4, Title = "B4" }
                }
                );

            BookController controller = new BookController(mock.Object);
            controller.PageSize = 3;

            BookListViewModel result = (BookListViewModel)controller.List(null, 2).Model;
            PagingInfo pageinfo = result.paginginfo;

            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemPerPage, 3);
            Assert.AreEqual(pageinfo.TotalPages, 2);
            Assert.AreEqual(pageinfo.TotalItems, 4);


        }

        [TestMethod]

        public void Can_Filter_Books()
        {

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns(
                new Book[] {
                new Book { ISBN=  1,Title="B1",Specizailation="IT" },
                new Book { ISBN = 2, Title = "B2" ,Specizailation="IT"},
                new Book { ISBN = 3, Title = "B3" ,Specizailation="Pharm"},
                new Book { ISBN = 4, Title = "B4" ,Specizailation="IT"}
                }
                );

            BookController controller = new BookController(mock.Object);
            controller.PageSize = 2;



            Book[] result = ((BookListViewModel)controller.List("IT", 2).Model).Books.ToArray();


            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual(result[0].Title, "B4");



        }

    }
}
