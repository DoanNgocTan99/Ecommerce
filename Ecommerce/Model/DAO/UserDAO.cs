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
    public class UserDAO
    {
        EcommerceContext db = null;
        public UserDAO()
        {
            db = new EcommerceContext();
        }
        public bool Update(User entity)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            try
            {
                var user = db.Users.Find(entity.Id);

                user.Name = entity.Name;
                user.UserName = entity.UserName;
                user.Password = entity.Password;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Gender = entity.Gender;
                user.DOB = entity.DOB;
                user.Address = entity.Address;
                user.ModifiedBy = session.Name;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool UpdateByAdmin(User entity)
        {
            var session = (Ecommerce.Common.UserLogin)HttpContext.Current.Session[Ecommerce.Common.CommonConstants.USER_SESSION];
            try
            {
                var user = db.Users.Find(entity.Id);
                user.IdRole = entity.IdRole;
                user.ModifiedBy = session.Name;
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
                var dao = new ShopDAO().DeleteListShopByIdUser(Id);
                var user = db.Users.Find(Id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    model = model.Where(x => x.Phone.Contains(searchString)|| x.Address.Contains(searchString));
            //}
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

        }
        public int Login(string username, string password)
        {
            var result = db.Users.FirstOrDefault(x => x.UserName == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.IsActive == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else
                    {

                        return -2;
                    }
                }
            }

        }
        public int Login(string username, string password, bool isLogin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLogin == true)
                {
                    if (result.IdRole == CommonConstants.ROLE_ADMIN || result.IdRole == CommonConstants.ROLE_USER)
                    {
                        if (result.IsActive == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == password)
                            {
                                return 1;
                            }
                            else
                            {

                                return -2;
                            }
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {

                    if (result.IsActive == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == password)
                        {
                            return 1;
                        }
                        else
                        {

                            return -2;
                        }
                    }
                }

            }

        }
        public User GetByUserName(string userName)
        {
            return db.Users.FirstOrDefault(x => x.UserName == userName);
        }
        public long Insert(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.Id;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<User> ListAll()
        {
            IEnumerable<User> model = db.Users;
            return model.ToList();
        }
        public bool SearchByUser(User user)
        {
            try
            {
                var entity = db.Users.Where(p => p.UserName == user.UserName).Count();
                if (entity > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<string> GetListCredential(string UserName)
        {
            var user = db.Users.Single(x => x.UserName == UserName);

            var data = (from a in db.Users
                        join b in db.Roles on a.IdRole equals b.Id

                        where b.Id == user.IdRole
                        select new
                        {
                            IdRole = a.IdRole,

                            Name = b.Name
                        }).AsEnumerable().Select(x => new Role()
                        {
                            Id = x.IdRole,
                            Name = x.Name
                        });

            return data.Select(x => x.Name).ToList();

        }
        public long GetIdShopByIdUser(long IdUser)
        {
            var user = db.Users.Single(x => x.Id == IdUser);

            var data = (from a in db.Users
                        join b in db.Shops on a.Id equals b.IdUser

                        where b.IdUser == user.Id
                        select new
                        {
                            Id = b.Id

                        }).AsEnumerable().Select(x => new Shop()
                        {
                            Id = x.Id
                        });


            return data.Select(x => x.Id).FirstOrDefault();


        }
        public User ViewDetail(int Id)
        {
            try
            {
                return db.Users.Find(Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.IsActive = !user.IsActive;
            db.SaveChanges();
            return user.IsActive;
        }
    }
}
