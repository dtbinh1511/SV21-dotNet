using MyDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDB.DAO
{
    public class BookDAO
    {
        private DBContext db = new DBContext();


        public List<Book> GetList()
        {
            return db.Books.ToList();
        }
        public List<BookInfo> GetListJoin()
        {
            var multiptable = (from book in db.Books
                               join cate in db.Categories on book.CategoryId equals cate.Id
                               join author in db.Authors on book.AuthorId equals author.Id
                               join pub in db.Publishers on book.PublisherId equals pub.Id
                               select new BookInfo
                               {
                                   Id = book.Id,
                                   Name = book.Name,
                                   Description = book.Description,
                                   Image = book.Image,
                                   Price = book.Price,
                                   Rate = book.Rate,
                                   SalePrice = (int?)(book.Price - (book.Price * book.Rate)),
                                   QuantityStock = book.QuantityStock,
                                   QuantityImport = book.QuantityImport,
                                   DateImport = book.DateImport,
                                   AuthorId = author.Id,
                                   AuthorName = author.Name,
                                   CategoryId = cate.Id,
                                   CategoryName = cate.Name,
                                   PublisherId = pub.Id,
                                   PublisherName = pub.Name,
                                   Size = book.Size,
                                   Weight = book.Weight,
                                   DatePub = book.DatePub,
                                   Slug = book.Slug,
                                   CreateAt = book.CreateAt,
                                   CreateBy = book.CreateBy,
                                   UpdateAt = book.UpdateAt,
                                   UpdateBy = book.UpdateBy,
                                   MetaDesc = book.MetaDesc,
                                   MetaKey = book.MetaKey,
                                   Status = book.Status
                               }).ToList();

            return multiptable;

        }


        public List<Book> GetListByPubId(int? id)
        {
            return db.Books
               .Where(m => m.PublisherId == id)
               .ToList();
        }

        public List<Book> GetListByAuId(int? id)
        {
            return db.Books
                .Where(m => m.AuthorId == id)
                .ToList();
        }

        public List<Book> GetListByCateId(int? id)
        {
            return db.Books
                .Where(m => m.CategoryId == id)
                .ToList();
        }
        public List<Book> GetNewBook()
        {
            DateTime CurrentTime = DateTime.Now;

            return db.Books
                .Where(m => DbFunctions.DiffDays(m.DatePub, CurrentTime) > 0
                      && DbFunctions.DiffDays(m.DatePub, CurrentTime) < 200)
                .ToList();
        }
        public List<Book> GetComingSoon()
        {
            DateTime CurrentTime = DateTime.Now;
            return db.Books
                .Where(m => DbFunctions.DiffDays(m.DatePub, CurrentTime) < 0)
                .ToList();
        }
        public List<Book> GetSeller()
        {
            DateTime CurrentTime = DateTime.Now;

            return db.Books
                .OrderByDescending(m => m.QuantityImport - m.QuantityStock)
                .Take(10)
                .ToList();
        }

        public Book GetRow(int? id)
        {
            return (id == null) ? null : db.Books.Find(id);
        }

        public Book GetRow(string slug)
        {
            return db.Books
                .Where(m => m.Slug == slug)
                .FirstOrDefault();

        }
        public BookInfo GetRowJoin(int? id)
        {
            BookInfo bookInfo = new BookInfo();
            foreach (BookInfo b in GetListJoin())
            {
                if (b.Id.Equals(id))
                {
                    return b;
                }
            }
            return null;
        }

        public int Add(Book entity)
        {
            db.Books.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Book entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Book entity)
        {
            db.Books.Remove(entity);
            return db.SaveChanges();
        }
    }
}
