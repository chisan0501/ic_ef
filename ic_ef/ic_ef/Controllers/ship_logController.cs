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
    public class ship_logController : Controller
    {
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

        // GET: ship_log
        public ActionResult Index()
        {
            return View(db.ship_log.ToList());
        }

        // GET: ship_log/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship_log ship_log = db.ship_log.Find(id);
            if (ship_log == null)
            {
                return HttpNotFound();
            }
            return View(ship_log);
        }

        // GET: ship_log/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ship_log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ictag,shipdate,status,order_id,order_num,row_id,store_id,sku")] ship_log ship_log)
        {
            if (ModelState.IsValid)
            {
                db.ship_log.Add(ship_log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ship_log);
        }

        // GET: ship_log/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship_log ship_log = db.ship_log.Find(id);
            if (ship_log == null)
            {
                return HttpNotFound();
            }
            return View(ship_log);
        }

        // POST: ship_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ictag,shipdate,status,order_id,order_num,row_id,store_id,sku")] ship_log ship_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ship_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ship_log);
        }

        // GET: ship_log/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ship_log ship_log = db.ship_log.Find(id);
            if (ship_log == null)
            {
                return HttpNotFound();
            }
            return View(ship_log);
        }

        // POST: ship_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ship_log ship_log = db.ship_log.Find(id);
            db.ship_log.Remove(ship_log);
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
