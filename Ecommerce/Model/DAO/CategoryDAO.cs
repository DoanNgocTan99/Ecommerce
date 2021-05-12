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
    public class CategoryDAO
    {
        EcommerceContext db = null;
        public CategoryDAO()
        {
            db = new EcommerceContext();
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public long Insert(Category entity)
        {
            try
            {
                db.Categories.Add(entity);
                db.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Category ViewDetail(int Id)
        {
            return db.Categories.Find(Id);
        }
        public bool Update(Category entity)
        {
            try
            {
                var user = db.Categories.Find(entity.Id);

                user.Name = entity.Name;
                user.Description = entity.Description;
                user.IsActive = true;
                user.ModifiedDate = DateTime.Now;
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
                var category = db.Categories.Find(Id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }
        //public ProductCategory ViewDetail(long id)
        //{
        //    return db.ProductCategories.Find(id);
        //}
        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.IsActive = !category.IsActive;
            db.SaveChanges();
            return category.IsActive;
        }
        public List<String> GetListNamByIdShop(long id)
        {
            //var user = db.Users.Single(x => x.UserName == UserName);
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            var data = (from a in db.Products
                        join b in db.Categories on a.IdCategory equals b.Id

                        where a.IdShop == id
                        select new
                        {
                            Id = a.IdShop,
                            Name = b.Name
                        }).AsEnumerable().Select(x => new Category()
                        {

                            Name = x.Name
                        });

            return data.Select(x => x.Name).Distinct().ToList();
        }
    }
}
