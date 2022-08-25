﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopThoiTrang.Library;
using ShopThoiTrang.Models;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ShopThoiTrangDBContext db = new ShopThoiTrangDBContext();

        // GET: Admin/Category
        public ActionResult Index()
        {   
            var list = db.Categorys.Where(m => m.Status!=0)
                .OrderByDescending(m=>m.Created_At)
                .ToList();
            return View("Index", list);
        }


        public ActionResult Trash()
        {
            var list = db.Categorys.Where(m => m.Status == 0)
                .OrderByDescending(m => m.Created_At)
                .ToList();
            return View("Trash", list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.ToList(), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Category category)
        {
            if (ModelState.IsValid)
            {   
                string slug = XString.Str_Slug(category.Name);
                category.Slug = slug;
                category.Created_At = DateTime.Now;
                category.Created_By = int.Parse(Session["UserAdmin"].ToString());
                category.Updated_At = DateTime.Now;
                category.Updated_By = int.Parse(Session["UserAdmin"].ToString());
                db.Categorys.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.ToList(), "Orders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.ToList(), "Orders", "Name", 0);
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(category.Name);
                category.Slug = slug;
                category.Created_At = DateTime.Now;
                category.Created_By = int.Parse(Session["UserAdmin"].ToString());
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(db.Categorys.ToList(), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.Categorys.ToList(), "Orders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categorys.Find(id);
            db.Categorys.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Trash", "Category");
        }

        //Thay đổi trạng thái 1 -> 2, 2-> 1
        public ActionResult Status(int id)
        {
            Category category = db.Categorys.Find(id);
            int status = (category.Status == 1) ? 2 : 1;
            category.Status = status;
            category.Updated_By = 1;
            category.Created_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Xóa vào thùng rác Status = 0
        public ActionResult DelTrash(int id)
        {
            Category category = db.Categorys.Find(id);
            category.Status = 0;
            category.Updated_By = 1;
            category.Created_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","Category");

        }

        //Khôi phục rác Status = 2
        public ActionResult ReTrash(int id)
        {
            Category category = db.Categorys.Find(id);
            category.Status = 2;
            category.Updated_By = 1;
            category.Created_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Category");

        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
