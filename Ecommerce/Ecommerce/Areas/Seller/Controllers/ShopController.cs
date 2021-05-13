using Ecommerce.Common;
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
        [HasCredential(RoleID = "USER")]
        public ActionResult Edit(int id)
        {
            var shop = new ShopDAO().ViewDetail(id);
            return View(shop);
        }
        [HttpPost]
        [HasCredential(RoleID = "USER")]
        [ValidateInput(false)]
        public ActionResult Edit(Shop shop)
        {
            if (!ModelState.IsValid)
            {
                var dao = new ShopDAO();
                if(shop.ImageFile != null)
                {
                    //return file name
                    string fileName = Path.GetFileNameWithoutExtension(shop.ImageFile.FileName);
                    //return type file
                    string extension = Path.GetExtension(shop.ImageFile.FileName);


                //return file name 
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //category.Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/Category"), fileName);
                shop.ImageFile.SaveAs(fileName);
                //var result = dao.Update(shop);

                    var result = dao.Update(shop);
                    if (!result)
                    {
                        SetAlert("Shop không tồn tại!!", "success");

                        return RedirectToAction("Index", "Home");
                    }
                    var ImageDao = new ImageDAO();

                    Image image = new Image();
                    image.IdShop = shop.Id;
                    image.Path = fileName;


                    bool ID = ImageDao.UpdateByIdShop(image);
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
                    var result = dao.Update(shop);
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
                

            }
            return View("Index");
        }
        
    }
}