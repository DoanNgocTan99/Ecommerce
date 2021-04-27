using Ecommerce.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    //[Authorize]

    public class ProductController : BaseController
    {
        // GET: Seller/Products
        [HasCredential(RoleID  = "USER")]
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
            if (!ModelState.IsValid)
            {
                var dao = new ProductDAO();


                long id = dao.Insert(product);
                if (id > 0)
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
            SetViewBag(product.IdCategory, product.IdShop);
            return View("Index");
        }

        [HasCredential(RoleID = "USER")]

        public ActionResult Delete(int Id)
        {
            new CategoryDAO().Delete(Id);

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

        public void SetViewBag(long? selectIdCategory = null, long? selectIdShop = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);

            //var dao_Shop = new ShopDAO();
            //ViewBag.IdShop = new SelectList(dao_Shop.ListAll(), "Id", "Name", selectIdShop);

        }
    }
}