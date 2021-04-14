using Ecommerce.Areas.Seller.Models;
using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class LoginController : Seller.Controllers.BaseController
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var UserSession = new UserLogin();
                    UserSession.UserName = user.UserName;
                    UserSession.UserID = user.Id;
                    Session.Add(CommonConstants.USER_SESSION, UserSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError(" ", "Tài Khoản Không Tồn Tại!! Hihi ^^! ");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản Bị Khoá Rồi Nhé :> Hehe");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai Mật Khẩu Rồi :< Hic_hic");
                }
            }
            return View("Index");
        }
    }
}