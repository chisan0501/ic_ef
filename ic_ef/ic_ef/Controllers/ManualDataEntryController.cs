using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Semantics3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Data.Entity.Migrations;


namespace ic_ef.Controllers
{
    public class ManualDataEntryController : Controller
    {
        db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();
        public JsonResult get_setting()
        {
           
            var hdd_menu = (from t in db.create_asset_menu where t.type == "hdd" select t).ToList();
            var ram_menu = (from y in db.create_asset_menu where y.type == "ram" select y).ToList();
            var cpu_menu = (from x in db.create_asset_menu where x.type == "cpu" select x).ToList();
            return Json(new { hdd_menu = hdd_menu, ram_menu = ram_menu,cpu_menu = cpu_menu} ,JsonRequestBehavior.AllowGet);
        }


        // GET: ManualDataEntry
        public ActionResult Index()
        {
          
            return View();
        }

        [HttpPost]

        public ActionResult edit_discovery_asset(string hdd, string ram, string sku, string pre_coa, string refurbisher, string asset )
        {

            var temp_asset = int.Parse(asset);
           var discovery = db.discovery.Where(s => s.ictag == temp_asset ).FirstOrDefault<discovery>();
           // var discovery = new discovery();
            discovery.hdd = hdd;
            discovery.ram = ram;
            db.discovery.AddOrUpdate(discovery);
            db.SaveChanges();



            return View("edit_asset",this);

        }
        [HttpPost]

        public ActionResult edit_rediscovery_asset(string hdd, string ram, string sku, string pre_coa, string refurbisher, string asset)
        {


            var temp_asset = int.Parse(asset);
            var rediscovery = db.rediscovery.Where(s => s.ictag == temp_asset).FirstOrDefault<rediscovery>();
            rediscovery.hdd = hdd;
            rediscovery.ram = ram;
            rediscovery.pallet = sku;
            rediscovery.pre_coa = pre_coa;
            rediscovery.refurbisher = refurbisher;
            db.rediscovery.AddOrUpdate(rediscovery);
            db.SaveChanges();



            return View("edit_asset", this);
        }
        [HttpPost]

        public ActionResult edit_img_asset(string hdd, string ram, string sku, string pre_coa, string refurbisher, string serial,string asset)
        {

            var temp_asset = int.Parse(asset);
            var imaging = db.production_log.Where(s => s.serial == serial).FirstOrDefault<production_log>();
            imaging.HDD = hdd;
            imaging.RAM = ram;
            imaging.channel = sku;
            imaging.pre_coa = pre_coa;
            
            db.production_log.AddOrUpdate(imaging);
            db.SaveChanges();

            

            return View("edit_asset", this);

        }

        [HttpPost]
        //add new database entry for discover data
        public ActionResult insert_asset(int asset, string brand, string model, string refrub, string download,string cpu_brand, string cpu_type, string cpu_speed, string ram,string hdd, string serial)
        {



            var cpu = cpu_brand + " " + cpu_type +" " +cpu_speed;
           var xml = mrm_xml(asset.ToString(),brand,model,hdd,ram,serial,cpu);


            var db = new db_a094d4_icdbEntities1();
           
            var exisit = (from t in db.discovery where t.ictag == asset select t).ToList();
            if (exisit.Count == 0)
            {
                using (db as db_a094d4_icdbEntities1) {

                    var new_entry = new discovery();
                    new_entry.brand = brand;
                    new_entry.cpu = cpu;
                    new_entry.hdd = hdd;
                    new_entry.ictag = asset;
                    new_entry.location = null;
                    new_entry.model = model;
                    new_entry.optical_drive = null;
                    new_entry.ram = ram;
                    new_entry.serial = serial;
                    new_entry.time = DateTime.Now;

                    db.discovery.Add(new_entry);
                    db.SaveChanges();

                    TempData["message"] = "Data Imported";
                }
                if (refrub == "on")
                {
                    using (var db2 = new db_a094d4_icdbEntities1())
                    {

                        var new_entry = new rediscovery();
                        new_entry.brand = brand;
                        new_entry.cpu = cpu;
                        new_entry.hdd = hdd;
                        new_entry.ictag = asset;
                        new_entry.location = null;
                        new_entry.model = model;
                        new_entry.optical_drive = null;
                        new_entry.ram = ram;
                        new_entry.serial = serial;
                        new_entry.time = DateTime.Now;

                        db2.rediscovery.Add(new_entry);
                        db2.SaveChanges();

                        TempData["message"] = "Discovery and Rediscovery Data Imported";
                    }
                }
                if (download == "on")
                {
                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);
                    xml.WriteTo(writer);
                    writer.Flush();
                    Response.Clear();
                    byte[] byteArray = stream.ToArray();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + asset.ToString() + ".xml");
                    Response.AppendHeader("Content-Length", byteArray.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.BinaryWrite(byteArray);
                    writer.Close();


                    Response.End();
                }
                }

            else
            {
                TempData["message"] = "an error has occurred";
            }
            return View();
        }


        public JsonResult fetch_data(string search,string vendor)
        {
            List<Dictionary<string,string>> feature_dictionary = new List<Dictionary<string, string>>();
             
            var hashResponse = Semantic3(search,vendor);

            if ((int)hashResponse["results_count"] > 0)
            {
                JArray arrayProducts = (JArray)hashResponse["results"];

                // For each product in the results
                for (int i = 0; i < arrayProducts.Count; i++)
                {
                    var item = new Models.ProductViewModel();
                    item.title =  (String)arrayProducts[i]["name"];
                    item.model = (String)arrayProducts[i]["model"];
                    item.brand = (String)arrayProducts[i]["brand"];


                    var temp_data = new Dictionary<string, string>();
                    // spec info
                    JObject spec = (JObject)arrayProducts[i]["features"];
                    foreach(var f in spec)
                    {
                        temp_data.Add(f.Key, f.Value.ToString());
                        
                    }
                    feature_dictionary.Add(temp_data);

                    //item.ram = (String)spec["Memory"];
                    item.hdd = (String)spec["Hard Drive"];
                    String cpu_name = (String)spec["Processor"];
                    //string cpu_speed = (String)spec["Base Clock Speed"];
                    //item.cpu = cpu_name + "@" + cpu_speed;


                    JArray ecommerceStores = (JArray)arrayProducts[i]["sitedetails"];
                    for (int k = 0; k < ecommerceStores.Count; k++)
                    {
                        

                        // Latest offers from the ecommerceStore
                        for (int s = 0; s < ((JArray)ecommerceStores[k]["latestoffers"]).Count; s++)
                        {
                            item.seller = (String)ecommerceStores[k]["latestoffers"][s]["seller"];
                          

                            // Preprocess and use in application ...

                        }
                        // Parsed offers
                    }

                 
                    // Parsed offers

                    // Loop Ecommerse stores

                }
                // Loop each product in the api response

            }



            return Json(feature_dictionary,JsonRequestBehavior.AllowGet);
        }

        public XDocument mrm_xml (string asset_tag, string brand, string model, string hdd,string ram,string serial, string cpu)
        {
            int temp_ram = int.Parse(ram);
            ulong bytes = (ulong)temp_ram * 1024 * 1024 * 1024;
          
            hdd = hdd + " GB";
            XDocument doc = new XDocument(new XElement("Computer",new XAttribute("Barcode",asset_tag),
                                           new XElement("components", new XAttribute("name", "system"),
                                               new XElement("component", new XAttribute("name", "Manufacturer"), brand),
                                               new XElement("component", new XAttribute("name", "Model"), model)), new XElement("components", new XAttribute("name", "HDD"),
                                               new XElement("component", new XAttribute("name", "InterfaceType"), "SATA"),
                                               new XElement("component", new XAttribute("name", "Manufacturer"), "(Standard disk drives)"), new XElement("component", new XAttribute("name", "Model"), "Not Applicable"), new XElement("component", new XAttribute("name", "HDDSerialNumber"), "None"), new XElement("component", new XAttribute("name", "Size"), hdd)),new XElement("components", new XAttribute("name", "RAM"),
                                               new XElement("component", new XAttribute("name", "DIMMCapacity"), bytes.ToString())), new XElement("components", new XAttribute("name", "Motherboard"),
                                               new XElement("component", new XAttribute("name", "Manufacturer"), brand), new XElement("component", new XAttribute("name", "MBSerialNumber"), serial)), new XElement("components", new XAttribute("name", "Processor"), new XElement("component", new XAttribute("name", "Name"), cpu))));
            return doc;

            
        }

        public JsonResult get_asset_data(int asset)
        {
            var db = new db_a094d4_icdbEntities1();
            var rediscovery = (from t in db.rediscovery where t.ictag == asset select t).FirstOrDefault();
            var discovery = (from t in db.discovery where t.ictag == asset select t).FirstOrDefault();
            var img = (from t in db.production_log where t.ictags == asset.ToString() select t).FirstOrDefault();

            return Json(new {discovery =discovery,rediscovery = rediscovery,img = img } , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult edit_asset(string ram, string hdd, string channel)
        {


            return View();
        }

        public ActionResult edit_asset()
        {


            return View();
        }


        public JsonResult get_asset_data_mac (string asset)
        {

            var temp_asset = int.Parse(asset);

            var result = (from t in db.mac_log where t.ictags == temp_asset select t).FirstOrDefault();
    
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult edit_asset_mac(string asset, string hdd, string ram, string serial, string refurbisher, string sku)
        {
            var temp_asset = int.Parse(asset);
            var mac = db.mac_log.Where(s => s.ictags == temp_asset).FirstOrDefault<mac_log>();
            // var discovery = new discovery();
            mac.refurbisher = refurbisher;
            mac.hdd = hdd;
            mac.ram = ram;
            mac.pallet = sku;
            db.mac_log.AddOrUpdate(mac);
            db.SaveChanges();



            return View("edit_asset_mac", this);


            
        }

        public ActionResult edit_asset_mac()
        {


            return View();
        }

        public ActionResult insert_asset ()
        {


            return View();

        }

        public JObject Semantic3(string keyword,string vendor) 
        {
            Products products = new Products("SEM337947B707DCFE40934FE4CBE643D4E02",
                                             "YTczZjFjZGE0NzllYTQ3MmJiN2JlNWVhZjRkNDUwNjE");

            products.products_field("search" , keyword);
            products.products_field("site" , vendor);

            JObject hashResponse = products.get_products();


            return hashResponse;
            // If the above query resulted in some results
        }

    }
}