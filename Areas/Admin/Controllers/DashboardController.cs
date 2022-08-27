 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            //Kiểm tra chứa đăng nhập
            if (Session["UserAdmin"].ToString()=="")
            {
                //Chuyển hướng website
                Redirect("~/Admin/login");
            }
            return View();
        }
    }
}