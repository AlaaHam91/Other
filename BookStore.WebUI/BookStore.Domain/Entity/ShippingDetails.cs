using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entity
{
   public class ShippingDetails
    {
        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a first adress line")]
        [Display(Name="Adress Line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Adress Line 2")]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Please enter the city name")]
        public string City { get; set; }

        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the city name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }


    }
}
