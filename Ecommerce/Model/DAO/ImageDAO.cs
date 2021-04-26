﻿using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ImageDAO
    {
        EcommerceContext db = null;
        public ImageDAO()
        {
            db = new EcommerceContext();
        }
        public IEnumerable<Image> ListAllPaging(string searchString, int page, int pageSize)
        {
            try
            {

                IQueryable<Image> model = db.Images;
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.Path.Contains(searchString) || x.Path.Contains(searchString));
                }
                return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long Insert(Image image)
        {
            try
            {
                db.Images.Add(image);
                db.SaveChanges();
                return image.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public bool Delete(int Id)
        {
            try
            {
                var image = db.Images.Find(Id);
                db.Images.Remove(image);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Image> ListALl()
        {
            try
            {

                return db.Images.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Image ViewDetail(int Id)
        {
            return db.Images.Find(Id);
        }
        public bool Update(Image entity)
        {
            try
            {
                var image = db.Images.Where(p => p.IdCategory == entity.IdCategory).FirstOrDefault();

                image.Path = entity.Path;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}