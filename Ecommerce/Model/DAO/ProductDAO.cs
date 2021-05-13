using Ecommerce.Common;
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
    public class ProductDAO
    {
        EcommerceContext db = null;
        public ProductDAO()
        {
            db = new EcommerceContext();
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) && x.IdShop == session.IdShop|| x.Name.Contains(searchString) && x.IdShop == session.IdShop);
            }
            return model.Where(x => x.IdShop ==session.IdShop).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public IEnumerable<Product> ListAllPagingByAdmin(string searchString, int page, int pageSize)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString)|| x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public long Insert(Product entity)
        {
            try
            {
                UserDAO user = new UserDAO();
                //var product = new Product();
                //var session = (UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
                //long id = session.Id;

                var session = (UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];

                var id = user.GetIdShopByIdUser(session.Id);
                entity.IdShop = id;
                entity.CreatedDate = DateTime.Now;


                //demo
                var dao_ca = new CategoryDAO();
                var IDDD = dao_ca.GetListNamByIdShop(id);


                db.Products.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Product ViewDetail(int Id)
        {
            return db.Products.Find(Id);
        }
        public bool Update(Product entity)
        {
            try
            {
                var session = (UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
                var product = db.Products.Find(entity.Id);

                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Brand = entity.Brand;
                product.Material = entity.Material;
                product.Origin = entity.Origin;
                product.Price = entity.Price;
                product.DelPrice = entity.DelPrice;
                product.WarrantyDate = entity.WarrantyDate;
                product.Stock = entity.Stock;
                product.Discount = entity.Discount;
                product.Rate = entity.Rate;
                product.ModifiedBy = session.UserName;
                product.CreatedBy = session.UserName;
                product.IdCategory = entity.IdCategory;
                product.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
        public bool Delete(int Id)
        {
            try
            {
                var product = db.Products.Find(Id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Product> ListAll()
        {
            return db.Products.ToList();
        }
        public bool ChangeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.IsActive = !product.IsActive;
            db.SaveChanges();
            return product.IsActive;
        }
        public bool ChangeStatusFalseSale(long id)
        {
            var product = db.Products.Find(id);
            product.FlaseSale = !product.FlaseSale;
            db.SaveChanges();
            return product.FlaseSale;
        }
        public bool ChangeStatusAdvertisement(long id)
        {
            var product = db.Products.Find(id);
            product.Advertisement = !product.Advertisement;
            db.SaveChanges();
            return product.Advertisement;
        }
        public bool DeleteProductByIdCategory(long idcategory)
        {
            var data = db.Products.Where(x => x.IdCategory == idcategory).ToList();
            db.Products.RemoveRange(data);
            db.SaveChanges();
            return true;
        }
    }
}
