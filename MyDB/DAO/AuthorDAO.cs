using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB.Models;
namespace MyDB.DAO
{
    public class AuthorDAO
    {
        private DBContext db = new DBContext();
        public List<Author> GetList()
        {
            return db.Authors.ToList();
        }

        public Author GetRow(int? id)
        {
            return (id == null) ? null : db.Authors.Find(id);

        }
        public Author GetRow(string slug)
        {
            return db.Authors
               .Where(m => m.Slug == slug && m.Status == 1)
               .FirstOrDefault();
        }

        public int Add(Author entity)
        {
            db.Authors.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Author entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Author entity)
        {
            db.Authors.Remove(entity);
            return db.SaveChanges();
        }

    }
}
