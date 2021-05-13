using Ecommerce.Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class UserController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Username,Password,Email,Birthday,Phone,Address,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase manualfile)
        {
            if (manualfile != null)
            {
                string path = Server.MapPath("../Assets/User/images/UploadFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string imageName;
                Image f;
                int id = int.Parse(Request.Cookies["IdUser"].Value);
                var acc = from e in db.Users
                          where e.Id == id
                          select e;
                var oldF = from of in db.Images
                           where of.IdUser == id
                           select of;
                if (oldF.FirstOrDefault() != null)
                {
                    db.Images.Remove(oldF.FirstOrDefault());
                    db.SaveChanges();
                }
                imageName = Path.GetFileNameWithoutExtension(manualfile.FileName);
                manualfile.SaveAs(path + "\\" + imageName);
                f = new Image();
                //f.ImageFile.FileName = imageName;
                f.Path = path + "\\" + imageName;
                f.IdUser = acc.FirstOrDefault().Id;
                db.Images.Add(f);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Update(int Id, string Name, string Username, string Birthday, string Phone, string btnUpdate, bool Gender, string Address)
        {
            User c = db.Users.Find(Id);
            if (btnUpdate == "Save")
            {
                c.Name = Name;
                c.UserName = Username;
                if (Birthday != null)
                {
                    c.DOB = DateTime.Parse(Birthday);
                }
                c.Address = Address;
                c.Gender = Gender;
                c.Phone = Phone;
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "User", new { id = Id });
        }

        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        public ActionResult Password(int Id, string Password, string btnUpdate)
        {
            User c = db.Users.Find(Id);
            if (btnUpdate == "Save")
            {
                var pass = Encryptor.MD5Hash(Password);
                c.Password = pass;
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "User", new { id = Id });
        }
    }
}