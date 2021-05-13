using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class PaymentController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        // GET: Order
        public ActionResult Index(int? id)
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
        //public ActionResult Index(int? id)
        //{

        //}

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
        public ActionResult UpdateInfo(int Id, string Name, string Username, string Birthday, string Phone, string btnUpdate, bool Gender, string Address)
        {
            User c = db.Users.Find(Id);
            if (btnUpdate == "Checkout")
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
            return RedirectToAction("Index", "Home", new { id = Id });
        }
    }
}