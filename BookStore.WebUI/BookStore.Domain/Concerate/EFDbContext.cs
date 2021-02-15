using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookStore.Domain.Entity;
using System.Configuration;

namespace BookStore.Domain.Concerate
{
    class EFDbContext : DbContext
    {
        public EFDbContext ():base("name = EFDbContext")
        {

        }
        public DbSet<Book> Books { get; }
    }
}
