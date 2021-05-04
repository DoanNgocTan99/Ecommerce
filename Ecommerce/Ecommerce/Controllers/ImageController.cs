using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ImageController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        public string getImage(int id)
        {
            Image image = new Image();
            image = db.Images.Where(x => x.IdUser == id).FirstOrDefault();
            if (image != null)
            {
                return image.Path;
            }
            return "";
        }
        public string getImgCat(int id)
        {
            Image image = db.Images.Where(x => x.IdCategory == id).FirstOrDefault();
            if (image != null)
            {
                return image.Path;
            }
            return "";
        }
    }
}