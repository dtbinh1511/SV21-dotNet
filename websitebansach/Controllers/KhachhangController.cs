using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;

namespace websitebansach.Controllers
{
    public class KhachhangController : Controller
    {
        UserDAO userDAO = new UserDAO();

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection form)
        {
            string email = form["email"];
            string password = form["password"];
            User user = userDAO.GetRow(email);
            String strError = "";
            if (user == null)
            {
                strError = "Email không tồn tại";
            }
            else
            {
                if (password.Equals(user.Password))
                {
                    Session["UserCustomer"] = user;
                    Session["CustomerId"] = user.Id;
                    return Redirect("~/");
                }
                else
                {
                    strError = "Mật khẩu không đúng";
                }
            }
            ViewBag.Error = strError;
            return View("DangNhap");
        }

        public ActionResult DangKy()
        {
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["MyCart"] = "";
            return Redirect("~/dang-nhap");
        }
    }
}