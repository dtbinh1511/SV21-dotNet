using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;
namespace websitebansach.Controllers
{
    public class ModuleController : Controller

    {
        private MenuDAO menuDAO = new MenuDAO();
        private SlideDAO slideDAO = new SlideDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private BookDAO bookDAO = new BookDAO();
        AuthorDAO authorDAO = new AuthorDAO();
        PublisherDAO publisherDAO = new PublisherDAO();
        public ActionResult MainMenu()
        {
            List<Menu> menus = menuDAO.GetList("mainmenu", 0);
            return View("MainMenu", menus);
        }

        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.GetRow(id);
            List<Menu> list = menuDAO.GetList("mainmenu", id);

            if (list.Count == 0)
            {
                return View("MainMenuSub1", menu);
            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);
            }
        }


        public ActionResult MobileMainMenu()
        {
            List<Menu> menus = menuDAO.GetList("Category");
            return View("MobileMainMenu", menus);
        }      

        public ActionResult Footer()
        {
            List<Menu> menus = menuDAO.GetList("footer", 0);
            return View("Footer", menus);
        }
        public ActionResult FooterSub(int id)
        {
            Menu menu = menuDAO.GetRow(id);
            List<Menu> menus = menuDAO.GetList("footer", id);
            if (menus.Count == 0)
            {
                return View("FooterSub1", menu);
            }
            else
            {

                ViewBag.Footer = menu;
                return View("FooterSub2", menus);
            }
        }

        public ActionResult Slider()
        {
            List<Slide> slides = slideDAO.GetListWithCondition();
            ViewBag.ListCategory = categoryDAO.GetList(false);
            return View("Slider", slides);
        }

        public ActionResult NewBook()
        {
            List<Book> books = bookDAO.GetNewBook();
            return View("NewBook", books);
        }
        public ActionResult Seller()
        {
            List<Book> books = bookDAO.GetSeller();
            return View("Seller", books);
        }
        public ActionResult ComingSoon()
        {
            List<Book> books = bookDAO.GetComingSoon();
            return View("ComingSoon", books);
        } public ActionResult About()
        {
            List<Category> list = categoryDAO.GetList(false);
            return View("About", list);
        }
     
        public ActionResult ListCategory()

        {
            List<Category> list = categoryDAO.GetList(false);
            return View("ListCategory", list);
        }
        public ActionResult ListAuthor()
        {
            List<Author> list = authorDAO.GetList();

            return View("ListAuthor", list);
        }
        public ActionResult ListPublisher()
        {
            List<Publisher> list = publisherDAO.GetList();

            return View("ListPublisher", list);
        }

        public ActionResult SameAuthor(int? id)
        {
            List<Book> books = bookDAO.GetListByAuId(id);
            return View("SameAuthor", books);
        }
       
        public ActionResult Support()
        {
            XCart xCart = new XCart();
            List<CartItem> carts = xCart.GetCart();

            return View("Support",carts);
        }
    }
}