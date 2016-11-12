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
        public ActionResult landing()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult search_to_print (string asset)
        {
            int temp_asset = int.Parse(asset);
            var result = (from t in db.mac_log
                          where (t.ictags == temp_asset)
                          select t).FirstOrDefault();
            string Model = result.Model;
            string cpu = result.cpu;
            string serial = result.serial;
            string ram = result.ram;
            string hdd = result.hdd;
            var time = DateTime.Today.ToString("yyyy-MM-dd");
            string channel = result.pallet;
            string user = result.refurbisher;
            return Json(new { model = Model, cpu = cpu, serial = serial, ram = ram, hdd = hdd, time = time,user=user,channel=channel }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult readapple(string[] arr, bool is_db, string channel, string user, string asset_tag, string hddchecked)
        {
            try
            { 
                var Model = Array.FindAll(arr, s => s.Contains("Model Identifier")).FirstOrDefault(); ;
                Model = Model.Substring(Model.IndexOf(':') + 1).ToString();
                Model = Model.Trim();
                var cpu = Array.FindAll(arr, s => s.Contains("Processor Name")).FirstOrDefault(); ;
                cpu = cpu.Substring(cpu.IndexOf(':') + 1).ToString();
                cpu = cpu.Trim();
                var cpu_speed = Array.FindAll(arr, s => s.Contains("Processor Speed")).FirstOrDefault(); ;
                cpu_speed = cpu_speed.Substring(cpu_speed.IndexOf(':') + 1).ToString();
                cpu_speed = cpu_speed.Trim();
                cpu = cpu + " @ " + cpu_speed;
                var serial = Array.FindAll(arr, s => s.Contains("Serial Number (system)")).FirstOrDefault(); ;
                serial = serial.Substring(serial.IndexOf(':') + 1).ToString();
                serial = serial.Trim();
                var ram = Array.FindAll(arr, s => s.Contains("Memory: ")).FirstOrDefault();
                ram = ram.Substring(ram.IndexOf(':') + 1).ToString();
                ram = ram.Trim();

            string hdd = "";
            if (hddchecked == "false")
            {
                hdd = Array.FindAll(arr, s => s.Contains("Capacity:")).FirstOrDefault();
                hdd = hdd.Substring(hdd.IndexOf(':') + 1).ToString();
                int index = hdd.IndexOf("(");
                if (index > 0)
                    hdd = hdd.Substring(0, index);
                hdd = hdd.Trim();
            }
            else
            {
                hdd = "0GB";
            }


            var time = DateTime.Today.ToString("yyyy-MM-dd");
               
                if (is_db == true)
                {
                    int temp_asset_tag = int.Parse(asset_tag);
                    write_macDymo(temp_asset_tag, channel, serial, Model, cpu, ram, hdd, time, user);
                }


                return Json(JsonRequestBehavior.AllowGet);    
            }
           catch (Exception e)
            {
                return Json(new { result = e }, JsonRequestBehavior.AllowGet);
            }
           
        }
        [HttpGet]
        public JsonResult get_channel ()
        {
            var menu = (from t in db.label_menu
                                     where (t.name == "MAC")
                                     select t.product).ToList();
            return Json(menu,JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_user()
        {
            var users = (from t in db.users
                        
                        select t.user_name).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult apple ()
        {
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
        public JsonResult write_macDymo(int asset_tag, string product_name, string serial ,string manu, string cpu, string ram, string hdd, string time,string user)
        {


           
                var create_dymo = new mac_log();
                create_dymo.Time = DateTime.Today;
            create_dymo.refurbisher = user;
            create_dymo.pallet = product_name;
                create_dymo.ictags = asset_tag;
                create_dymo.Model = manu;
                create_dymo.cpu = cpu;
                create_dymo.ram = ram;
                create_dymo.hdd = hdd;
            create_dymo.serial = serial;
                db.mac_log.Add(create_dymo);
                db.SaveChanges();
                db.Dispose();
   


         return Json(new { success = "true"}, JsonRequestBehavior.AllowGet);       
        }


        //this is originally a function to record item input and sold date, but it was abandon due to lack of usage
        //update price on click 
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

                //update price to magento system after price label have printed out 
                string price_str = price.ToString();
                mage mage = new mage();
              bool success =  mage.update_price(sku, price_str);
            
            
            
            return Json(new { success = success }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RetailDymo_mac(string asset, string price, string channel)
        {

            if (String.IsNullOrEmpty(asset))
            {
                asset = "0";
            }
            int ictag = int.Parse(asset);
           
            var result = (from m in db.mac_log where m.ictags == ictag select m).SingleOrDefault();
            // var memory = (from m in db.rediscovery where m.ictag == ictag select m.ram).SingleOrDefault();
            
            


            var multiple = new { ram = result.ram, hdd = result.hdd, price = price, result = result, channel = channel };
            return Json(multiple, JsonRequestBehavior.AllowGet);
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