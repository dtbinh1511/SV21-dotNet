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
namespace websitebansach.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private PostDAO postDAO = new PostDAO();

        public ActionResult Index()
        {
            ViewBag.ListCategory = categoryDAO.GetList(false);
            ViewBag.ListPage = postDAO.GetList();
            List<Menu> menus = menuDAO.GetList();

            return View("Index", menus);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.GetRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }

            if (menu.ParentId != 0)
            {
                Menu parent = menuDAO.GetRow(menu.ParentId);
                ViewBag.ParentName = parent.Name;
            }
            else
            {
                ViewBag.ParentName = menu.Name;
            }
            return View(menu);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            // category
            if (!string.IsNullOrEmpty(form["AddCategory"]))
            {
                if (!string.IsNullOrEmpty(form["nameCategory"]))
                {
                    var listItem = form["nameCategory"];
                    var listId = listItem.Split(',');
                    foreach (var id in listId)
                    {
                        int cateId = int.Parse(id);
                        Category category = categoryDAO.GetRow(cateId);
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.TableId = category.Id;
                        menu.TypeMenu = "Category";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.DisplayOrder = 0;
                        menu.Status = 1;
                        menu.CreateAt = DateTime.Now;
                        menu.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                        menuDAO.Add(menu);
                    }
                    TempData["Message"] = new XMessage("success", "Thêm menu thành công");

                }
                else
                {
                    TempData["Message"] = new XMessage("danger", "Chưa chọn danh mục sản phẩm");
                }
            }

            //page
            if (!string.IsNullOrEmpty(form["AddPage"]))
            {
                if (!string.IsNullOrEmpty(form["namePage"]))
                {
                    var listItem = form["namePage"];
                    var listId = listItem.Split(',');
                    foreach (var id in listId)
                    {
                        int pageId = int.Parse(id);
                        Post page = postDAO.GetRow(pageId);
                        Menu menu = new Menu();
                        menu.Name = page.Title;
                        menu.Link = page.Slug;
                        menu.TableId = page.Id;
                        menu.TypeMenu = page.PostType;
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.DisplayOrder = 0;
                        menu.Status = 1;
                        menu.CreateAt = DateTime.Now;
                        menu.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                        menuDAO.Add(menu);
                    }
                    TempData["Message"] = new XMessage("success", "Thêm menu thành công");

                }
                else
                {
                    TempData["Message"] = new XMessage("danger", "Chưa chọn trang đơn/bài viết");
                }
            }
            //custom

            if (!string.IsNullOrEmpty(form["AddCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    var custom = form["name"];


                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    menu.TypeMenu = "Custom";
                    menu.Position = form["Position"];
                    menu.ParentId = 0;
                    menu.DisplayOrder = 0;
                    menu.Status = 1;
                    menu.CreateAt = DateTime.Now;
                    menu.CreateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());
                    menuDAO.Add(menu);

                    TempData["Message"] = new XMessage("success", "Thêm menu thành công");

                }
                else
                {
                    TempData["Message"] = new XMessage("danger", "Chưa nhập đủ thông tin menu");
                }
            }
            return RedirectToAction("Index", "Menu");
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.GetList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(menuDAO.GetList(), "DisplayOrder", "Name", 0);
            Menu menu = menuDAO.GetRow(id);
            return View("Edit", menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {

            if (ModelState.IsValid)
            {
                menu.ParentId = (menu.ParentId == null) ? 0 : menu.ParentId;
                menu.DisplayOrder = (menu.DisplayOrder == null) ? 1 : (menu.DisplayOrder + 1);
                menu.UpdateAt = DateTime.Now;
                menu.UpdateBy = (Session["SessionAccountId"].Equals("")) ? 1 : int.Parse(Session["SessionAccountId"].ToString());

                menuDAO.Update(menu);
                TempData["Message"] = new XMessage("success", "Cập nhật mẫu tin thành công");

                return RedirectToAction("Index", "Menu");
            }
            TempData["Message"] = new XMessage("warning", "Không được để trống các trường");
            //ViewBag.ListMenu = new SelectList(menuDAO.GetList(), "Id", "Name", 0);
            //ViewBag.ListOrder = new SelectList(menuDAO.GetList(), "DisplayOrder", "Name", 0);

            return View(menu);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = menuDAO.GetRow(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            if (menu.ParentId != 0)
            {
                Menu parent = menuDAO.GetRow(menu.ParentId);
                ViewBag.ParentName = parent.Name;
            }
            else
            {
                ViewBag.ParentName = menu.Name;
            }
            return View(menu);
        }

        // POST: Admin/Category/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Menu menu = menuDAO.GetRow(id);
            menuDAO.Delete(menu);
            TempData["Message"] = new XMessage("success", "Xóa mẫu tin thành công");

            return RedirectToAction("Index", "Menu");
        }



        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Menu");
            }
            Menu menu = menuDAO.GetRow(id);
            if (menu == null)
            {
                TempData["Message"] = new XMessage("danger", "Trường không tồn tại");

                return RedirectToAction("Index", "Menu");
            }
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = Convert.ToInt32(Session["SessionAccountId"].ToString());
            menuDAO.Update(menu);
            TempData["Message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Menu");

        }
    }
}
