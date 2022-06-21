using MyDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDB.DAO
{
    public class LinkDAO
    {
        private DBContext db = new DBContext();       

        public Link GetRow(string typeLink, int tableId)
        {
            return db.Links.Where(m => m.TableId == tableId && m.Type == typeLink).FirstOrDefault();

        }
        public Link GetRow(string slug)
        {
            return db.Links.Where(m => m.Slug == slug).FirstOrDefault();

        }
        public int Add(Link entity)
        {
            db.Links.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Link entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Link entity)
        {
            db.Links.Remove(entity);
            return db.SaveChanges();
        }
    }
}
