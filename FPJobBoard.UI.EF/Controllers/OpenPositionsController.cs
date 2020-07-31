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
    public class OpenPositionsController : Controller
    {
        private FPDBEntities db = new FPDBEntities();

        // GET: OpenPositions
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                var openPositions = db.OpenPositions.Include(o => o.Location).Where(o => o.Location.ManagerID == o.Location.UserDetail.UserID);
                return View(openPositions.ToList());
            }
            else
            {
                var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
                return View(openPositions.ToList());
            }
        }

        public ActionResult Apply(int id)
        {
            string userid = User.Identity.GetUserId();
            UserDetail ud = db.UserDetails.Find(userid);
            string resume = ud.ResumeFilename;
            if (resume == "NoImageAvalible.png")
            {
                Session["ErrorMessage"] = "Please upload a resume before applying.";
                return RedirectToAction("Edit", "UserDetails", new { id = ud.UserID });
            }


            Application application = new Application();
            application.UserID = userid;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatusID = 2;
            application.ResumeFilename = resume;
            application.ManagerNotes = null;
            application.OpenPositionID = id;

            db.Applications.Add(application);
            db.SaveChanges();
            return RedirectToAction("Index", "Applications");
        }


        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // GET: OpenPositions/Create
        [Authorize(Roles ="Admin, Manager")]
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title");
            return View();
        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionID,LocationID,PositionID")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Manager"))
                {
                    string userID = User.Identity.GetUserId();
                    openPosition.LocationID = db.Locations.Where(op => op.ManagerID == userID).Select(x => x.LocationID).FirstOrDefault();
                }
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber", openPosition.LocationID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // GET: OpenPositions/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber", openPosition.LocationID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionID,LocationID,PositionID")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "StoreNumber", openPosition.LocationID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // GET: OpenPositions/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
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
