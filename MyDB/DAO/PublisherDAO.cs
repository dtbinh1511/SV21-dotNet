using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB.Models;
namespace MyDB.DAO
{
    public class PublisherDAO
    {
        private DBContext db = new DBContext();
        public List<Publisher> GetList()
        {
            return db.Publishers.ToList();
        }

        public Publisher GetRow(int? id)
        {
            return (id == null) ? null : db.Publishers.Find(id);

        }
        public Publisher GetRow(string slug)
        {
            return db.Publishers
               .Where(m => m.Slug == slug && m.Status == 1)
               .FirstOrDefault();
        }
        public int Add(Publisher entity)
        {
            db.Publishers.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Publisher entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Publisher entity)
        {
            db.Publishers.Remove(entity);
            return db.SaveChanges();
        }
    }
}
