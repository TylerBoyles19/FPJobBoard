using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPJobBoard.Data.EF;
using FPJobBoard.UI.EF.Models;

namespace FPJobBoard.UI.EF.Controllers
{
    [Authorize(Roles = "Employee, Manager, Admin")]
    public class LocationsController : Controller
    {
        private FPDBEntities db = new FPDBEntities();

        // GET: Locations
        public ActionResult Index(string divId)
        {
            ViewBag.Scroll = divId;

            var locations = db.Locations.Include(l => l.UserDetail).Where(l => l.ManagerID == l.UserDetail.UserID);
            return View(locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id, string divId)
        {
            ViewBag.Scroll = divId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create(string divId)
        {
            ViewBag.Scroll = divId;

            var context = new ApplicationDbContext();            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("d8b72df0-558c-4e6b-b269-a4da58f987d3")).Select(x => x.Id).ToList();            ViewBag.ManagerID = new SelectList(db.UserDetails.Where(x => users.Contains(x.UserID)), "UserID", "FirstName");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,StoreNumber,City,State,ManagerID")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index", new { divId="ScrollDivID"});
            }

            var context = new ApplicationDbContext();            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("d8b72df0-558c-4e6b-b269-a4da58f987d3")).Select(x => x.Id).ToList();            ViewBag.ManagerID = new SelectList(db.UserDetails.Where(x => users.Contains(x.UserID)), "UserID", "FirstName");
            return View(location);
        }

        // GET: Locations/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id, string divId)
        {
            ViewBag.Scroll = divId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            var context = new ApplicationDbContext();            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("d8b72df0-558c-4e6b-b269-a4da58f987d3")).Select(x => x.Id).ToList();            ViewBag.ManagerID = new SelectList(db.UserDetails.Where(x => users.Contains(x.UserID)), "UserID", "FirstName");
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,StoreNumber,City,State,ManagerID")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { divId="ScrollDivID"});
            }
            var context = new ApplicationDbContext();            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains("d8b72df0-558c-4e6b-b269-a4da58f987d3")).Select(x => x.Id).ToList();            ViewBag.ManagerID = new SelectList(db.UserDetails.Where(x => users.Contains(x.UserID)), "UserID", "FirstName");
            return View(location);
        }

        // GET: Locations/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, string divId)
        {
            ViewBag.Scroll = divId;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index", new { divId="ScrollDivID"});
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
