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
    public class OrderController : Controller
    {
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        UserDAO userDAO = new UserDAO();

        // GET: Admin/Order
        public ActionResult Index()
        {
            return View(orderDAO.GetList());
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.GetRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CTDonHang = orderDetailDAO.GetList(id);

            ViewBag.KH = userDAO.GetRow(order.UserId);
            return View(order);
        }

        public ActionResult Huy(int? bid)
        {
            Order order = orderDAO.GetRow(bid);
            if (order == null)
            {
                TempData["Message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index");
            }
            switch (order.Status)
            {
                case 0:
                    TempData["Message"] = new XMessage("info", "Đơn hàng đang ở trạng thái này");
                    break;
                case 1:
                case 2:
                    order.Status = 0;
                    order.UpdateAt = DateTime.Now;
                    order.UpdateBy = int.Parse(Session["AdminId"].ToString());
                    orderDAO.Update(order);
                    TempData["Message"] = new XMessage("success", "Hủy đơn hàng thành công");
                    break;
                case 3:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đang vận chuyển không thể hủy");
                    break;
                case 4:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đã giao không thể hủy");
                    break;
            }

            return RedirectToAction("Index");
        }

        public ActionResult DaXacMinh(int? bid)
        {
            Order order = orderDAO.GetRow(bid);
            if (order == null)
            {
                TempData["Message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index");
            }
            switch (order.Status)
            {
                case 0:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đã hủy không thể thay đổi trạng thái");
                    break;
                case 1:
                    order.Status = 2;
                    order.UpdateAt = DateTime.Now;
                    order.UpdateBy = int.Parse(Session["AdminId"].ToString());
                    orderDAO.Update(order);
                    TempData["Message"] = new XMessage("success", "Cập nhật trạng thái đơn hàng thành công");
                    break;
                case 2:
                    TempData["Message"] = new XMessage("info", "Đơn hàng đang ở trạng thái này");
                    break;
                case 3:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đang được vận chuyển");
                    break;
                case 4:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đã được giao");
                    break;
            }

            return RedirectToAction("Index");
        }

        public ActionResult DangVanChuyen(int? bid)
        {
            Order order = orderDAO.GetRow(bid);
            if (order == null)
            {
                TempData["Message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index");
            }
            switch (order.Status)
            {
                case 0:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đã hủy không thể thay đổi trạng thái");
                    break;
                case 1:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đang chờ xác nhận để được vận chuyển");
                    break;
                case 2:
                    order.Status = 3;
                    order.UpdateAt = DateTime.Now;
                    order.UpdateBy = int.Parse(Session["AdminId"].ToString());
                    orderDAO.Update(order);
                    TempData["Message"] = new XMessage("success", "Cập nhật trạng thái đơn hàng thành công");
                    break;                    
                case 3:
                    TempData["Message"] = new XMessage("info", "Đơn hàng đang ở trạng thái này");
                    break;
                case 4:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đã được giao");
                    break;
            }

            return RedirectToAction("Index");
        }

        public ActionResult ThanhCong(int? bid)
        {
            Order order = orderDAO.GetRow(bid);
            if (order == null)
            {
                TempData["Message"] = new XMessage("danger", "Đơn hàng không tồn tại");
                return RedirectToAction("Index");
            }
            switch (order.Status)
            {
                case 0:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đã hủy không thể thay đổi trạng thái");
                    break;
                case 1:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đang chờ xác minh để được vận chuyển");
                    break;
                case 2:
                    TempData["Message"] = new XMessage("warning", "Đơn hàng đang chờ vận chuyển");
                    break;
                case 3:
                    order.Status = 4;
                    order.UpdateAt = DateTime.Now;
                    order.UpdateBy = int.Parse(Session["AdminId"].ToString()); // !!!
                    orderDAO.Update(order);
                    TempData["Message"] = new XMessage("success", "Cập nhật trạng thái đơn hàng thành công");
                    break;
                case 4:
                    TempData["Message"] = new XMessage("info", "Đơn hàng đang ở trạng thái này");
                    break;
            }

            return RedirectToAction("Index");
        }
              

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.GetRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.GetRow(id);
            orderDAO.Delete(order);
            return RedirectToAction("Index");
        }

    }
}
