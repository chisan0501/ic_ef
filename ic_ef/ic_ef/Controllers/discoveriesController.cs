using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ic_ef;
using PagedList;

namespace ic_ef.Controllers
{
    public class discoveriesController : Controller
    {
        private db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();


        public ActionResult asset_detail ()
        {
            return View();
        }

        // GET: discoveries
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ChannelSortParm = String.IsNullOrEmpty(sortOrder) ? "channel_desc" : "";
            ViewBag.ModelSortParm = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "";
            ViewBag.PalletSortParm = String.IsNullOrEmpty(sortOrder) ? "pallet_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var machine = (from t in db.discovery
                           where (t.time >= startDateTime && t.time <= endDateTime)
                           select t);
            if (!String.IsNullOrEmpty(searchString))
            {
                int temp_searchString = int.Parse(searchString);
                machine = (from t in db.discovery
                           where (t.ictag == temp_searchString)
                           select t);
            }
            //    var machine = (from t in db.rediscovery

            //    select t);
            switch (sortOrder)
            {
                case "name_desc":
                    machine = machine.OrderByDescending(s => s.ictag);
                    break;
                case "channel_desc":
                    machine = machine.OrderByDescending(s => s.location);
                    break;
                //case "pallet_desc":
                //    machine = machine.OrderByDescending(s => s.pallet);
                //    break;
                case "model_desc":
                    machine = machine.OrderByDescending(s => s.model);
                    break;
                case "Date":
                    machine = machine.OrderBy(s => s.time);
                    break;
                case "date_desc":
                    machine = machine.OrderByDescending(s => s.time);
                    break;
                default:  // Name ascending 
                    machine = machine.OrderBy(s => s.ictag);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(machine.ToPagedList(pageNumber, pageSize));
        }

        // GET: discoveries/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {  
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discovery discovery = db.discovery.Find(id);
            if (discovery == null)
            {
                return HttpNotFound();
            }
            
            return View(discovery);
            
 
        }

        // GET: discoveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: discoveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ictag,time,serial,brand,model,cpu,hdd,ram,optical_drive,location")] discovery discovery)
        {
            if (ModelState.IsValid)
            {
                db.discovery.Add(discovery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discovery);
        }

        // GET: discoveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discovery discovery = db.discovery.Find(id);
            if (discovery == null)
            {
                return HttpNotFound();
            }
            return View(discovery);
        }

        // POST: discoveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ictag,time,serial,brand,model,cpu,hdd,ram,optical_drive,location")] discovery discovery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discovery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discovery);
        }

        // GET: discoveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discovery discovery = db.discovery.Find(id);
            if (discovery == null)
            {
                return HttpNotFound();
            }
            return View(discovery);
        }

        // POST: discoveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            discovery discovery = db.discovery.Find(id);
            db.discovery.Remove(discovery);
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
