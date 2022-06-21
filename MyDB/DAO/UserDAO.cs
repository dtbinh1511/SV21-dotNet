using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyDB.Models;
namespace MyDB.DAO
{
    public class UserDAO
    {
        private DBContext db = new DBContext();
        public List<User> GetList()
        {
            return db.Users.ToList();
        }

        public User GetRow(int? id)
        {
            return (id == null) ? null : db.Users.Find(id);

        }

        public User GetRow(string email)
        {
            return db.Users.Where(x => x.Status ==1 && x.Email == email).FirstOrDefault();
        }

        public int Add(User entity)
        {
            db.Users.Add(entity);
            return db.SaveChanges();
        }

        public int Update(User entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(User entity)
        {
            db.Users.Remove(entity);
            return db.SaveChanges();
        }
    }
}
