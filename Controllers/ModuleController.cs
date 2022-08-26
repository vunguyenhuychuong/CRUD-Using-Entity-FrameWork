using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Controllers
{
    public class ModuleController : Controller
    {
        private ShopThoiTrangDBContext db = new ShopThoiTrangDBContext();

        // GET: Module
        public ActionResult ListCategoryMenu()
        {
            var list = db.Categorys.Where(m => m.ParentId == 0 && m.Status == 1).ToList();
            return View("ListCategoryMenu", list);
        }
    }
}