using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public long Insert(Product entity)
        {
            try
            {

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
                var product = db.Products.Find(entity.Id);
                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Brand = entity.Brand;
                product.Material = entity.Material;
                product.Origin = entity.Origin;
                product.Price = entity.Price;
                product.DelPrice = entity.DelPrice;
                product.WarrantyDate = DateTime.Now;
                product.Stock = entity.Stock;
                product.Discount = entity.Discount;
                product.Views = entity.Views;
                product.Rate = entity.Rate;
                product.IsActive = entity.IsActive;
                product.ModifiedBy = entity.ModifiedBy;
                product.CreatedBy = entity.CreatedBy;
                product.IdCategory = entity.IdCategory;
                db.SaveChanges();
                return true;
            }
            catch
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
    }
}
