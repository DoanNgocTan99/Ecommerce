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
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) && x.IdProgramme == 1 || x.Name.Contains(searchString) && x.IdProgramme == 1);
            }
            return model.Where(x => x.IdProgramme == 1).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public IEnumerable<Product> ListAllPagingAdvertisement(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) && x.IdProgramme == 2 || x.Name.Contains(searchString) && x.IdProgramme == 2);
            }
            return model.Where(x => x.IdProgramme == 2).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
    }
}
