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
    public class rediscoveriesController : Controller
    {
        private ic_databaseEntities2 db = new ic_databaseEntities2();

        // GET: rediscoveries
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

            var machine = (from t in db.rediscovery
                          where (t.time >= startDateTime && t.time <= endDateTime)
                           select t);
            if (!String.IsNullOrEmpty(searchString))
            {
                int temp_searchString = int.Parse(searchString);
                machine = (from t in db.rediscovery
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
                case "pallet_desc":
                    machine = machine.OrderByDescending(s => s.pallet);
                    break;
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


    

        // GET: rediscoveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rediscovery rediscovery = db.rediscovery.Find(id);
            if (rediscovery == null)
            {
                return HttpNotFound();
            }
            return View(rediscovery);
        }

        // GET: rediscoveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: rediscoveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ictag,time,serial,brand,model,cpu,hdd,ram,optical_drive,location,pallet,pre_coa,refurbisher")] rediscovery rediscovery)
        {
            if (ModelState.IsValid)
            {
                db.rediscovery.Add(rediscovery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rediscovery);
        }

        // GET: rediscoveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rediscovery rediscovery = db.rediscovery.Find(id);
            if (rediscovery == null)
            {
                return HttpNotFound();
            }
            return View(rediscovery);
        }

        // POST: rediscoveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ictag,time,serial,brand,model,cpu,hdd,ram,optical_drive,location,pallet,pre_coa,refurbisher")] rediscovery rediscovery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rediscovery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rediscovery);
        }

        // GET: rediscoveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rediscovery rediscovery = db.rediscovery.Find(id);
            if (rediscovery == null)
            {
                return HttpNotFound();
            }
            return View(rediscovery);
        }

        // POST: rediscoveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rediscovery rediscovery = db.rediscovery.Find(id);
            db.rediscovery.Remove(rediscovery);
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
