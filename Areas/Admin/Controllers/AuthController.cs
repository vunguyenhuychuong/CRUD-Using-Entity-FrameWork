using ShopThoiTrang.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {   
        private ShopThoiTrangDBContext db = new ShopThoiTrangDBContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(FormCollection field)
        {
            ViewBag.Error = "";
            string username = field["username"];
            string password = field["password"];
            //Kiểm tra xem trong CSDL có UserName: admin hoặc Emal 
            //SELECT * FROM User WHERE Roles="Admin"
            User user = db.Users
                .Where(m => m.Roles == "Admin" && m.Status == 1 && (m.UserName == username || m.Email == username))
                .FirstOrDefault();
            if (user != null)
            {   
                //Chặn người dùng mò password , trừ những người Admin
                if (user.CountError > 5 && user.Roles != "Admin")
                {
                    ViewBag.Error = "<p class='login-box-msg text-danger'>Liên hệ lại người quản lý!</p>";
                }
                else
                {
                    //Hỏi mật khẩu có khớp không
                    if (user.Password.Equals(password))
                    {
                        //Khớp
                        Session["UserAdmin"] = username;
                        Session["UserId"] = user.Id.ToString();
                        Session["FullName"] = user.FullName;
                        Session["Img"] = user.Img;
                    }
                    else
                    {
                        //Cập nhập lại số lần sai
                        if (user.CountError == null)
                        {
                            user.CountError = 0;
                        }
                        else
                        {
                            user.CountError += 1; // Tăng lên 1 lần
                        }
                        //Cập nhật lại mẫu tin
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Error = "<p class='login-box-msg text-danger'>Mật khẩu không chính xác!</p>";
                    }
                }            
            }
            else
            {
                ViewBag.Error = "<p class='login-box-msg text-danger'>Tài Khoản <strong> "+username+"</strong> không tồn tại </p>";
            }
            return View("Login");
        }


    }
}