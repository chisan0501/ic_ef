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
    public class monitor_logController : Controller
    {
        private ic_databaseEntities2 db = new ic_databaseEntities2();

        // GET: monitor_log
        public ActionResult Index()
        {
            return View(db.monitor_log.ToList());
        }

        [HttpPost]
        public JsonResult get_asset(int asset)
        {
            try
            {
                var result = (from im in db.monitor_log where im.ictag == asset select im).SingleOrDefault();
                if (result == null)
                {
                    return Json(new { success = false, responseText = "Asset Not Found" }, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "Asset Tag can only be Numbers" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult monitor_date(string date)
        {
            try
            {
                DateTime start_date = DateTime.Parse(date);
                DateTime end_date = start_date.AddDays(1).AddTicks(-1);
                var existing = (from im in db.monitor_log
                                where im.time >= start_date && im.time <= end_date
                                select im).ToList();

                return Json(existing, JsonRequestBehavior.AllowGet);
            }
          
           catch
            {
                return Json(new { success = false, responseText = "There seem to be an error" }, JsonRequestBehavior.AllowGet);
            }
            

        }
        // GET: monitor_log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monitor_log monitor_log = db.monitor_log.Find(id);
            if (monitor_log == null)
            {
                return HttpNotFound();
            }
            return View(monitor_log);
        }

        // GET: monitor_log/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: monitor_log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "time,manu,monitor_ID,serial,size,resou,model,ictag")] monitor_log monitor_log)
        {
            if (ModelState.IsValid)
            {
                db.monitor_log.Add(monitor_log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monitor_log);
        }

        // GET: monitor_log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monitor_log monitor_log = db.monitor_log.Find(id);
            if (monitor_log == null)
            {
                return HttpNotFound();
            }
            return View(monitor_log);
        }

        // POST: monitor_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "time,manu,monitor_ID,serial,size,resou,model,ictag")] monitor_log monitor_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monitor_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monitor_log);
        }

        // GET: monitor_log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monitor_log monitor_log = db.monitor_log.Find(id);
            if (monitor_log == null)
            {
                return HttpNotFound();
            }
            return View(monitor_log);
        }

        // POST: monitor_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            monitor_log monitor_log = db.monitor_log.Find(id);
            db.monitor_log.Remove(monitor_log);
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
