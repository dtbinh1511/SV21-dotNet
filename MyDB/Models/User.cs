namespace MyDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Fullname { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        public bool? Gender { get; set; }

        public bool? Role { get; set; }


        [StringLength(255)]
        public string Slug { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? UpdateBy { get; set; }

        public int? Status { get; set; }
    }
}
