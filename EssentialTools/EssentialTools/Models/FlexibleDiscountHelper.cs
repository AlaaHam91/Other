using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class FlexibleDiscountHelper : IDiscountHelper
    {
        public decimal ApplayDiscount(decimal TotalParam)
        {
            decimal discount = TotalParam > 100 ? 50:20;
            return TotalParam - discount;

        }
    }
}