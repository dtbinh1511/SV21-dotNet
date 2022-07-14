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
    public class PostController : Controller
    {
        private PostDAO postDAO = new PostDAO();
        private LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.GetList());
        }

        // GET: Admin/Post/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.GetRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Slug = XString.Str_Slug(post.Title);
                post.CreateAt = DateTime.Now;
                post.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());



                if (postDAO.Add(post) == 1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.Type = "page";
                    linkDAO.Add(link);
                }
                TempData["Message"] = new XMessage("success", "Thêm mẫu tin thành công");
                return RedirectToAction("Index", "Post");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(post);
        }

        // GET: Admin/Post/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.GetRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {

            if (ModelState.IsValid)
            {
                post.Slug = XString.Str_Slug(post.Title);
                post.UpdateAt = DateTime.Now;
                post.UpdateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());

                if (postDAO.Update(post) == 1)
                {
                    Link link = linkDAO.GetRow("page", post.Id);
                    link.Slug = post.Slug;
                    linkDAO.Update(link);
                }
                TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");

                return RedirectToAction("Index", "Post");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");
            return View(post);
        }

        // GET: Admin/Post/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.GetRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Post post = postDAO.GetRow(id);
            Link link = linkDAO.GetRow("page", post.Id);

            if (postDAO.Delete(post) == 1)
            {
                linkDAO.Delete(link);
            }

            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Index", "Post");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Post");
            }
            Post post = postDAO.GetRow(id);
            if (post == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.UpdateAt = DateTime.Now;
            post.UpdateBy = Convert.ToInt32(Session["SessionAccountId"].ToString());
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            postDAO.Update(post);
            return RedirectToAction("Index", "Post");
        }
    }
}
