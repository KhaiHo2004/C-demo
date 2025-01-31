﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021143.Web.Controllers
{
    public class AccountController : Controller
    {/// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            if(username == "abc@gmail.com" && password == "123" )
            {
                //Ghi vlaij cokie phiên đăng nhập
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                //Nhảy vào trang chủ
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UserName = username;
            ViewBag.Message = "Đăng nhập thất bại";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");

        } 
        public ActionResult ChangePassword()
        {
            return View();
        }
        
    }
}