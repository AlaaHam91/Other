using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator:ICalculator 
    {
        private IDiscountHelper ds;
        private static int counter=0;
        public LinqValueCalculator(IDiscountHelper d)
        {
           Debug.WriteLine(string.Format ("The value for Object is {0}",++counter));
            ds = d;
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {

            return ds.ApplayDiscount( products.Sum(p => p.Price));
        }
    }
}