using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class ShopManageController : Controller
    {
        // GET: Admin/ShopManage
        // GET: Admin/ProductManage
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ShopDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HasCredential(RoleID = "ADMIN")]
        public ActionResult Delete(int Id)
        {
            new ShopDAO().Delete(Id);

            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "ADMIN")]

        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result

            });
        }
        [HasCredential(RoleID = "ADMIN")]

        public void SetViewBag(long? selectIdCategory = null, long? selectIdShop = null)
        {
            var dao_Category = new CategoryDAO();
            ViewBag.IdCategory = new SelectList(dao_Category.ListAll(), "Id", "Name", selectIdCategory);

            //var dao_Shop = new ShopDAO();
            //ViewBag.IdShop = new SelectList(dao_Shop.ListAll(), "Id", "Name", selectIdShop);

        }
    }
}