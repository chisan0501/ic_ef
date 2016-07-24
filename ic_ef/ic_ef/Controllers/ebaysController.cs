using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ic_ef;
using eBay.Service.Core.Sdk;
using eBay.Service.Call;
using eBay.Service.Core.Soap;

namespace ic_ef.Controllers
{
    public class ebaysController : Controller
    {
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

        // GET: ebays
        
        public ActionResult Index()
        {
            string endpoint = "https://api.ebay.com/wsapi";
            string callName = "GetMyeBaySelling";
            string siteId = "0";
            string appId = "Intercon-intercon-PRD-f2f839365-96d3eb4b";     // use your app ID
            string devId = "cd3c0caa-6a1a-4038-841a-9a8d168aa5c9";     // use your dev ID
            string certId = "PRD-2f839365d132-ba64-4cc3-a7af-dfd4";   // use your cert ID
            string version = "967";
            // Build the request URL
            string requestURL = endpoint
            + "?callname=" + callName
            + "&siteid=" + siteId
            + "&appid=" + appId
            + "&version=" + version
            + "&routing=default";
            // Create the service
            eBayAPIInterfaceService service = new eBayAPIInterfaceService();
            // Assign the request URL to the service locator.
            service.Url = requestURL;
            // Set credentials
            service.RequesterCredentials = new CustomSecurityHeaderType();
            service.RequesterCredentials.eBayAuthToken = "AgAAAA**AQAAAA**aAAAAA**JsZZVw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wGkYSgDZOLpAqdj6x9nY+seQ**tlEDAA**AAMAAA**eKcOt7CkiMhdUxdu2zS1N1N3kXt2IqUE0eTuAMzKM4RwOu4gYg2nQzJ1VEHvU6KmFy4pSZzLChYa27P9QEbi44TVF9e/kiHzTs/VawcJ0fFJ77DmyROESmER+Js3pOhEk/+WGsufRX/YsIDk6Y6sTiQ/SmwPFWsQp7bmrs5DWhDzL5YlqbyAjGPOD9Vy/wNOov1jW/wSYQPvhH08K57AzSaBWpwxbDmk9KIb5sIhf7lbK1qw/R8QHDOV1mtr63r+VkzUcSELNNXrz1olTdfmR7SGbZBEjNaom7nnq2zGcIrAmBSL5GS0gyX+aM4XU8b80B9Eq0yjPx8UHwXnk6B6m36dkoVXSeO4oX3ZB9O1/CI8xirVz9+XOjQuXyt2xAYmgnoVCoMDTOKAL//6yi08vvpRSzzQYPDhSF6M1i7bUYIf+7Nh9pKl/KyWbdeXBYZn7mkfup9CcFO3UdE71YafZSHGoDJojkhnK8Af/TGzyp8zE0lPVsIaYgXPRl611dYTv/CKCSj1iqNq/MKSZ8biuYjKd9K4T3sWp3+Fbl+wR6z3ETF0VZol8nnE2yHS3zFYYv/L4gxWXeQSFPkZt2ZDaKFR/L4R6ru4MF/v7ADKj0GbpmSLpVDMdin19SX0D2mw8/fvcJxZtZp5NuD51PPilK8Yal2Wfv4AUWJlAgqUEFvmb1uMuNwH4R2dougyI+QLBq55hkbvZjnmgOTzR7HF1GLMMeaGPB6hbts+vP1tUaNS1B2/9JDLKUdQ6i7FD4C4";    // use your token
            service.RequesterCredentials.Credentials = new UserIdPasswordType();
            service.RequesterCredentials.Credentials.AppId = appId;
            service.RequesterCredentials.Credentials.DevId = devId;
            service.RequesterCredentials.Credentials.AuthCert = certId;
            // Make the call to GeteBayOfficialTime
            GetMyeBaySellingRequestType selling = new GetMyeBaySellingRequestType();
            selling.Version = "967";
            GetMyeBaySellingResponseType request_selling = service.GetMyeBaySelling(selling);

            GetAccountRequestType account = new GetAccountRequestType();
            account.Version = "967";
            GetAccountResponseType account_response = service.GetAccount(account);
            account_response.PageNumber = 2;
            var count = account_response.AccountEntries.AccountEntry.ToArray();
            foreach(var item in count)
            {
                string title = item.Title;
                

            }
           
            
            

            GeteBayOfficialTimeRequestType request = new GeteBayOfficialTimeRequestType();
            request.Version = "967";
            GeteBayOfficialTimeResponseType response = service.GeteBayOfficialTime(request);
            Console.WriteLine("The time at eBay headquarters in San Jose, California, USA, is:");
            Console.WriteLine(response.Timestamp);

            return View(db.ebay.ToList());
        }

        // GET: ebays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ebay ebay = db.ebay.Find(id);
            if (ebay == null)
            {
                return HttpNotFound();
            }
            return View(ebay);
        }

        // GET: ebays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ebays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "listID,title,time,start_price,end_price,end_time,start_time")] ebay ebay)
        {
            if (ModelState.IsValid)
            {
                db.ebay.Add(ebay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ebay);
        }

        // GET: ebays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ebay ebay = db.ebay.Find(id);
            if (ebay == null)
            {
                return HttpNotFound();
            }
            return View(ebay);
        }

        // POST: ebays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "listID,title,time,start_price,end_price,end_time,start_time")] ebay ebay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ebay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ebay);
        }

        // GET: ebays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ebay ebay = db.ebay.Find(id);
            if (ebay == null)
            {
                return HttpNotFound();
            }
            return View(ebay);
        }

        // POST: ebays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ebay ebay = db.ebay.Find(id);
            db.ebay.Remove(ebay);
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
