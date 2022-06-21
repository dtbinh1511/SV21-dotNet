using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDB.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime? TimeOrder { get; set; }

        public string ReceiverName { get; set; }

        public string ReceiverPhone { get; set; }

        public string ReceiverAddress { get; set; }

        public string Note { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? UpdateBy { get; set; }

        public int? Status { get; set; }

        public string PayMethod { get; set; }

        public int BookId { get; set; }

        public int OrderId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? Amount { get; set; }
    }
}
