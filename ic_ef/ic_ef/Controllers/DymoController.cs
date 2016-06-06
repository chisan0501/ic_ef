using ic_ef.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ic_ef.Controllers
{
    [Authorize]
    public class DymoController : Controller
    {
        // GET: 
        ic_databaseEntities2 db = new ic_databaseEntities2();

        [HttpPost]
        public ActionResult Index(int? asset,string pre_coa,string product, DymoViewModel model,string myList)
        {
            if (string.IsNullOrEmpty(pre_coa) == true)
            {
                pre_coa = "00999-999-000-999";
            }
            ViewBag.user = User.Identity.GetUserName();
            ViewBag.channel = myList;
            ViewBag.pre_coa = pre_coa;
           ViewBag.pallet = product;
            ViewBag.asset = asset;
            ViewBag.time = DateTime.Now;
            var current_machine = (from m in db.rediscovery where m.ictag == asset select m).ToList();
            // start update query
            var update = db.rediscovery.Single(u => u.ictag == asset);
            update.pre_coa = pre_coa;
            update.pallet = product;
            update.location = myList;
            update.refurbisher = User.Identity.GetUserName();
            db.SaveChanges();
            //end update query

            //iterate asset obj
            foreach (var item in current_machine)
            {
              
                ViewBag.CPU = item.cpu;
                ViewBag.HDD = item.hdd + " GB";
                int temp_ram = int.Parse(item.ram);
                temp_ram = temp_ram / 1000;

                ViewBag.Ram = temp_ram + " GB";

                ViewBag.Model = item.model;
                ViewBag.Serial = item.serial;
            }
            var channel_list = db.label_menu
                                    .Select(p => p.name)
                                    .Distinct().ToList();
            SelectList list = new SelectList(channel_list);
            ViewBag.myList = list;
            return View();
        }

        [HttpPost]
        public ActionResult Print (int? asset, string pre_coa, string product,string myList)
        {
           
          
            return RedirectToAction("index");
        }
        [HttpPost]
        public JsonResult create_dymo (int asset,string model, string cpu, string ram, string hdd, double price)
        {
            var create_dymo = new retail();
            create_dymo.time = DateTime.Today;
            create_dymo.custom_label = "YES";
            create_dymo.asset_tag = asset;
            create_dymo.model = model;
            create_dymo.cpu = cpu;
            create_dymo.ram = ram;
            create_dymo.hdd = hdd;
            create_dymo.price = price;
            db.retail.Add(create_dymo);
            db.SaveChanges();
            db.Dispose();
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult Retail_Dymo()
        {
            return View();
        }
        
        public JsonResult RetailDymo (string asset)
        {
            if (String.IsNullOrEmpty(asset))
            {
                asset = "0";
            }
            int ictag = int.Parse(asset);

            var result = (from m in db.rediscovery where m.ictag == ictag select m);
           

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
           
            var channel_list = db.label_menu
                                  .Select(p => p.name)
                                  .Distinct().ToList() ;
            SelectList list = new SelectList(channel_list);
            ViewBag.myList = list;
            
            
          
            var last_five = user_info();
            return View(last_five.ToList());
        }

        public JsonResult GetProduct(string product)
        {
            var p_list = new List<string>();
            var product_list = (from m in db.label_menu where m.name ==  product select m.product).ToList();

            foreach (var a in product_list)
            {
                p_list.Add(a);
            }
           

            //Add JsonRequest behavior to allow retrieving states over http get
            return Json(p_list, JsonRequestBehavior.AllowGet);
        }


        public rediscovery[] user_info()
        {
            var manager = new UserManager<ApplicationUser>(
 new UserStore<ApplicationUser>(
     new ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            string name = user.UserName.ToString();
            ViewBag.refurb_user = name;
            var last_five_machine = (from m in db.rediscovery where m.refurbisher.Equals(name) orderby m.time descending select m).Take(5).ToArray();
            
            return last_five_machine;

        }

  
    }
}