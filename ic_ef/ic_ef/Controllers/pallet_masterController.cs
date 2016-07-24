using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Reflection;
using System.Dynamic;

namespace ic_ef.Controllers
{
    [Authorize]
    public class pallet_masterController : Controller
    {
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

        // GET: pallet_master
        public ActionResult Index()
        {
            return View(db.pallet_master.ToList());
        }


        public void ExportCSV<T>(IEnumerable<T> list, string strFileName)
        {



            //IEnumerable<discovery> result = from p in db.discovery
            //                            select p;

            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
            Response.ContentType = "application/unkown";
            Response.ContentEncoding = Encoding.UTF8;

            GenerateCSV(list);


        }
        private void GenerateCSV<T>(IEnumerable<T> list)
        {
            Type t = typeof(T);

            //write data to front end
            StreamWriter sw = new StreamWriter(Response.OutputStream, Encoding.Default);

            object o = Activator.CreateInstance(t);

            PropertyInfo[] props = o.GetType().GetProperties();

            //output the header
            foreach (PropertyInfo pi in props)
            {
                sw.Write(pi.Name.ToUpper() + ",");
            }
            sw.WriteLine();

            //write out the list
            foreach (T item in list)
            {
                foreach (PropertyInfo pi in props)
                {

                    string whatToWrite =
                 Convert.ToString(item.GetType().GetProperty(pi.Name).GetValue(item, null)).Replace(',', ' ') + ',';
                    sw.Write(whatToWrite);

                }
                sw.WriteLine();

            }

            sw.Close();

        }

        public ActionResult export_detail_csv(string id)
        {
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + "_" + id + "_detail.csv";
            var pallet_id = from n in db.pallet where n.pallet_name == id select n.ictags;

            int[] strArray = pallet_id.ToArray();

            var result = from c in db.discovery
                         where strArray.Contains(c.ictag)
                        
                         select c;



            ExportCSV(result, file_name);
            return new EmptyResult();
        }

        public JsonResult check_asset ()
        {

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult bulk()
        {
            return View();
        }




       [HttpPost]
        public ActionResult bulk(HttpPostedFileBase FileUpload, string pallet_name)
        {
            string path = "";
            DataTable dt = new DataTable();

            //check we have a file
            if (FileUpload.ContentLength > 0)
            {


                //Workout our file path
                string fileName = Path.GetFileName(FileUpload.FileName);
                path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                try
                {
                    FileUpload.SaveAs(path);
                    List<string> temp_line = new List<string>();
                    using (StringReader reader = new StringReader(System.IO.File.ReadAllText(path)))
                    {
                        string line = null;
                        while ((line = reader.ReadLine()) != null)
                            if (!temp_line.Contains(line))
                                temp_line.Add(line);
                    }
                    using (StreamWriter writer = new StreamWriter(System.IO.File.Open(path, FileMode.Create)))

                    foreach (string r in temp_line)
                    {
                        var updateQuery = new pallet();
                        updateQuery.ictags = int.Parse(r);
                        updateQuery.pallet_name = pallet_name;
                            db.pallet.Add(updateQuery);
                        }
                    var palletmaster = new pallet_master();
                    palletmaster.pallet_id = pallet_name;
                    db.pallet_master.Add(palletmaster);
                    db.SaveChanges();
                   
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception ex)
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    //Catch errors
                    TempData["message"] = ex.ToString();

                }

            }
            return RedirectToAction("bulk");
        }

        public ActionResult export_csv(string id)
        {
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + "_" + id + ".csv";
            var pallet_id = from n in db.pallet where n.pallet_name == id select n;
            ExportCSV(pallet_id,file_name);
            return new EmptyResult();
        }


        

        public ActionResult View_pallet(string id) 
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // list from pallet table
            //var pallet_id = from n in db.pallet where n.pallet_name == id select n;
            //ViewBag.pallet_count = pallet_id.Count();
            //  ViewBag.data = pallet_id;

            //var pallet_ictags = (from n in db.pallet where n.pallet_name == id select n.ictags).ToList();

            //  //list from discoveries
            //var pallet_detail = (from n in db.discovery where pallet_ictags.Contains(n.ictag) select n);

          


            var innerjoin = (from o in db.pallet where o.pallet_name == id from h in db.discovery where h.ictag == o.ictags select new Models.detailViewModel  {ictags = o.ictags  , pallet_name = o.pallet_name, note = o.note, brand = h.brand,model= h.model, cpu =h.cpu,ram= h.ram,hdd= h.hdd }).ToList();

            ViewBag.palletCount = innerjoin.Count();
            ViewBag.data = innerjoin;

            ViewBag.currentid = id;


            if (innerjoin == null)
            {
                return HttpNotFound();
            }

         

            return View();

        }

        // GET: pallet_master/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pallet_master pallet_master = db.pallet_master.Find(id);
         
            if (pallet_master == null)
            {
                return HttpNotFound();
            }
            return View(pallet_master);
        }

        // GET: pallet_master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pallet_master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pallet_id")] pallet_master pallet_master)
        {
            if (ModelState.IsValid)
            {
                db.pallet_master.Add(pallet_master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pallet_master);
        }

        // GET: pallet_master/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pallet_master pallet_master = db.pallet_master.Find(id);
            if (pallet_master == null)
            {
                return HttpNotFound();
            }
            return View(pallet_master);
        }

        // POST: pallet_master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pallet_id")] pallet_master pallet_master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pallet_master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pallet_master);
        }

        // GET: pallet_master/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pallet_master pallet_master = db.pallet_master.Find(id);
            if (pallet_master == null)
            {
                return HttpNotFound();
            }
            return View(pallet_master);
        }

        // POST: pallet_master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            
           pallet_master pallet_master = db.pallet_master.Find(id);
            db.pallet_master.Remove(pallet_master);
            db.pallet.RemoveRange(db.pallet.Where(c => c.pallet_name == id));
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
