using MyDB.DAO;
using MyDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using websitebansach.Library;

namespace websitebansach.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        private BookDAO bookDAO = new BookDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private PublisherDAO publisherDAO = new PublisherDAO();
        private AuthorDAO authorDAO = new AuthorDAO();

        // GET: Admin/Book
        public ActionResult Index()
        {

            List<BookInfo> list = bookDAO.GetListJoin();
            return View("Index", list);
        }

        // GET: Admin/Book/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfo book = bookDAO.GetRowJoin(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Admin/Book/Create
        public ActionResult Create()
        {
            //sap xep 

            ViewBag.ListCate = new SelectList(categoryDAO.GetList(true), "Id", "Name", 0);
            ViewBag.ListAu = new SelectList(authorDAO.GetList(), "Id", "Name", 0);
            ViewBag.ListPub = new SelectList(publisherDAO.GetList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Book/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Slug = XString.Str_Slug(book.Name);

                book.CreateAt = DateTime.Now;
                book.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                book.Rate = book.Rate / 100;
                //upload file
                var fileImg = Request.Files["BookImage"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {

                        string pathDir = "~/Assets/Client/Images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), fileImg.FileName);
                        //upload
                        fileImg.SaveAs(pathImg);
                        // luu file
                        book.Image = fileImg.FileName;

                        bookDAO.Add(book);
                        TempData["Message"] = new XMessage("success", "Thêm mẫu tin thành công");
                    }
                }
                //End file
                else
                {
                    TempData["Message"] = new XMessage("warning", "Ảnh phải kiểu .png, .jpg, .jpeg");
                }
                return RedirectToAction("Index", "Book");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");


            //ViewBag.ListCate = new SelectList(categoryDAO.GetList(true), "Id", "Name", 0);
            //ViewBag.ListAu = new SelectList(authorDAO.GetList(), "Id", "Name", 0);
            //ViewBag.ListPub = new SelectList(publisherDAO.GetList(), "Id", "Name", 0);
            return View(book);
        }

        // GET: Admin/Book/Edit
        public ActionResult Edit(int? id)
        {

            ViewBag.ListCate = new SelectList(categoryDAO.GetList(true), "Id", "Name", 0);
            ViewBag.ListAu = new SelectList(authorDAO.GetList(), "Id", "Name", 0);
            ViewBag.ListPub = new SelectList(publisherDAO.GetList(), "Id", "Name", 0);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = bookDAO.GetRow(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            Session["pathImgOld"] = book.Image;
            return View(book);
        }

        // POST: Admin/Book/Edit        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {

            if (ModelState.IsValid)
            {
                book.Slug = XString.Str_Slug(book.Name);
                book.UpdateAt = DateTime.Now;
                book.UpdateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                book.Rate = book.Rate / 100;


                //upload file
                var fileImg = Request.Files["BookImage"];
                if (fileImg.ContentLength != 0)
                {
                    string pathDir = "~/Assets/Client/Images/products/";
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {

                        // xoa hinh cu
                        string delImgPath = Path.Combine(Server.MapPath(pathDir), Session["pathImgOld"].ToString());
                        System.IO.File.Delete(delImgPath);

                        // luu file
                        book.Image = fileImg.FileName;
                        //upload
                        string pathImg = Path.Combine(Server.MapPath(pathDir), fileImg.FileName);
                        fileImg.SaveAs(pathImg);

                        bookDAO.Update(book);
                        TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");
                        Session.Remove("pathImgOld");
                    }
                    else
                    {
                        TempData["Message"] = new XMessage("warning", "Ảnh phải kiểu .png, .jpg, .jpeg");

                    }

                }
                //End file  


                return RedirectToAction("Index", "Book");
            }

            //ViewBag.ListCate = new SelectList(categoryDAO.GetList(true), "Id", "Name", 0);
            //ViewBag.ListAu = new SelectList(authorDAO.GetList(), "Id", "Name", 0);
            //ViewBag.ListPub = new SelectList(publisherDAO.GetList(), "Id", "Name", 0);
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");

            return View(book);
        }

        // GET: Admin/Book/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookInfo bookInfo = bookDAO.GetRowJoin(id);

            if (bookInfo == null)
            {
                return HttpNotFound();
            }
            return View(bookInfo);
        }

        // POST: Admin/Book/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Book book = bookDAO.GetRow(id);
            string pathDir = "~/Assets/Client/Images/products/";

            if (book.Image != null)
            {
                string delImgPath = Path.Combine(Server.MapPath(pathDir), book.Image);
                System.IO.File.Delete(delImgPath);
            }

            bookDAO.Delete(book);
            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");

            return RedirectToAction("Index", "Book");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Book");
            }
            Book book = bookDAO.GetRow(id);
            if (book == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Book");
            }
            book.Status = (book.Status == 1) ? 2 : 1;
            book.UpdateAt = DateTime.Now;
            book.UpdateBy = Convert.ToInt32(Session["SessionAccountId"].ToString());
            bookDAO.Update(book);
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Book");
        }


        public String BookImg(int? bid)
        {
            Book book = bookDAO.GetRow(bid);
            if (book == null)
            {
                return " ";
            }
            else
            {
                return book.Image;
            }
        }

        public String BookName(int? bid)
        {
            Book book = bookDAO.GetRow(bid);
            if (book == null)
            {
                return " ";
            }
            else
            {
                return book.Name;
            }
        }
    }
}