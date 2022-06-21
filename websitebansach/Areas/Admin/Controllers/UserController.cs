using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyDB.Models;
using MyDB.DAO;
using websitebansach.Library;

namespace websitebansach.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private UserDAO userDAO = new UserDAO();

        // GET: Admin/User
        public ActionResult Index()
        {
            return View(userDAO.GetList());
        }

        // GET: Admin/User/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.GetRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            //sap xep 
            return View();
        }

        // POST: Admin/User/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Slug = XString.Str_Slug(user.Fullname);
                user.CreateAt = DateTime.Now;
                user.CreateBy = (Session["AdminId"].Equals("")) ? 1 : int.Parse(Session["AdminId"].ToString());

                userDAO.Add(user);

                TempData["Message"] = new XMessage("success", "Thêm mẫu tin thành công");
                return RedirectToAction("Index", "User");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(user);
        }

        // GET: Admin/User/Edit
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.GetRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {

            if (ModelState.IsValid)
            {
                user.Slug = XString.Str_Slug(user.Fullname);

                user.UpdateAt = DateTime.Now;
                user.UpdateBy = (Session["AdminId"].Equals("")) ? 1 : int.Parse(Session["AdminId"].ToString());

                userDAO.Update(user);
                TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");

                return RedirectToAction("Index", "User");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(user);
        }

        // GET: Admin/User/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.GetRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            User user = userDAO.GetRow(id);

            userDAO.Delete(user);
            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Index", "User");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "User");
            }
            User user = userDAO.GetRow(id);
            if (user == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "User");
            }
            user.Status = (user.Status == 1) ? 2 : 1;
            user.UpdateAt = DateTime.Now;
            user.UpdateBy = int.Parse(Session["AdminId"].ToString());
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            userDAO.Update(user);
            return RedirectToAction("Index", "User");
        }
    }
}
