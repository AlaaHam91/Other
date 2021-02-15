using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplayDiscount (decimal TotalParam);
    }
    public class Discount : IDiscountHelper
    {
        //   public decimal DiscountSize { get; set; }
        public decimal DiscountSize;
        public Discount(decimal DiscountSize)
        {
            this.DiscountSize = DiscountSize;
        }
        public decimal ApplayDiscount(decimal TotalParam)
        {
            // throw new NotImplementedException();
    return TotalParam - (DiscountSize/100M * TotalParam );
        }
    }
}