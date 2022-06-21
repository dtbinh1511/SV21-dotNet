namespace MyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        public decimal Price { get; set; }

        public decimal Rate { get; set; }

        public int? QuantityStock { get; set; }

        public int? QuantityImport { get; set; }

        public DateTime? DateImport { get; set; }

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public int? PublisherId { get; set; }

        [StringLength(100)]
        public string Size { get; set; }

        [StringLength(10)]
        public string Weight { get; set; }

        public DateTime? DatePub { get; set; }

        

        [StringLength(255)]
        public string Slug { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? UpdateBy { get; set; }

        [StringLength(255)]
        public string MetaDesc { get; set; }

        [StringLength(255)]
        public string MetaKey { get; set; }

        public int? Status { get; set; }
    }
}
