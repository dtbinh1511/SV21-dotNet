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
    public class CategoryController : Controller
    {
        private CategoryDAO categoryDAO = new CategoryDAO();
        private LinkDAO linkDAO = new LinkDAO();

        public ActionResult Index()
        {
            return View(categoryDAO.GetList(true));
        }

        // GET: Admin/Category/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.GetRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if (category.ParentId != 0)
            {
                Category parent = categoryDAO.GetRow(category.ParentId);
                ViewBag.ParentName = parent.Name;
            }
            else
            {
                ViewBag.ParentName = category.Name;
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            //dropdown
            ViewBag.ListOrder = new SelectList(categoryDAO.GetList(true), "DisplayOrder", "Name", 0);
            ViewBag.ListCate = new SelectList(categoryDAO.GetList(true), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = XString.Str_Slug(category.Name);
                category.ParentId = (category.ParentId == null) ? 0 : category.ParentId;
                category.DisplayOrder = (category.DisplayOrder == null) ? 1 : (category.DisplayOrder + 1);
                category.CreateAt = DateTime.Now;
                category.CreateBy = (Session["AdminId"].Equals("")) ? 1 : int.Parse(Session["AdminId"].ToString());
                if (categoryDAO.Add(category) == 1)
                {
                    Link link = new Link();
                    link.Slug = category.Slug;
                    link.TableId = category.Id;
                    link.Type = "category";
                    linkDAO.Add(link);
                }
                TempData["Message"] = new XMessage("success", "Thêm mẫu tin thành công");

                return RedirectToAction("Index", "Category");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");
            return View(category);
        }

        // GET: Admin/Category/Edit
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCate = new SelectList(categoryDAO.GetList(true), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(categoryDAO.GetList(true), "DisplayOrder", "Name", 0);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.GetRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                category.Slug = XString.Str_Slug(category.Name);
                category.ParentId = (category.ParentId == null) ? 0 : category.ParentId;
                category.DisplayOrder = (category.DisplayOrder == null) ? 1 : (category.DisplayOrder + 1); // bug lưu
                category.UpdateAt = DateTime.Now;
                category.UpdateBy = (Session["AdminId"].Equals("")) ? 1 : int.Parse(Session["AdminId"].ToString());

                if (categoryDAO.Update(category) == 1)
                {
                    Link link = linkDAO.GetRow("category", category.Id);
                    link.Slug = category.Slug;

                    linkDAO.Update(link);
                    TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");
                    return RedirectToAction("Index", "Category");
                }
            }

            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(category);
        }

        // GET: Admin/Category/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.GetRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if (category.ParentId != 0)
            {
                Category parent = categoryDAO.GetRow(category.ParentId);
                ViewBag.ParentName = parent.Name;
            }
            else
            {
                ViewBag.ParentName = category.Name;
            }
            return View(category);
        }

        // POST: Admin/Category/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Category category = categoryDAO.GetRow(id);
            Link link = linkDAO.GetRow("category", category.Id);
            if (categoryDAO.Delete(category) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");

            return RedirectToAction("Index", "Category");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.GetRow(id);
            if (category == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Category");
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.UpdateAt = DateTime.Now;
            category.UpdateBy = Convert.ToInt32(Session["AdminId"].ToString());
            categoryDAO.Update(category);
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Category");
        }
    }
}
