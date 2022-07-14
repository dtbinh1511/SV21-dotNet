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
using System.IO;

namespace websitebansach.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        private SlideDAO slideDAO = new SlideDAO();

        // GET: Admin/Slide
        public ActionResult Index()
        {
            return View(slideDAO.GetList());
        }

        // GET: Admin/Slide/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = slideDAO.GetRow(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Admin/Slide/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slide/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {

                slide.CreateAt = DateTime.Now;
                slide.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());

                //upload file
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {

                        string pathDir = "~/Assets/Client/Images/slides/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), fileImg.FileName);
                        //upload
                        fileImg.SaveAs(pathImg);
                        // luu file
                        slide.Image = fileImg.FileName;
                    }
                }
                //End file

                slideDAO.Add(slide);
                TempData["Message"] = new XMessage("success", "Thêm mẫu tin thành công");
                return RedirectToAction("Index", "Slide");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");
            return View(slide);
        }

        // GET: Admin/Slide/Edit
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = slideDAO.GetRow(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            Session["SlideImg"] = slide.Image;
            return View(slide);
        }

        // POST: Admin/Slide/Edit        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slide slide)
        {

            if (ModelState.IsValid)
            {
                slide.UpdateAt = DateTime.Now;
                slide.UpdateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                string pathDir = "~/Assets/Client/Images/slides/";

                //upload file
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        // xoa hinh cu
                        string delImgPath = Path.Combine(Server.MapPath(pathDir), (string)Session["SlideImg"]);
                        System.IO.File.Delete(delImgPath);


                        // luu file
                        slide.Image = fileImg.FileName;

                        //upload
                        string pathImg = Path.Combine(Server.MapPath(pathDir), fileImg.FileName);
                        fileImg.SaveAs(pathImg);
                        slideDAO.Update(slide);
                        TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");
                    }

                }
                //End file             
                Session.Remove("SlideImg");
                return RedirectToAction("Index", "Slide");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(slide);
        }

        // GET: Admin/Slide/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = slideDAO.GetRow(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slide/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Slide slide = slideDAO.GetRow(id);
            string pathDir = "~/Assets/Client/Images/slides/";

            if (slide.Image != null)
            {
                string delImgPath = Path.Combine(Server.MapPath(pathDir), slide.Image);
                System.IO.File.Delete(delImgPath);
            }
            slideDAO.Delete(slide);
            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");
            return RedirectToAction("Index", "Slide");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Slide");
            }
            Slide slide = slideDAO.GetRow(id);
            if (slide == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Slide");
            }
            slide.Status = (slide.Status == 1) ? 2 : 1;
            slide.UpdateAt = DateTime.Now;
            slide.UpdateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            slideDAO.Update(slide);
            return RedirectToAction("Index", "Slide");
        }
    }
}
