using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(string slug)
        {
            return View("ProductCategory");
        }
    }
}