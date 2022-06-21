using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB.Models;
namespace MyDB.DAO
{
    public class PostDAO
    {
        private DBContext db = new DBContext();
        public List<Post> GetList()
        {
            return db.Posts.ToList();
        }
        public List<Post> GetList(string type)
        {
            return db.Posts
                .Where(m => m.PostType == type)
                .ToList();
        }

        public Post GetRow(int? id)
        {
            return (id == null) ? null : db.Posts.Find(id);

        }

        public int Add(Post entity)
        {
            db.Posts.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Post entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Post entity)
        {
            db.Posts.Remove(entity);
            return db.SaveChanges();
        }

    }

}
