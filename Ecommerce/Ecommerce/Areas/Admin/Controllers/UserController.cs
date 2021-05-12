using Ecommerce.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserController : Seller.Controllers.BaseController
    {
        // GET: Admin/User
        [HttpGet]
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "ADMIN")]
        [ValidateInput(false)]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (user.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                    //return type file
                    string extension = Path.GetExtension(user.ImageFile.FileName);

                    //return file name 
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //category.Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/Shop"), fileName);
                    user.ImageFile.SaveAs(fileName);

                    var result = dao.Update(user);
                    if (!result)
                    {
                        SetAlert("Người dùng không tồn tại!!", "success");

                        return RedirectToAction("Index", "Home");
                    }
                    var ImageDao = new ImageDAO();

                    Image image = new Image();
                    image.IdUser = user.Id;
                    image.Path = fileName;


                    bool ID = ImageDao.UpdateByIdUser(image);
                    if (ID)
                    {
                        SetAlert("Cập nhập thông tin chi tiết  thành công!!", "success");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập Doanh mục không thành công");
                    }
                }
                else
                {
                    var result = dao.Update(user);
                    if (result)
                    {
                        SetAlert("Cập nhập thông tin chi tiết  thành công!!", "success");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập Doanh mục không thành công");
                    }
                }
                //return file name


            }
            return View("Index");
        }
    }
}