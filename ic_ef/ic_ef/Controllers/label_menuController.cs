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
    public class label_menuController : Controller
    {
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

        // GET: label_menu
        public ActionResult Index()
        {
            return View(db.label_menu.ToList());
        }

        // GET: label_menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            label_menu label_menu = db.label_menu.Find(id);
            if (label_menu == null)
            {
                return HttpNotFound();
            }
            return View(label_menu);
        }

        // GET: label_menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: label_menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,product,rowID")] label_menu label_menu)
        {
            if (ModelState.IsValid)
            {
                db.label_menu.Add(label_menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(label_menu);
        }

        // GET: label_menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            label_menu label_menu = db.label_menu.Find(id);
            if (label_menu == null)
            {
                return HttpNotFound();
            }
            return View(label_menu);
        }

        // POST: label_menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name,product,rowID")] label_menu label_menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(label_menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(label_menu);
        }

        // GET: label_menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            label_menu label_menu = db.label_menu.Find(id);
            if (label_menu == null)
            {
                return HttpNotFound();
            }
            return View(label_menu);
        }

        // POST: label_menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            label_menu label_menu = db.label_menu.Find(id);
            db.label_menu.Remove(label_menu);
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
