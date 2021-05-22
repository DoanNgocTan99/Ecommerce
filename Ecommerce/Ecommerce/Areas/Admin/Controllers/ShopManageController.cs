using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ShopManageController : Controller
    {
        #region Index
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ShopDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        #endregion

        #region Get Lisst Product
        [HasCredential(RoleID = "ADMIN")]
        [HttpGet]
        public ActionResult GetListProduct(long id, string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ShopDAO();
            var productdao = new ProductDAO();
            var model = productdao.ListProductByIdShopPaging(id,searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            var modelCategory = dao.ListCategoryByIdShopPaging(id);
            ViewBag.ListCategory = model;

            dynamic MyModel = new ExpandoObject();
            MyModel.Category = modelCategory;
            MyModel.Product = model;
            MyModel.ListCategory = model;
            return View(MyModel);
        }
        #endregion

        #region Get List Category
        [HasCredential(RoleID = "ADMIN")]
        [HttpGet]
        public ActionResult GetListCategory(long id)
        {
            var dao = new ShopDAO();
            var model = dao.ListCategoryByIdShopPaging(id);
            ViewBag.ListCategory = model;
            dynamic MyModel = new ExpandoObject();
            MyModel.Category = model;
            return View(model);
        }
        #endregion

        #region Delete
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Delete(int Id)
        {
            new ShopDAO().Delete(Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Product
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult DeleteProduct(int Id)
        {
            new ProductDAO().Delete(Id);
            return RedirectToAction("Index");
        }
        #endregion
        #region Change Status
        [HasCredential(RoleID = "ADMIN")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        #endregion
        #region Set view bag
        [HasCredential(RoleID = "ADMIN")]
        public void SetViewBag(long? selectIdCategory = null, long? selectIdShop = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);
        }
        #endregion
    }
}