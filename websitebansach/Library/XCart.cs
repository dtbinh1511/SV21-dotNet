using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;
namespace websitebansach
{
    public class XCart
    {
        BookDAO bookDAO = new BookDAO();

        public List<CartItem> AddCart(CartItem cartItem, int productId)
        {

            List<CartItem> carts = new List<CartItem>();
            if (GetCart() == null)
            {

                carts.Add(cartItem);
                System.Web.HttpContext.Current.Session["MyCart"] = carts;
            }
            else
            {
                carts = GetCart();
                int count = carts.Where(c => c.Id == productId).Count();
                if (count != 0)
                {
                    int vt = 0;
                    foreach (CartItem item in carts)
                    {
                        if (item.Id == productId)
                        {
                            carts[vt].Quantity += 1;
                            carts[vt].Amount = carts[vt].Quantity * carts[vt].SalePrice;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = carts;
                }
                else
                {
                    carts.Add(cartItem);
                    System.Web.HttpContext.Current.Session["MyCart"] = carts;
                }

            }
            return carts;
        }

        public void UpdateCart(int productId,int quantity)
        {

            List<CartItem> carts = GetCart();
            int vt = 0;
            foreach (CartItem item in carts)
            {
                if (item.Id == productId)
                {
                    carts[vt].Quantity = quantity;
                    carts[vt].Amount = carts[vt].Quantity * carts[vt].SalePrice;

                    break;
                }
                vt++;
            }
            System.Web.HttpContext.Current.Session["MyCart"] = carts;


        }

        public void DeleteCart(int productId)
        {
            if (GetCart() != null)
            {
                List<CartItem> carts = GetCart();
                int vt = 0;
                foreach (CartItem item in carts)
                {
                    if (item.Id == productId)
                    {
                        carts.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                System.Web.HttpContext.Current.Session["MyCart"] = carts;
            }



        }

        public List<CartItem> GetCart()
        {
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                return null;

            }
            return (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
        }

    }
}