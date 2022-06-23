using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;
using websitebansach.Library;
namespace websitebansach.Controllers
{
    public class GiohangController : Controller
    {
        BookDAO bookDAO = new BookDAO();
        XCart xCart = new XCart();
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO = new OrderDetailDAO();

        public ActionResult Index()
        {
            List<CartItem> carts = xCart.GetCart();

            return View(carts);
        }

        public ActionResult CartAdd(int productId)
        {
            Book book = bookDAO.GetRow(productId);
            decimal sale = book.Price - (book.Price * book.Rate);
            CartItem cartItem = new CartItem(book.Id, book.Name, book.Image, sale, 1);

            List<CartItem> carts = xCart.AddCart(cartItem, productId);

            return RedirectToAction("Index", "Giohang");
        }

        public ActionResult CartDelete(int productId)
        {
            xCart.DeleteCart(productId);

            return RedirectToAction("Index", "Giohang");
        }

        [HttpPost]
        public ActionResult CartUpdate(FormCollection form)
        {
            string[] quantities = form.GetValues("quantitySold");

            if (!string.IsNullOrEmpty(form["plusQuantity"]))
            {
                int id = Convert.ToInt32(form["plusQuantity"]);
                xCart.UpdateCart(quantities, "plus", id);
            }


            if (!string.IsNullOrEmpty(form["minusQuantity"]))
            {
                int id = Convert.ToInt32(form["minusQuantity"]);
                xCart.UpdateCart(quantities, "minus",id);
            }



            return RedirectToAction("Index", "Giohang");

        }

        public ActionResult ThanhToan()
        {
            List<CartItem> carts = xCart.GetCart();


            if (Session["CustomerAccount"].Equals(""))
            {
                return Redirect("~/dang-nhap");
            }

            int userId = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.GetRow(userId);
            ViewBag.UserCustomer = user;
            return View("ThanhToan", carts);
        }

        public ActionResult DatMua(FormCollection form)
        {
            int userId = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.GetRow(userId);

            //lay thong tin
            String name = form["Name"];
            String phone = form["Phone"];
            String address = form["Address"];
            String note = form["Note"];
            String pay = form["Payment"];

            //tao don hang
            Order order = new Order();
            order.UserId = userId;
            order.TimeOrder = DateTime.Now;
            order.ReceiverName = name;
            order.ReceiverPhone = phone;
            order.ReceiverAddress = address;
            order.Note = note;
            order.CreateAt = DateTime.Now;
            order.PayMethod = pay;
            //order.CreateBy = int.Parse(Session["AdminId"].ToString());
            order.Status = 1;
            if (orderDAO.Add(order) == 1)
            {
                List<CartItem> carts = xCart.GetCart();
                foreach (CartItem item in carts)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.BookId = item.Id;
                    orderDetail.Price = item.SalePrice;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Amount = item.Amount;
                    orderDetailDAO.Add(orderDetail);
                }
                Session["MyCart"] = "";
            }

            return Redirect("~/thanh-cong");
        }
        public ActionResult ThanhCong()
        {
            return View();
        }
    }
}