using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace websitebansach
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Tatcasanpham",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Sắp phát hành",
              url: "sap-phat-hanh",
              defaults: new { controller = "ComingSoon", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Sách mới",
              url: "sach-moi",
              defaults: new { controller = "NewBook", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Sách bán chạy",
              url: "sach-ban-chay",
              defaults: new { controller = "Seller", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "GioHang",
                url: "gio-hang",
                defaults: new { controller = "Giohang", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TimKiem",
                url: "tim-kiem",
                defaults: new { controller = "Timkiem", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VeNobita",
                url: "ve-nobita",
                defaults: new { controller = "Gioithieu", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "TuyenDung",
               url: "tuyen-dung",
               defaults: new { controller = "Tuyendung", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "BaiViet",
               url: "bai-viet",
               defaults: new { controller = "Baiviet", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Dangnhap",
               url: "dang-nhap",
               defaults: new { controller = "Khachhang", action = "DangNhap", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Dangky",
               url: "dang-ky",
               defaults: new { controller = "Khachhang", action = "DangKy", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                 name: "Huongdanmuahang",
                 url: "huong-dan-mua-hang",
                 defaults: new { controller = "Huongdanmuahang", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "PhuongThucThanhToan",
                 url: "phuong-thuc-thanh-toan",
                 defaults: new { controller = "Phuongthucthanhtoan", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "Chinhsachvanchuyen",
                 url: "chinh-sach-van-chuyen",
                 defaults: new { controller = "Chinhsachvanchuyen", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "Cauhoithuonggap",
                 url: "cau-hoi-thuong-gap",
                 defaults: new { controller = "Cauhoithuonggap", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Chinhsachbaomat",
                url: "chinh-sach-bao-mat-thong-tin",
                defaults: new { controller = "Chinhsachbaomat", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Thanhtoan",
               url: "thanh-toan",
               defaults: new { controller = "Giohang", action = "ThanhToan", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Thanhcong",
             url: "thanh-cong",
             defaults: new { controller = "Giohang", action = "ThanhCong", id = UrlParameter.Optional }
         );
            routes.MapRoute(
                name: "Thongbao",
                url: "thong-bao",
                defaults: new { controller = "Thongbao", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "SiteSlug",
             url: "{slug}",
             defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
         );
            //khai bao url dong

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
