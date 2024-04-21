using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using Database_First.Models;
namespace Database_First.Controllers
{
    public class UserController : Controller
    {
        private dbTestEntities db = new dbTestEntities();
        public ActionResult Index()
        {
            var users = db.T_Users;
            return View(users);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Family,Email,MobileNumber,Password,Address,Marital")]
                                    T_Users user, HttpPostedFileBase imgUpload)
        {

            if (ModelState.IsValid)
            {

                string imageName = "def.png";
                if (imgUpload != null)
                {
                    if (imgUpload.ContentType != "image/jpeg" && imgUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageName", "نوع فایل آپلود شده معتبر نیست.");
                        return View();
                    }

                    if (imgUpload.ContentLength > 200000)
                    {
                        ModelState.AddModelError("ImageName", "(اندازه تصویر انتخابی زیاد می باشد(تا 200کیلو بایت");
                    }
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/images/user_images/" + imageName));
                }

                user.ImageName = imageName;
                user.IsActive = true;
                user.RegisterDate = DateTime.Now;
                db.T_Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.T_Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.T_Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Family,Email,MobileNumber,ImageName,Password,Address,Marital,IsActive,RegisterDate")] T_Users user, HttpPostedFileBase imgUpload)
        {
            if (ModelState.IsValid)
            {
                if (imgUpload != null)
                {
                    if (user.ImageName != "def.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/images/user_images/") + user.ImageName);
                    }
                    string imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imgUpload.FileName);
                    imgUpload.SaveAs(Server.MapPath("/images/user_images/" + imageName));
                    user.ImageName = imageName;
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.T_Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.T_Users.Find(id);
            if (user != null)
            {
                db.T_Users.Remove(user);
                db.SaveChanges();
                if (user.ImageName != "def.png")
                {
                    if (System.IO.File.Exists(Server.MapPath("/images/user_images/") + user.ImageName))
                    {
                        System.IO.File.Delete(Server.MapPath("/images/user_images/") + user.ImageName);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}