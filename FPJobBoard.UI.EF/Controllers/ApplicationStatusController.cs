using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPJobBoard.Data.EF;

namespace FPJobBoard.UI.EF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationStatusController : Controller
    {   
        private FPDBEntities db = new FPDBEntities();

        // GET: ApplicationStatus
        public ActionResult Index(string divId)
        {
            ViewBag.Scroll = divId;

            return View(db.ApplicationStatus.ToList());
        }

        // GET: ApplicationStatus/Details/5
        public ActionResult Details(int? id, string divId)
        {
            ViewBag.Scroll = divId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            if (applicationStatu == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatu);
        }

        // GET: ApplicationStatus/Create
        public ActionResult Create(string divId)
        {
            ViewBag.Scroll = divId;

            return View();
        }

        // POST: ApplicationStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationStatusID,StatusName,StatusDescription")] ApplicationStatu applicationStatu)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationStatus.Add(applicationStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationStatu);
        }

        // GET: ApplicationStatus/Edit/5
        public ActionResult Edit(int? id, string divId)
        {
            ViewBag.Scroll = divId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            if (applicationStatu == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatu);
        }

        // POST: ApplicationStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationStatusID,StatusName,StatusDescription")] ApplicationStatu applicationStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationStatu);
        }

        // GET: ApplicationStatus/Delete/5
        public ActionResult Delete(int? id, string divId)
        {
            ViewBag.Scroll = divId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            if (applicationStatu == null)
            {
                return HttpNotFound();
            }
            return View(applicationStatu);
        }

        // POST: ApplicationStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationStatu applicationStatu = db.ApplicationStatus.Find(id);
            db.ApplicationStatus.Remove(applicationStatu);
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
