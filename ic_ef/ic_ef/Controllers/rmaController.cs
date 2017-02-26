using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ic_ef;
using ic_ef.SFWebReference;

namespace ic_ef.Controllers
{
    public class rmaController : Controller
    {
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

        // GET: rma
        public ActionResult Index()
        {

           // update_rma("2931");



            return View();
        }

        [AllowAnonymous]
        public ActionResult fetch_rma()
        {


            var rma_list = search_rma();
            foreach (var item in rma_list)
            {
                try
                {
                    var rma = new rma();
                    rma.ictag = item.IC_Barcodes__c;
                    if (!string.IsNullOrEmpty(item.IC_Barcodes__c) && item.IC_Barcodes__c.Contains("\n"))
                    {
                        string[] lines = item.IC_Barcodes__c.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            
                            rma = new rma();
                            var ar_temp_ictag = int.Parse(lines[i]);
                            rma.ictag = lines[i];
                            rma.id = item.Id;
                            rma.resolution_code = item.Return_Resolution__c;
                            rma.case_number = item.CaseNumber;
                            rma.description = item.Description;
                            rma.channel = item.Channel__c;
                            rma.date_requested = item.CreatedDate;
                            rma.production_finding = item.Production_Findings__c;
                            rma.rma_number = item.RMA_number__c;
                            var ar_serial = (from t in db.discovery where t.ictag == ar_temp_ictag select t.serial).FirstOrDefault();
                            rma.serial = ar_serial;
                            db.rma.Add(rma);
                            db.SaveChanges();
                        }
                        continue;

                    }
                    int temp_ictag = 0;
                    var sucess = int.TryParse(item.IC_Barcodes__c, out temp_ictag);
                    if (sucess == false)
                    {
                        continue;
                    }
                    rma.id = item.Id;
                    rma.description = item.Description;
                    rma.resolution_code = item.Return_Resolution__c;
                    rma.channel = item.Channel__c;
                    rma.date_requested = item.CreatedDate;
                    rma.case_number = item.CaseNumber;
                    rma.production_finding = item.Production_Findings__c;
                    rma.rma_number = item.RMA_number__c;
                    var serial = (from t in db.discovery where t.ictag == temp_ictag select t.serial).FirstOrDefault();
                    rma.serial = serial;
                    db.rma.Add(rma);
                    db.SaveChanges();
                    
                }
                catch
                {
                    continue;
                }
            }



            return View();
        }

        
        public JsonResult insert_finding (string asset, string finding)
        {
            var rma_num = (from t in db.rma where t.ictag == asset select t.rma_number).FirstOrDefault();
            string message = "";
          bool sucess =   update_rma(rma_num, finding);
            if (sucess == true)
            {
                message = "RMA Info Updated in SalesForce";
            }
            else
            {
               message = "Something has Went Wrong, RMA Info DID NOT get Updated";
            }

            return Json(new { message = message, finding = finding },JsonRequestBehavior.AllowGet);
        }
         
        public JsonResult get_rma (string asset_tag)
        {
           var asset = new production_log();
            var result = new rma();
            string message = "RMA Info Retrieved Successfully";
            try
            {
                
                result = (from t in db.rma where t.ictag == asset_tag select t).FirstOrDefault();
                 asset = (from t in db.production_log where t.ictags == asset_tag select t).FirstOrDefault();
                if (result == null)
                {
                    message = "No RMA Info Found with this Asset";
                }
            }
            catch ( Exception e)
            {
                message = e.Message;
            }
           


            return Json(new {asset = asset ,result = result, message = message } ,JsonRequestBehavior.AllowGet);

        }

        // GET: rma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rma rma = db.rma.Find(id);
            if (rma == null)
            {
                return HttpNotFound();
            }
            return View(rma);
        }

        // GET: rma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: rma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "row_id,serial,rma_number,production_finding,channel,date_requested,ictag,description")] rma rma)
        {
            if (ModelState.IsValid)
            {
                db.rma.Add(rma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rma);
        }

        // GET: rma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rma rma = db.rma.Find(id);
            if (rma == null)
            {
                return HttpNotFound();
            }
            return View(rma);
        }

        // POST: rma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "row_id,serial,rma_number,production_finding,channel,date_requested,ictag,description")] rma rma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rma);
        }


        public bool update_rma (string rma_num,string finding)
        {
            
            bool sucessful = false;
            string userName;
            string password;
            userName = "cdrain@interconnection.org";
            password = "3Emma3chaulk3!";
            //use default binding and address from app.config
            // string securityToken = "xxxxxxxxxxxxxxx";
            LoginResult currentLoginResult = null;
            SforceService sfdcBinding = null;

            var edit_case = (from t in db.rma where t.rma_number == rma_num select t).FirstOrDefault();
            using (var db = new db_a094d4_icdbEntities())
            {
                db.Database.ExecuteSqlCommand("Update rma set production_finding = '" + finding + "' where rma_number ='" + rma_num + "'");
            }
          

            sfdcBinding = new SforceService();
            currentLoginResult = sfdcBinding.login(userName, password);
            sfdcBinding.Url = currentLoginResult.serverUrl;
            sfdcBinding.SessionHeaderValue = new SessionHeader();
            sfdcBinding.SessionHeaderValue.sessionId = currentLoginResult.sessionId;
            var update_case = new Case();

            update_case.Reviewed_by_Production__c = true;
            update_case.Reviewed_by_Production__cSpecified = true;
            update_case.Id = edit_case.id;
            update_case.Production_Findings__c = finding;

            


            SaveResult[] saveResults = sfdcBinding.update(new sObject[] { update_case });
             
            string result = "";
            if (saveResults[0].success)
            {
                sucessful = true;
                result = "The update of Lead ID " + saveResults[0].id + " was succesful";
               
            }
            else
            {
                sucessful = false;
                result = "There was an error updating the Lead. The error returned was " + saveResults[0].errors[0].message;
            }




            return sucessful;
        }

        public List<Case> search_rma()
        {
            string userName;
            string password;
            userName = "cdrain@interconnection.org";
            password = "3Emma3chaulk3!";
            //use default binding and address from app.config
            // string securityToken = "xxxxxxxxxxxxxxx";

            SforceService sfdcBinding = null;
            LoginResult currentLoginResult = null;
            sfdcBinding = new SforceService();
            try
            {
                currentLoginResult = sfdcBinding.login(userName, password);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                // This is likley to be caused by bad username or password
                sfdcBinding = null;
                throw (ex);
            }
            catch (Exception ex)
            {
                // This is something else, probably comminication
                sfdcBinding = null;
                throw (ex);
            }


            //Change the binding to the new endpoint
            sfdcBinding.Url = currentLoginResult.serverUrl;

            //Create a new session header object and set the session id to that returned by the login
            sfdcBinding.SessionHeaderValue = new SessionHeader();
            sfdcBinding.SessionHeaderValue.sessionId = currentLoginResult.sessionId;

            QueryResult queryResult = null;
            String SOQL = "";
            List<Case> case_result = new List<Case>();
         
            // SOQL = "SELECT Id,Return_Resolution__c,Description ,CaseNumber,IC_Barcodes__c,RMA_number__c,Production_Findings__c,Channel__c,CreatedDate FROM case where CreatedDate >=2017-01-01T12:00:00Z AND CreatedDate  <=2017-05-31T12:00:00Z";
            SOQL = "SELECT Id,Return_Resolution__c,Description,CaseNumber,IC_Barcodes__c,RMA_number__c,Production_Findings__c,Channel__c,CreatedDate FROM case where CreatedDate = YESTERDAY";
            queryResult = sfdcBinding.query(SOQL);
            for (int i = 0; i < queryResult.size; i++)
            {
               
                case_result.Add((Case)queryResult.records[i]);

            }
           


            return case_result;
        }
        // GET: rma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rma rma = db.rma.Find(id);
            if (rma == null)
            {
                return HttpNotFound();
            }
            return View(rma);
        }

        // POST: rma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rma rma = db.rma.Find(id);
            db.rma.Remove(rma);
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
