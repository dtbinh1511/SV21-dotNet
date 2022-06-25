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
    public class NewBookController : Controller
    {
        BookDAO bookDAO = new BookDAO();
        // GET: NewBook
        public ActionResult Index(int? page)
        {

            if (page == null) page = 1;
            int pageSize = 12;
            int pageNumber = page ?? 1;
            IPagedList<Book> books = bookDAO.GetNewBook(pageSize, pageNumber);
            return View(books);
        }
    }
}