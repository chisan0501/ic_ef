using ic_ef.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
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
        db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

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
            string message = "";
            try
            {
                var existing = (from im in db.retail
                                where im.asset_tag.Equals(asset)
                                select im).SingleOrDefault();
                if (existing != null)
                {
                    
                   
                    existing.price = price;
                    existing.sold = "NO";
                    db.SaveChanges();
                }

                else
                {

                    var create_dymo = new retail();
                    create_dymo.time = DateTime.Today;
                    create_dymo.custom_label = "YES";
                    create_dymo.asset_tag = asset;
                    create_dymo.model = model;
                    create_dymo.cpu = cpu;
                    create_dymo.ram = ram;
                    create_dymo.sold = "NO";
                    create_dymo.hdd = hdd;
                    create_dymo.price = price;
                    db.retail.Add(create_dymo);
                    db.SaveChanges();
                    db.Dispose();
                }
                }
           catch (Exception e )
            {
                message = e.Message.ToString();
            }
            return Json(message ,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Retail_Dymo ()
        {
            return View();
        }

        public JsonResult mark_sold (string asset, string isSold)
        {
            int db_asset = int.Parse(asset);
            var sold = (from im in db.retail
                            where im.asset_tag.Equals(db_asset)
                            select im).SingleOrDefault();
            sold.sold = isSold;
            db.SaveChanges();

            return Json( JsonRequestBehavior.AllowGet);
        }

        //generate the list for inventroy 
        public JsonResult json_Retail_Dymo()
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(-30).AddTicks(-1);

            var exisit = (from t in db.retail
                          where (t.time >= endDateTime && t.time <= startDateTime && t.sold == "NO")
                          orderby t.time ascending
                          select t).ToList();
            ArrayList days = new ArrayList();
            foreach(var date in exisit)
            {
                TimeSpan difference = (startDateTime - date.time).Value;
                double diff_days = difference.TotalDays;
                days.Add(diff_days);
            }
            var result = new { item = exisit , days = days};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult write_RetailDymo(int asset_tag, double price, string manu, string cpu, string ram, string hdd,string sku)
        {
           
            
                var existing = (from im in db.retail
                                where im.asset_tag.Equals(asset_tag)
                                select im).SingleOrDefault();
                if (existing != null)
                {
                    existing.sold = "NO";
                    
                    existing.price = price;
                    db.SaveChanges();
                }
                else
                {
                    var create_dymo = new retail();
                    create_dymo.time = DateTime.Today;
                    create_dymo.custom_label = "NO";
                    create_dymo.sold = "NO";
                    create_dymo.asset_tag = asset_tag;
                    create_dymo.model = manu;
                    create_dymo.cpu = cpu;
                    create_dymo.ram = ram;
                    create_dymo.hdd = hdd;
                    create_dymo.price = price;
                    db.retail.Add(create_dymo);
                    db.SaveChanges();
                    db.Dispose();
                }

                
                string price_str = price.ToString();
                mage mage = new mage();
              bool success =  mage.update_price(sku, price_str);
            
            
            
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetailDymo (string asset, string price, string channel)
        {
            
            if (String.IsNullOrEmpty(asset))
            {
                asset = "0";
            }
            int ictag = int.Parse(asset);
            string real_memory = "";
            var result = (from m in db.rediscovery where m.ictag == ictag select m).SingleOrDefault();
            // var memory = (from m in db.rediscovery where m.ictag == ictag select m.ram).SingleOrDefault();
            string real_hdd = result.hdd + " GB";
            int temp_memory = int.Parse(result.ram);
            if (temp_memory < 20)
            {
                real_memory = temp_memory + " GB";
            }
            else
            {
                temp_memory = temp_memory / 1000;
                real_memory = temp_memory + " GB";
            }


            var multiple = new { ram = real_memory ,hdd= real_hdd, price = price , result = result,channel = channel };
            return Json(multiple, JsonRequestBehavior.AllowGet);
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