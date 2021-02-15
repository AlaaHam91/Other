using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CLanguageFeatures.Models
{
    public class Product
    {
        public string name { get; set; }
        public int id { get; set; }
        public string category { get; set; }

        public double price { get; set; }
       /* public string Gname()
        { return name; }
        public void Sname(string new_name)
        { this.name = new_name; }
        public string GSname
        {
            get { return this.name; }
            set { this.name = value; }
        }*/
    }
}