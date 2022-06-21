using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyDB.DAO;
using MyDB.Models;
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
        public ActionResult Index(string slug = null)
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
                                return this.ProductCategory(slug);
                            }
                        case "page":
                            {

                                return this.PostPage(slug);
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
                            return this.ProductAuthor(slug);
                        }
                        else
                        {
                            Publisher publisher = publisherDAO.GetRow(slug);
                            if (publisher != null)
                            {
                                return this.ProductPublisher(slug);
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
            return View("Product");
        }
        public ActionResult ProductCategory(string slug)
        {
            Category category = categoryDAO.GetRow(slug);
            List<Book> books = bookDAO.GetListByCateId(category.Id);
            ViewBag.Category = category;
            return View("ProductCategory", books);
        }
        public ActionResult ProductAuthor(string slug)
        {
            Author author = authorDAO.GetRow(slug);
            List<Book> books = bookDAO.GetListByAuId(author.Id);
            ViewBag.Author = author;
            return View("ProductAuthor", books);
        }
        public ActionResult ProductPublisher(string slug)
        {
            Publisher publisher = publisherDAO.GetRow(slug);
            List<Book> books = bookDAO.GetListByPubId(publisher.Id);
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
        public ActionResult PostPage(string slug)
        {
            return View("PostPage");
        }
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }

    }
}