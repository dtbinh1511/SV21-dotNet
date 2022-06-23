using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;
using PagedList;
namespace websitebansach.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        LinkDAO linkDAO = new LinkDAO();
        BookDAO bookDAO = new BookDAO();
        AuthorDAO authorDAO = new AuthorDAO();
        PublisherDAO publisherDAO = new PublisherDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        public ActionResult Index(string slug = null, int? page = null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = linkDAO.GetRow(slug);
                if (link != null)
                {
                    string typelink = link.Type;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "page":
                            {

                                return this.PostPage(slug, page);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }
                    }
                }
                else
                {
                    Book book = bookDAO.GetRow(slug);
                    if (book != null)
                    {
                        return this.BookDetail(book);
                    }
                    else
                    {
                        Author author = authorDAO.GetRow(slug);
                        if (author != null)
                        {
                            return this.ProductAuthor(slug, page);
                        }
                        else
                        {
                            Publisher publisher = publisherDAO.GetRow(slug);
                            if (publisher != null)
                            {
                                return this.ProductPublisher(slug, page);
                            }
                            else
                            {
                                return this.Error404(slug);
                            }
                        }
                    }

                }
            }
        }

        public ActionResult Home()
        {
            return View("Home");
        }
        public ActionResult Product()
        {
            // chua dung
            return View("Product");
        }
        public ActionResult ProductCategory(string slug, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = page ?? 1; //Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.

            Category category = categoryDAO.GetRow(slug);
            IPagedList<Book> books = bookDAO.GetListByCateId(category.Id, pageSize, pageNumber);
            ViewBag.Category = category;
            return View("ProductCategory", books);
        }
        public ActionResult ProductAuthor(string slug, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = page ?? 1; //Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.

            Author author = authorDAO.GetRow(slug);
            IPagedList<Book> books = bookDAO.GetListByAuId(author.Id, pageSize, pageNumber);

            ViewBag.Author = author;
            return View("ProductAuthor", books);
        }
        public ActionResult ProductPublisher(string slug, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            int pageNumber = page ?? 1; //Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.

            Publisher publisher = publisherDAO.GetRow(slug);
            IPagedList<Book> books = bookDAO.GetListByPubId(publisher.Id, pageSize, pageNumber);
            ViewBag.Publisher = publisher;
            return View("ProductPublisher", books);
        }
        public ActionResult BookDetail(Book book)
        {
            Category category = categoryDAO.GetRow(book.CategoryId);
            Publisher publisher = publisherDAO.GetRow(book.PublisherId);
            Author author = authorDAO.GetRow(book.AuthorId);
            ViewBag.Category = category;
            ViewBag.Author = author;
            ViewBag.Publisher = publisher;
            return View("BookDetail", book);
        }
        public ActionResult PostPage(string slug, int? page)
        {
            // chua dung
            return View("PostPage");
        }
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }

    }
}