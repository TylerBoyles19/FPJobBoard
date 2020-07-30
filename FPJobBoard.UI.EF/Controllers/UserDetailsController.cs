using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPJobBoard.Data.EF;
using Microsoft.AspNet.Identity;

namespace FPJobBoard.UI.EF.Controllers
{
    public class UserDetailsController : Controller
    {
        private FPDBEntities db = new FPDBEntities();

        // GET: UserDetails
        public ActionResult Index()
        {
            string currentUserID = User.Identity.GetUserId();

            var user = db.UserDetails.Where(u => u.UserID == currentUserID).ToList();
            return View(user);
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: UserDetails/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,ResumeFilename")] UserDetail userDetail, HttpPostedFileBase resume)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string imageName = "NoImageAvalible.png";
                if (resume != null)
                {
                    imageName = resume.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    string[] goodExts = new string[] { ".pdf" };
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;
                        resume.SaveAs(Server.MapPath("~/Content/UploadedImg/" + imageName));
                    }
                    else
                    {
                        imageName = "NoImageAvalible.png";
                    }
                }
                userDetail.ResumeFilename = imageName;
                #endregion

                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail.ResumeFilename != "NoImageAvalible.png")
            {
                Session["ErrorMessage"] = null;
            }
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,ResumeFilename")] UserDetail userDetail, HttpPostedFileBase resume)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string imageName = "NoImageAvalible.png";
                if (resume != null)
                {
                    imageName = resume.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    string[] goodExts = new string[] { ".pdf" };
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;
                        resume.SaveAs(Server.MapPath("~/Content/UploadedImg/" + imageName));
                    }
                    else
                    {
                        imageName = "NoImageAvalible.png";
                    }
                    userDetail.ResumeFilename = imageName;
                }
                #endregion

                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
            db.SaveChanges();
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
