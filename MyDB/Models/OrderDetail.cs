namespace MyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int OrderId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? Amount { get; set; }
    }
}
