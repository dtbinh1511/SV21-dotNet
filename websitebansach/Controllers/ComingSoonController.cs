using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.Models;
using MyDB.DAO;
using PagedList;
namespace websitebansach.Controllers
{
    public class ComingSoonController : Controller
    {
        BookDAO bookDAO = new BookDAO();

        // GET: ComingSoon
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 12;
            int pageNumber = page ?? 1;
            IPagedList<Book> books = bookDAO.GetComingSoon(pageSize, pageNumber);
            return View(books);
        }
    }
}