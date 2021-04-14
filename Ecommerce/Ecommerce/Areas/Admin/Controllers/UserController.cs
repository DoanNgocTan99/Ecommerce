using Ecommerce.Areas.Seller.Controllers;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View();
        }
    }
}