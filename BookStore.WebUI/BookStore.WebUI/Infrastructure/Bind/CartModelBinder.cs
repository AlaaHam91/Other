using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Infrastructure.Bind
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionkey= "Cart";
        public object BindModel(ControllerContext ContrallerContext, ModelBindingContext bindingContext)
        {

            Cart cart =null;
            if (ContrallerContext.HttpContext.Session != null)
                cart = (Cart)ContrallerContext.HttpContext.Session[sessionkey];

            if (cart == null)
            {
                cart = new Cart();
               if (ContrallerContext.HttpContext.Session != null)
                    ContrallerContext.HttpContext.Session[sessionkey] = cart;
            }

            return cart;
        }

    }
}