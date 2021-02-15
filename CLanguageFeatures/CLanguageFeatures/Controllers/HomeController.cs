using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLanguageFeatures.Models;
using System.Text;

namespace CLanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigae to URL to show an example";
        }
        public ActionResult Result()
        {
            Product my_prod = new Product();
            string vname;
            my_prod.name="Alaa";
           vname = my_prod.name;
            return View("Result",(object) vname);

        }
        public ViewResult CreateProduct1()
        {
            Product prod = new Product();
            prod.id = 100;
            prod.name = "PMW";
            prod.category = "Car";
            prod.price = 27.5;

            return View("Result",
                
                (object)string.Format("Category {0},Name {1},Price {2} ,ID {3}", prod.category,prod.name,prod.price,prod.id));
        }

        public ViewResult CreateProduct2()
        {
            Product prod = new Product
            {
                id = 100,
                name = "PMW",
                category = "Car",
                price = 27.5
            };

            return View("Result",

                (object)string.Format("Category {0},Name {1},Price {2} ,ID {3}", prod.category, prod.name, prod.price, prod.id));
        }

        public ActionResult CreateCollection()
        {
            string[] mystring =  {"A","B","C" };
            List<int> mylist =new List<int> { 1,2,3};
            Dictionary<string, int> mydict = new Dictionary<string, int> { { "A",1}, {"B",2 } };

            return View("Result",(object)mydict["B"].ToString());

        }


        public ActionResult UseExtension()
        {

            ShopingCart cart = new ShopingCart{
                products= new List <Product> {
                new Product {name= "P1",id=10,category="PM",price=100 },
                new Product {name= "P1",id=10,category="PM" ,price=20}
            }
            };
           double total= cart.TotalPrices();
            return View("Result", (object)string.Format("Total: {0:c} ",total));
        }

        public ActionResult UseExtensionIE()
        {
            IEnumerable <Product> carts =new ShopingCart {

                products = new List<Product> {
                new Product {name= "P1",id=10,category="PM",price=100 },
                new Product {name= "P1",id=10,category="PM" ,price=20}
            }
            };

            Product[] productarray = { new Product() { name = "P10", id = 100, category = "PM", price = 160 } };
            double total1 = productarray.TotalPricesIE();
            double total2 = carts.TotalPricesIE();

            return View("Result", (object)string.Format("From IE {0} , from Array {1}", total1, total2));
        }

        public ActionResult UseFilterExtensionMethod()
        {
            var MyAnnon = new { id=1, name="Alaa", type ="C"};
            var a = "tst";
            var b = new Product { };
            IEnumerable <Product> products = new ShopingCart
            {

                products = new List<Product> {
                new Product {name= "P1",id=10,category="PM",price=100 },
                new Product {name= "P1",id=10,category="PMC" ,price=20}
            }

               
            };
            double total = 0;
            foreach (Product p in products.FilterByCategoryIE("PM"))
                total += p.price;
                 
            return View("Result",(object)total.ToString());

        }

        public ActionResult CreateAnnonArray()
        {
            var annon = new[]
            {
                new { Name="AA",Category="CAT1"},
                 new { Name="AB",Category="CAT2"},
                  new { Name="AC",Category="CAT3"}
            };
            StringBuilder bld = new StringBuilder();
            foreach (var a in annon)
                bld.Append(a.Name).Append(" ");
            return View("Result",(object) bld.ToString());
        }

        public ActionResult FindProducts()
        {

            Product[] prod = {      new Product {name= "P1",id=10,category="PM",price=100 },
                new Product {name= "P1",id=10,category="PM" ,price=20},
                  new Product {name= "P1",id=10,category="PM" ,price=22},
                    new Product {name= "P1",id=10,category="PM" ,price=23}
            };

            var lq = from p in prod
                     where p.price > 20
                     select new { p.price, p.name };

            StringBuilder st = new StringBuilder();
            int count = 0;
            foreach (var v in lq) {
                st.AppendFormat("Price: {0}", v.price);
                if (++count == 3)
                break;
        }
                    return View("Result",(object)st.ToString());


        }

        public ActionResult FindProductsDo()
        {

            Product[] prod = {      new Product {name= "P1",id=10,category="PM",price=100 },
                new Product {name= "P1",id=10,category="PM" ,price=20},
                  new Product {name= "P1",id=10,category="PM" ,price=22},
                    new Product {name= "P1",id=10,category="PM" ,price=23}
            };

            var lq = prod.OrderByDescending(p => p.price).Take(3).Select(p => new { p.id, p.name,p.price}).Where(p => p.price > 10);

            StringBuilder st = new StringBuilder();
            int count = 0;
            foreach (var v in lq)
            {
                st.AppendFormat("Price: {0}", v.price);
                if (++count == 3)
                    break;
            }
            return View("Result", (object)st.ToString());


        }
        public ActionResult FindProductsDo2()
        {

            Product[] prod = {      new Product {name= "P1",id=10,category="PM",price=100 },
                new Product {name= "P1",id=10,category="PM" ,price=20},
                  new Product {name= "P1",id=10,category="PM" ,price=22},
                    new Product {name= "P1",id=10,category="PM" ,price=23}
            };

            double sum1 = prod.Sum(p => p.price);
            var lq = prod.OrderByDescending(p => p.price).Take(3).Select(p => new { p.id, p.name, p.price }).Where(p => p.price > 10);
            prod[3] = new Product { name= "Add", price=4000};//it will considered regardless it came after lq definition
            double sum2 = prod.Sum(p => p.price);

            //StringBuilder st = new StringBuilder();
            //int count = 0;
            //foreach (var v in lq)
            //{
            //    st.AppendFormat("Price: {0}", v.price);
            //    if (++count == 3)
            //        break;
            //}
            return View("Result", (object)string.Format("SUM1 is: {0} , SUM2 is : {1}",sum1,sum2));


        }

    }
}