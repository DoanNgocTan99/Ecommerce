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
    //[Authorize]

    public class ProductController : BaseController
    {

        // GET: Seller/Products
        [HasCredential(RoleID = "USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "USER")]

        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [HasCredential(RoleID = "USER")]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                var check = dao.CheckProduct(product.Name);
                if (check)
                {
                    SetAlert("Tên sản phẩm đã tồn tại", "erro");
                    return RedirectToAction("Index", "Product");
                }
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string temp = fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/Product"), fileName);
                string filename = fileName.Substring(fileName.Length - (15 + temp.Length), (15 + temp.Length));
                product.ImageFile.SaveAs(fileName);

                long id = dao.Insert(product);

                var ImageDao = new ImageDAO();
                Image image = new Image();
                image.IdProduct = id;
                image.Path = filename;
                long ID = ImageDao.Insert(image);
                if (ID > 0)
                {
                    SetAlert("Thêm mới sản phẩm thành công!!", "success");
                    return RedirectToAction("Index", "Product");

                }
                else
                {

                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }

            }
            SetViewBag();
            return View("Index");
        }
        [HttpGet]
        [HasCredential(RoleID = "USER")]
        public ActionResult Edit(int id)
        {
            var product = new ProductDAO().ViewDetail(id);
            SetViewBag(product.IdCategory, product.IdShop);
            return View(product);
        }
        [HttpPost]
        [HasCredential(RoleID = "USER")]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                var ImageDao = new ImageDAO();
                if (product.ImageFile != null)
                {

                    var check = dao.CheckProduct(product.Name);
                    if (check)
                    {
                        SetAlert("Tên sản phẩm đã tồn tại", "erro");
                        return RedirectToAction("Index", "Product");
                    }
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    string extension = Path.GetExtension(product.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string temp = fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/Product"), fileName);
                    string filename = fileName.Substring(fileName.Length - (15 + temp.Length), (15 + temp.Length));
                    product.ImageFile.SaveAs(fileName);

                    var result = dao.Update(product);
                    if (!result)
                    {
                        SetAlert("Cập nhập sản phẩm không thành công, kiểm tra lại thông tin!!", "erro");
                        return RedirectToAction("Index", "Product");
                    }
                    Image image = new Image();
                    image.IdProduct = product.Id;
                    image.Path = filename;
                    var ID = ImageDao.UpdateByIdProduct(image);
                    if (ID)
                    {
                        SetAlert("Cập nhập sản phẩm thành công!!", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập sản phẩm không thành công");
                    }
                }
                else
                {
                    var result = dao.Update(product);
                    if (result)
                    {
                        SetAlert("Cập nhập sản phẩm thành công!!", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhập sản phẩm không thành công");
                    }
                }
            }
            SetViewBag(product.IdCategory, product.IdShop);
            return View("Index");
        }

        [HasCredential(RoleID = "USER")]

        public ActionResult Delete(int Id)
        {
            new ProductDAO().Delete(Id);
            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "USER")]

        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HasCredential(RoleID = "USER")]

        public void SetViewBag(long? selectIdCategory = null, long? selectIdProgramme = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);
            var dao_Programme = new ProgrammeDAO();
            ViewBag.IdProgramme = new SelectList(dao_Programme.ListAll(), "Id", "Name", selectIdProgramme);
        }
    }
}