namespace MyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Link")]
    public partial class Link
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Slug { get; set; }

        public int? TableId { get; set; }

        [StringLength(50)]
        public string Type { get; set; }
    }
}
