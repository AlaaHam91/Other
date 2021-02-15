using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLanguageFeatures.Models
{
    public static class MyectensionMethod
    {
        public static double TotalPrices(this ShopingCart cart)
        {
            double total = 0;
            foreach (Product p in cart.products)
                total = total + p.price;
            return total;

        }

        public static double TotalPricesIE(this IEnumerable<Product> cart)
        {
            double total = 0;
            foreach (Product p in cart)
                total = total + p.price;
            return total;

        }


        public static IEnumerable<Product> FilterByCategoryIE(this IEnumerable<Product> cart ,string cat)
        {
            foreach (Product p in cart)
                if (p.category==cat)
                yield return p; 

        }
    }
}