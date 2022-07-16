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
using System.Web.Security;

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
            if (user == null)
            {
                TempData["Message"] = new XMessage("danger", "Email không tồn tại");
            }
            else
            {
                if (GetMD5(password).Equals(user.Password))
                {
                    Session["SessionAccount"] = user;
                    Session["SessionAccountId"] = user.Id;
                    if (user.Role == true)
                    {
                        return RedirectToAction("Index", "Admin/Book");

                    }
                    else
                    {
                        return Redirect("~/");
                    }
                }
                else
                {
                    TempData["Message"] = new XMessage("danger", "Mật khẩu không đúng");
                }
            }
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
                        TempData["Message"] = new XMessage("danger", "Mật khẩu không trùng khớp");
                        return RedirectToAction("Dangky", "Khachhang");
                    }
                }
                else
                {
                    TempData["Message"] = new XMessage("danger", "Email đã tồn tại");
                    return RedirectToAction("Dangky", "Khachhang");
                }
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["SessionAccount"] = "";
            Session["MyCart"] = "";
            return RedirectToAction("Dangnhap", "Khachhang");
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