using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;
using websitebansach.Library;
using System.Security.Cryptography;
using System.Text;

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
                if (GetMD5(password).Equals(user.Password))
                {
                    Session["CustomerAccount"] = user;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                User user = userDAO.GetRow(userInfo.Email);
                if (user == null)
                {
                    String pass = userInfo.Password;
                    String confPass = userInfo.ConfirmPassword;
                    if (pass.Equals(confPass))
                    {
                        user = new User();
                        user.Email = userInfo.Email;
                        user.Address = userInfo.Address;
                        user.CreateAt = DateTime.Now;
                        user.CreateBy = 1;
                        user.Fullname = userInfo.Fullname;
                        user.Gender = userInfo.Gender;
                        user.Phone = userInfo.Phone;
                        user.Role = false;
                        user.Status = 1;
                        user.Password = GetMD5(userInfo.Password);
                        userDAO.Add(user);
                        return RedirectToAction("Dangnhap", "Khachhang");

                    }
                    else
                    {
                        XMessage message = new XMessage("danger", "Mật khẩu không trùng khớp");
                        return View("Dangky", "Khachhang");
                    }
                }
                else
                {
                    XMessage message = new XMessage("danger", "Email đã tồn tại");
                    return View("Dangky", "Khachhang");
                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["CustomerAccount"] = "";
            Session["MyCart"] = "";
            return Redirect("~/dang-nhap");
        }



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}