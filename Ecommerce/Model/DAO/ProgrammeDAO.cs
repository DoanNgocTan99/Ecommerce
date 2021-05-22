using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.DAO
{
    public class ProgrammeDAO
    {
        EcommerceContext db = null;
        public ProgrammeDAO()
        {
            db = new EcommerceContext();
        }
        public List<Programme> ListAll()
        {
            return db.Programmes.ToList();
        }
        public IEnumerable<Product> ListAllPagingFlaseSale(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            var DateEnd = db.Programmes.Where(x => x.Id == 1).Select(x => x.DateEnd).FirstOrDefault();
            var products = db.Products.Where(x => x.IdProgramme ==1 ).ToList();
            List<Product> product = new List<Product>();
            foreach (var a in products)
            {
                var DateOpen = a.DateAddFlaseSale;
                int number = DateTime.Compare((DateTime)DateOpen, (DateTime)DateEnd);
                if (number >= 0)
                {
                    product.Add(a);
                }
            }
            return product.Where(x => x.IdProgramme == 1).OrderByDescending(x => x.CreatedDate).ToList();
            //IQueryable<Product> model = db.Products;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    product.Where(x => x.Name.Contains(searchString) && x.IdProgramme == 1 || x.Name.Contains(searchString) && x.IdProgramme == 1);
            //}
            //var program = db.Programmes.Where(x => x.Id == 1).ToList().Select(x => x.DateEnd);
            //var temp = db.Products.Where(x => x.DateAddFlaseSale >= program.FirstOrDefault());
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    temp = temp.Where(x => x.Name.Contains(searchString) && x.IdProgramme == 1 || x.Name.Contains(searchString) && x.IdProgramme == 1);
            //}
            //return temp.Where(x => x.IdProgramme == 1).OrderByDescending(x => x.DateAddFlaseSale).ToPagedList(page, pageSize);
            //IQueryable<Product> model = db.Products;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) && x.IdProgramme == 1 || x.Name.Contains(searchString) && x.IdProgramme == 1);
            //}
            //return model.Where(x => x.IdProgramme == 1).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> ListAllPagingAdvertisement(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            var DateEnd = db.Programmes.Where(x => x.Id == 2).Select(x => x.DateEnd).FirstOrDefault();
            var products = db.Products.Where(x => x.IdProgramme == 2).ToList();
            List<Product> product = new List<Product>();
            foreach (var a in products)
            {
                var DateOpen = a.DateAddAdvertisement;
                int number = DateTime.Compare((DateTime)DateOpen, (DateTime)DateEnd);
                if (number >= 0)
                {
                    product.Add(a);
                }
            }
            return product.Where(x => x.IdProgramme == 2).OrderByDescending(x => x.CreatedDate).ToList();
            //return product.Where(x => x.IdProgramme == 1).OrderByDescending(x => x.CreatedDate).ToList();
            //var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            //IQueryable<Product> model = db.Products;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Name.Contains(searchString) && x.IdProgramme == 2 || x.Name.Contains(searchString) && x.IdProgramme == 2);
            //}
            //return model.Where(x => x.IdProgramme == 2).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
    }
}
