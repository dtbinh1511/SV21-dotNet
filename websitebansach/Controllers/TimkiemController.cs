using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MyDB.DAO;
using MyDB.Models;
namespace websitebansach.Controllers
{
    public class TimkiemController : Controller
    {
        BookDAO bookDAO = new BookDAO();
        // GET: TimKiem
        public ActionResult Index(string search, int? page)

        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = page ?? 1;

            IPagedList<Book> books = bookDAO.GetListByName(search, pageSize, pageNumber);
            List<Book> bookss = bookDAO.GetListByName(search);

            ViewBag.Keyword = search;
            ViewBag.Count = bookss.Count;
            return View("Index", books);
        }
    }
}