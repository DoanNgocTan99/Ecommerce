using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Seller/Shop
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new ShopDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Shop shop)
        {
            if (ModelState.IsValid)
            {
                var dao = new ShopDAO();
                //return file name
                string fileName = Path.GetFileNameWithoutExtension(shop.ImageFile.FileName);
                //return type file
                string extension = Path.GetExtension(shop.ImageFile.FileName);

                //return file name 
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //category.Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/Category"), fileName);
                shop.ImageFile.SaveAs(fileName);
                var result = dao.Update(shop);

                var ImageDao = new ImageDAO();

                Image image = new Image();
                image.IdShop = shop.Id;
                image.Path = fileName;


                bool ID = ImageDao.Update(image);
                if (ID)
                {
                    SetAlert("Cập nhập thông tin chi tiết  thành công!!", "success");

                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập Doanh mục không thành công");
                }

            }
            return View("Index");
        }
    }
}