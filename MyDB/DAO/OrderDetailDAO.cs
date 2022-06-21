using MyDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDB.DAO
{
    public class OrderDetailDAO
    {
        private DBContext db = new DBContext();

        public List<OrderDetail> GetList(int? orderId)
        {
            return db.OrderDetails.Where(x => x.OrderId == orderId).ToList();
        }

        public OrderDetail GetRow(int? id)
        {
            return (id == null) ? null : db.OrderDetails.Find(id);

        }


        public int Add(OrderDetail entity)
        {
            db.OrderDetails.Add(entity);
            return db.SaveChanges();
        }

        public int Update(OrderDetail entity)
        {

            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(OrderDetail entity)
        {
            db.OrderDetails.Remove(entity);
            return db.SaveChanges();
        }
    }
}
