using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Domain.Entity
{
    public class Book
    {
        [Key]
       // [HiddenInput(DisplayValue =false)]
        public int ISBN { get; set; }
        [Required(ErrorMessage ="Please enter the title")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter the Description")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter the price")]
        [Range(0,1000,ErrorMessage ="Please enter a valid price")]
        public decimal Price { get; set; }
        [Required]
        public string Specizailation { get; set; }
        public string Author { get; set; }

        public DateTime VersionDate { get; set; }
        [Required]
        public Byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }


    }
}
