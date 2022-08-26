using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Controllers
{
    public class TrangchuController : Controller
    {

        private ShopThoiTrangDBContext db = new ShopThoiTrangDBContext();
        // GET: Trangchu
        public ActionResult Index()
        {
            var listcat = db.Categorys.Where(m=>m.Status==1 && m.ParentId==0).ToList();
            return View(listcat);
        }

        public ActionResult ProductHome(int catid, string namecat)
        {
            var listproduct = db.Products.Where(m => m.Status == 1 && m.CatId == catid)
                .OrderByDescending(m => m.Created_At).ToList();
            ViewBag.CatName = namecat;
            return View("ProductHome", listproduct);
        }
    }
}