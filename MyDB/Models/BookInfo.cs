using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDB.Models
{
    public class BookInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal? Price { get; set; }

        public decimal? Rate { get; set; }
        public int? SalePrice { get; set; } 
        public int? QuantityStock { get; set; }

        public int? QuantityImport { get; set; }

        public DateTime? DateImport { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public string Size { get; set; }

        public string Weight { get; set; }

        public DateTime? DatePub { get; set; }

        public String Slug { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? UpdateBy { get; set; }


        public string MetaDesc { get; set; }


        public string MetaKey { get; set; }

        public int? Status { get; set; }
    }
}
