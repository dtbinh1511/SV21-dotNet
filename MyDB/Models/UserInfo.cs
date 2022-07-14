using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDB.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Fullname { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool? Gender { get; set; }

        public bool? Role { get; set; }

        public string Slug { get; set; }

        public DateTime? CreateAt { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? UpdateBy { get; set; }

        public int? Status { get; set; }
    }
}
