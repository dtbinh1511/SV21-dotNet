using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace websitebansach
{
    public class CartItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal SalePrice { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }


        public CartItem(int id) { }

        public CartItem(int id, string name, string image, decimal sale, int quantity)
        {
            Id = id;
            Name = name;
            Image = image;
            SalePrice = sale;
            Quantity = quantity;
            Amount = quantity * sale;
        }
    }
}