using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class MinimumDiscountHelper : IDiscountHelper
    {

        public decimal ApplayDiscount(decimal TotalParam)
        {
            // throw new NotImplementedException();
            if (TotalParam < 0) throw new ArgumentOutOfRangeException();
            else if (TotalParam > 100)
                return TotalParam * 0.9M;
            else if (TotalParam >= 10 && TotalParam <= 100)
                return TotalParam - 5;
            else return TotalParam;
        }
    }
    
}