namespace MyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        

        [StringLength(255)]
        public string Slug { get; set; }

        public DateTime? TimeOrder { get; set; }

        [StringLength(100)]
        public string ReceiverName { get; set; }

        [StringLength(15)]
        public string ReceiverPhone { get; set; }

        [StringLength(500)]
        public string ReceiverAddress { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? UpdateBy { get; set; }

        public int? Status { get; set; }

        [StringLength(255)]
        public string PayMethod { get; set; }
    }
}
