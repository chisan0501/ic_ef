using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ic_ef.Controllers
{
    [Authorize]
    public class palletsController : Controller
    {
        private db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();

        // GET: pallets
        

  

        public async Task<ActionResult> Index(string searchString)
        {
            //Login();
         
            
           

                var data = from p in db.pallet_master
                       select new
                       {
                           pallet = p.pallet_id,

                       };

            SelectList list = new SelectList(data, "pallet", "pallet");
            ViewBag.Roles = list;

            var pallet = from m in db.pallet
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                pallet = pallet.Where(s => s.pallet_name.Contains(searchString));
            }

            ViewBag.FilterValue = searchString;
            return View(pallet);

        }
     
        // GET: pallets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pallet pallet = db.pallet.Find(id);
            if (pallet == null)
            {
                return HttpNotFound();
            }
            return View(pallet);
        }

        // GET: pallets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pallets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ictags,pallet_name,type,note")] pallet pallet)
        {
            if (ModelState.IsValid)
            {
                db.pallet.Add(pallet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pallet);
        }

        // GET: pallets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pallet pallet = db.pallet.Find(id);
            if (pallet == null)
            {
                return HttpNotFound();
            }
            return View(pallet);
        }

        // POST: pallets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ictags,pallet_name,type,note")] pallet pallet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pallet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pallet);
        }

        // GET: pallets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pallet pallet = db.pallet.Find(id);
            if (pallet == null)
            {
                return HttpNotFound();
            }
            return View(pallet);
        }

        // POST: pallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pallet pallet = db.pallet.Find(id);
            db.pallet.Remove(pallet);
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
