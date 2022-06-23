using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace websitebansach
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            // Lưu thông tin đăng nhập quản lý
            Session["AdminAccount"] = "";
            Session["AdminId"] = 1;

            // lưu thông tin đăng nhập người dùng
            Session["CustomerAccount"] = "";
            Session["CustomerId"] = "";

            // Giỏ hàng
            Session["MyCart"] = "";
        }
    }
}
