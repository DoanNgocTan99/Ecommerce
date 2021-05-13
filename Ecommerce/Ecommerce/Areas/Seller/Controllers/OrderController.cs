using Ecommerce.Common;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Seller.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Seller/Order
        [HasCredential(RoleID = "USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new OrderDAO();
            var model = dao.GetAllByIdShop(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
    }
}