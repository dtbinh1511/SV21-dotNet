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
    public class PublisherController : Controller
    {
        private PublisherDAO publisherDAO = new PublisherDAO();

        public ActionResult Index()
        {
            return View(publisherDAO.GetList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = publisherDAO.GetRow(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                publisher.Slug = XString.Str_Slug(publisher.Name);
                publisher.CreateAt = DateTime.Now;
                publisher.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                
                publisherDAO.Add(publisher);
                TempData["Message"] = new XMessage("success", "Thêm mẫu tin thành công");
                return RedirectToAction("Index", "Publisher");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(publisher);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(publisherDAO.GetList(), "DisplayOrder", "Name", 0);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = publisherDAO.GetRow(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Publisher publisher)
        {

            if (ModelState.IsValid)
            {
                publisher.Slug = XString.Str_Slug(publisher.Name);
                publisher.UpdateAt = DateTime.Now;
                publisher.UpdateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                
                publisherDAO.Update(publisher);
                TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");

                return RedirectToAction("Index", "Publisher");
            }
            ViewBag.ListOrder = new SelectList(publisherDAO.GetList(), "DisplayOrder", "Name", 0);
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(publisher);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = publisherDAO.GetRow(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Publisher publisher = publisherDAO.GetRow(id);

            publisherDAO.Delete(publisher);
            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Index", "Publisher");
        }


        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Publisher");
            }
            Publisher publisher = publisherDAO.GetRow(id);
            if (publisher == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Publisher");
            }
            publisher.Status = (publisher.Status == 1) ? 2 : 1;
            publisher.UpdateAt = DateTime.Now;
            publisher.UpdateBy = Convert.ToInt32(Session["SessionAccountId"].ToString());
            
            publisherDAO.Update(publisher);
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Publisher");
        }
    }
}
