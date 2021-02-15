using BookStore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class BookListViewModel
    {
        public IEnumerable <Book> Books { set; get; }
        public PagingInfo paginginfo { set; get; }

        public string CurrentSpecilization  { set; get; }//للتخصص فلرة

    }
}