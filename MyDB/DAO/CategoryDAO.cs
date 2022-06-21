using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using MyDB.Models;

namespace MyDB.DAO
{
    public class CategoryDAO
    {
        private DBContext db = new DBContext();
        public List<Category> GetList(bool isShow)
        {
            if (isShow)
            {
                return db.Categories.ToList();
            }
            return db.Categories
                 .Where(m => m.Status == 1)
                 .OrderBy(m => m.DisplayOrder).ToList();
        }

        public Category GetRow(int? id)
        {
            return (id == null) ? null : db.Categories.Find(id);

        }
        public Category GetRow(string slug)
        {
            return db.Categories
                .Where(m => m.Slug == slug && m.Status == 1)
                .FirstOrDefault();
        }
        public int Add(Category entity)
        {
            db.Categories.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Category entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Category entity)
        {
            db.Categories.Remove(entity);
            return db.SaveChanges();
        }
    }
}
