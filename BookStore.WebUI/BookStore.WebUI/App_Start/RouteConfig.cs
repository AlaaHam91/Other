using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //  URL/
            routes.MapRoute(
                      null,
                      "",
                      new
                      {
                          controller = "Book",
                          action = "List",
                          specilization = (string)null,
                          pagenum = 1
                      }
                      );
            // URL/BookListPage2
            routes.MapRoute(
                       null,
                       "BookListPage{pagenum}",
                        new { controller = "Book", action = "List", specilization = (string)null }
                       );

            // URL/IT
            routes.MapRoute(
                      null,
                      "{specilization}",
                      new { controller = "Book", action = "List", pagenum = 1 }
                      );

            // URL/IT/Page2
            routes.MapRoute(
                     null,
                     "{specilization}/Page{pagenum}",
                      new { controller = "Book", action = "List" }, new { pagenum = @"\d+" }
                     );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "List", id = UrlParameter.Optional }
            );



        }
    }
}
