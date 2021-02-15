using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShopingCart
    {
        //  private LinqValueCalculator c;
        private ICalculator c;
        public IEnumerable<Product> p  { get;set; }
        public ShopingCart(ICalculator icparam)
        {

            c = icparam;
        }
        public decimal TotalSum()
        {
            return c.ValueProducts(p);
        }

    }
}