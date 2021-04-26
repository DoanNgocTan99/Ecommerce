using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Seller/Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
                    string extension = Path.GetExtension(image.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    image.Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    image.ImageFile.SaveAs(fileName);


                    var dao = new ImageDAO();

                    //long id = dao.Insert(image);
                    //if (id > 0)
                    //{
                    //    SetAlert("Thêm mới ảnh thành công!!", "success");
                    //    return RedirectToAction("Index", "Image");
                    //}
                    //else
                    //{

                    //    ModelState.AddModelError("", "Thêm ảnh không thành công");
                    //}
                    //ModelState.Clear();
                }

                SetViewBag();
                return View("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}