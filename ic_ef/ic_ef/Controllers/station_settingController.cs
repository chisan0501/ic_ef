using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ic_ef;

namespace ic_ef.Controllers
{
    [Authorize(Roles ="Admin")]
    public class station_settingController : Controller
    {
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

        // GET: station_setting
        public ActionResult Index()
        {
            return View(db.station_setting.ToList());
        }

        // GET: station_setting/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            station_setting station_setting = db.station_setting.Find(id);
            if (station_setting == null)
            {
                return HttpNotFound();
            }
            return View(station_setting);
        }

        // GET: station_setting/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: station_setting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "station_name,station_dropdown_value,description")] station_setting station_setting)
        {
            if (ModelState.IsValid)
            {
                db.station_setting.Add(station_setting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(station_setting);
        }

        // GET: station_setting/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            station_setting station_setting = db.station_setting.Find(id);
            if (station_setting == null)
            {
                return HttpNotFound();
            }
            return View(station_setting);
        }

        // POST: station_setting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "station_name,station_dropdown_value,description")] station_setting station_setting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station_setting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station_setting);
        }

        // GET: station_setting/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            station_setting station_setting = db.station_setting.Find(id);
            if (station_setting == null)
            {
                return HttpNotFound();
            }
            return View(station_setting);
        }

        // POST: station_setting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            station_setting station_setting = db.station_setting.Find(id);
            db.station_setting.Remove(station_setting);
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
