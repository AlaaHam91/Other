using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entity;
using BookStore.Domain.Concerate;
using System.Data.Entity;

namespace BookStore.Domain.Concerate
{
  public  class EFBookRepository : IBookRepository
    {
        EFDbContext context = new EFDbContext();
        
        public IEnumerable<Book> Books
        {
            get
            {
                return context.Books;
            }
        }

        public Book DeleteBook(int ISBN)
        {
            Book dbEntity = context.Books.Find(ISBN);
            if (dbEntity != null)
            {
                context.Books.Remove(dbEntity);
                context.SaveChanges();
            }
            return dbEntity;
        }

        public void SaveBook(Book book)
        {
            Book dbEntity = context.Books.Find(book.ISBN);
            if (dbEntity == null)
                context.Books.Add(book);
            else
            {
                dbEntity.Title = book.Title;
                dbEntity.Description = book.Description;
                dbEntity.Price = book.Price;
                dbEntity.Specizailation = book.Specizailation;
                dbEntity.ImageData = book.ImageData;
                dbEntity.ImageMimeType = book.ImageMimeType;
            }

            context.SaveChanges();
        }
    }
}
