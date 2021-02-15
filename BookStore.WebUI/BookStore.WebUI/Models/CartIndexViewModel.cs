using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set;}
        public string returnUrl { get; set; }

    }
}