using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDB.Models;
namespace MyDB.DAO
{
    public class OrderDAO
    {
        private DBContext db = new DBContext();


        public List<Order> GetList()
        {
            return db.Orders.ToList();
        }

      

        public Order GetRow(int? id)
        {
            return (id == null) ? null : db.Orders.Find(id);

        }

        public int Add(Order entity)
        {
            db.Orders.Add(entity);
            return db.SaveChanges();
        }

        public int Update(Order entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Order entity)
        {
            db.Orders.Remove(entity);
            return db.SaveChanges();
        }
    }
}
