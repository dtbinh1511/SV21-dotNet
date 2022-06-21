using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB.Models;
namespace MyDB.DAO
{
    public class SlideDAO
    {
        private DBContext db = new DBContext();
        public List<Slide> GetList()
        {
            return db.Slides.ToList();
        }
        public List<Slide> GetListWithCondition()
        {
            return db.Slides
                .Where(m => m.Status == 1)
                .ToList();
        }
        public Slide GetRow(int? id)
        {
            return (id == null) ? null : db.Slides.Find(id);

        }

        public int Add(Slide entity)
        {
            db.Slides.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Slide entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Slide entity)
        {
            db.Slides.Remove(entity);
            return db.SaveChanges();
        }
    }
}
