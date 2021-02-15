﻿using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Abstract
{
  public  interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingdetails);
      
    }
}