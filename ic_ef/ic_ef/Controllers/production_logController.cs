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
using HtmlAgilityPack;
using Newtonsoft.Json;
using ic_ef.org.connectall;
using System.Collections;

namespace ic_ef.Controllers
{


    public class production_logController : Controller
    {
        public string oem_des = "";
        public string mar_lap = "";
        public string mar_des = "";
        public string oem_lap = "";
        public string apple = "";
        public string oem_des_current = "";
        public string mar_lap_current = "";
        public string mar_des_current = "";
        public string oem_lap_current = "";
        public string apple_current = "";
        
        private ic_databaseEntities2 db = new ic_databaseEntities2();
        Models.magentoViewModel magentoView = new Models.magentoViewModel();

        //read json object from the web
        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
        //fetch the lastest sku from magento site
        protected void fetch_latest_sku()
        {

            HtmlDocument page = new HtmlWeb().Load("http://interconnection.org/nonprofit/example/amasty/");
            var pageLinks = page.DocumentNode.SelectNodes("//h1");
            foreach (var link in pageLinks)
            {
                string titleText = link.InnerText;

                if (titleText.Contains("ICRD"))
                {
                    oem_des = titleText;
                    oem_des_current = titleText;
                    ViewBag.oem_des_current = oem_des_current;
                    oem_des = oem_des.Replace("ICRD", "");
                    int temp = int.Parse(oem_des);
                    temp = temp + 1;
                    oem_des = temp.ToString();
                    oem_des = "ICRD" + oem_des;
                }
                else if (titleText.Contains("ICL"))
                {
                    mar_lap = titleText;
                    mar_lap_current = titleText;
                    ViewBag.mar_lap_current = mar_lap_current;
                    mar_lap = mar_lap.Replace("ICL", "");
                    int temp = int.Parse(mar_lap);
                    temp = temp + 1;
                    mar_lap = temp.ToString();
                    mar_lap = "ICL" + mar_lap;
                }
                else if (titleText.Contains("ICD"))
                {
                    mar_des = titleText;
                    mar_des_current = titleText;
                    ViewBag.mar_des_current = mar_des_current;
                    mar_des = mar_des.Replace("ICD", "");
                    int temp = int.Parse(mar_des);
                    temp = temp + 1;
                    mar_des = temp.ToString();
                    mar_des = "ICD" + mar_des;
                }
                else if (titleText.Contains("ICRL"))
                {
                    oem_lap = titleText;
                    oem_lap_current = titleText;
                    ViewBag.oem_lap_current = oem_lap_current;
                    oem_lap = oem_lap.Replace("ICRL", "");
                    int temp = int.Parse(oem_lap);
                    temp = temp + 1;
                    oem_lap = temp.ToString();
                    oem_lap = "ICRL" + oem_lap;
                }
                else if (titleText.Contains("ICMA"))
                {
                    apple_current = titleText;
                    ViewBag.apple_current = apple_current;
                }
                else
                {

                }
            }

        }
        public string sku_builder (string sku)
        {
            string new_sku = "";
            try
            {
                if (sku.Contains("MARDK"))
                {
                    new_sku = mar_des;
                }
                else if (sku.Contains("MARLP"))
                {
                    new_sku = mar_lap;
                }
                else if (sku.Contains("OEMDK"))
                {
                    new_sku = oem_des;
                }
                else if (sku.Contains("OEMLP"))
                {
                    new_sku = oem_lap;
                }
                else
                {
                    new_sku = "error";
                }
            }
           catch(Exception e )
            {
                ViewBag.error = e.Message.ToString();
            }
            return new_sku;
        }
        protected void pc_type_selection(string sku_family)
        {
            try
            {
                if (sku_family.Contains("DK"))
                {

                    magentoView.selectedId = 1;
                    magentoView.wireless_selectedId = 2;
                    magentoView.accessory_include = "Power Cord, Keyboard, Mouse";
                }
                else if (sku_family.Contains("LP"))
                {
                    magentoView.selectedId = 2;
                    magentoView.wireless_selectedId = 1;
                    magentoView.accessory_include = "AC Adapter";
                }
                else
                {
                    magentoView.selectedId = 3;
                    magentoView.wireless_selectedId = 1;
                    magentoView.accessory_include = "No Accessories include";
                }
            }

            catch (Exception e)
            {
                ViewBag.error = e.Message.ToString();
            }
        }
        private void software_selection(string sku_family)
        {
            try
            {
                //title
                magentoView.name = magentoView.brand + " " + magentoView.model + " (" + magentoView.cpu + ", " + magentoView.ram + " , " + magentoView.hdd + " )";


                magentoView.meta_description = "US based nonprofits and low-income individuals can get a great deal on a refurbished laptop right here on the InterConnection Online Store. This is a Windows 7 machine that has been tested and IC Certified by our in-house technicians.";
                if (sku_family.Contains("MAR"))
                {

                    magentoView.software_description = "<p>Windows 7 Professional, 64 bit</p><p>Microsoft Office 2010 Home and Business with Office, Excel, Word, Outlook and Power Point</p><p><strong>InterConnection exclusive:</strong>&nbsp;all of our computers come with a recovery partition, allowing users to restore the PCs operating system to like-new condition&nbsp;<em>without an installation disk.</em></p>";
                    magentoView.software = "Windows 7 Professional & Microsoft Office Home and Business 2010";
                    if (magentoView.selectedId == 1)
                    {
                        magentoView.short_description = "Get a great price on a great quality refurbished desktop right here at InterConnection. This desktop comes with a " + magentoView.cpu + " processor, " + magentoView.ram + " of Memory, " + magentoView.hdd + " Hard Drive, with Windows 7 Pro and Microsoft Office 2010 Home & Business.We back all of our products with a 1 year warranty.";
                    }
                    else if (magentoView.selectedId == 2)
                    {
                        magentoView.short_description = "Get a great price on a great quality refurbished laptop right here at InterConnection. This laptop comes with a " + magentoView.cpu + " processor, " + magentoView.ram + ", " + magentoView.hdd + ", with Windows 7 Pro and Microsoft Office 2010 Home & Business.We back all of our products with a 1 year warranty.";
                    }

                }
                else if (sku_family.Contains("OEM"))
                {

                    magentoView.software_description = "<p>Windows 7 Home Premium, 64 bit</p><p><strong>InterConnection exclusive:</strong>&nbsp;all of our computers come with a recovery partition, allowing users to restore the PCs operating system to like-new condition&nbsp;<em>without an installation disk.</em></p>";
                    magentoView.software = "Windows 7 Home Premium";
                    if (magentoView.selectedId == 1)
                    {
                        magentoView.short_description = "Get a great price on a great quality refurbished desktop right here at InterConnection. This desktop comes with an " + magentoView.cpu + " processor, " + magentoView.ram + ", " + magentoView.hdd + " , with Windows 7 Home Premium.  We back all of our products with a 1 year warranty.";
                    }
                    else if (magentoView.selectedId == 2)
                    {
                        if (magentoView.brand.Contains("HP"))
                        {
                            magentoView.short_description = "Get a great price on a great quality refurbished HP laptop right here at InterConnection. This HP laptop comes with an " + magentoView.cpu + " processor, " + magentoView.ram + " of Memory, " + magentoView.hdd + " Hard Drive, with Windows 7 Home Premium. We back all of our products with a 1 year warranty.";
                        }
                        else if (magentoView.brand.Contains("Lenovo"))
                        {
                            magentoView.short_description = "Get a great price on a great quality refurbished Lenovo laptop right here at InterConnection. This Lenovo laptop comes with an " + magentoView.cpu + " processor, " + magentoView.ram + " , " + magentoView.hdd + " , with Windows 7 Home Premium.  We back all of our products with a 1 year warranty.";
                        }
                        else
                        {
                            magentoView.short_description = "Get a great price on a great quality refurbished laptop right here at InterConnection. This laptop comes with an " + magentoView.cpu + " processor, " + magentoView.ram + " , " + magentoView.hdd + " , with Windows 7 Home Premium. We back all of our products with a 1 year warranty.";
                        }


                    }

                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message.ToString();
            }

     
        }

        [HttpPost]
        public ActionResult laptop_inv_update (string qty, string laptop_listing)
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123");
            string message = "";
            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


            qty_update.qty = qty;
  
            mservice.catalogInventoryStockItemUpdate(
mlogin, laptop_listing, qty_update);
            message = "Update Complete";
            return Json(new { result = message });
        }
        [HttpPost]
        public ActionResult desktop_inv_update(string qty, string desktop_listing)
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123");
            string message = "";
            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


            qty_update.qty = qty;

            mservice.catalogInventoryStockItemUpdate(
mlogin, desktop_listing, qty_update);
            message = "Update Complete";
            return Json(new { result = message });
        }
        [HttpPost]
        public ActionResult export(string qty, string price, string listingid, string tax_class,string des, string short_des, string meta_des, string soft_des,string software, string name, string create_date, string Wireless, string type, string include, string sku, string optical,string video, string serial, string screen,string model, string ram, string hdd, string cpu, string pallet, string brand,string asset, string grade,string computerType, string laptop_listing, string desktop_listing)
        {
            MagentoService mservice = new MagentoService();
           String mlogin = mservice.login("admin", "Interconnection123");

            //            string[] arr1 = new string[] { "1218" };

            //           // var item = mservice.catalogInventoryStockItemList(mlogin, arr1);
            

            //working up_sell function
            //            //catalogProductLinkEntity assign = new catalogProductLinkEntity();
            //            //assign.position = "1";


            //            //mservice.catalogProductLinkUpdate(mlogin, "related", "1030", "1029", assign, "product_id");

           string message = "";

    //        if(laptop_listing != "" || desktop_listing != "")
    //        {
    //            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


    //            qty_update.qty = qty;
    //            qty_update.is_in_stock = 1;
    //            qty_update.manage_stock = 1;

    //            mservice.catalogInventoryStockItemUpdate(
    //mlogin, sku, qty_update);
    //            message = "Update Complete";
    //            return Json(new { result = message });
    //        }
            try
            {
                catalogProductCreateEntity create = new catalogProductCreateEntity();
                var inv = new catalogProductTierPriceEntity();
                
                create.name = name;
                create.price = price;
                create.description = des;
                create.short_description = short_des;
                create.tax_class_id = tax_class;
                // create.meta_title = 
                create.visibility = "4";
                create.weight = "0";
                create.status = "2";
                create.meta_description = meta_des;
                inv.qty = int.Parse(qty);

                associativeEntity[] attributes = new associativeEntity[18];
                attributes[0] = new associativeEntity();
                attributes[0].key = "asset_tag";
                attributes[0].value = asset;

                attributes[1] = new associativeEntity();
                attributes[1].key = "sku_family";
                attributes[1].value = pallet;

                attributes[2] = new associativeEntity();
                attributes[2].key = "cpu";
                attributes[2].value = cpu;

                attributes[3] = new associativeEntity();
                attributes[3].key = "software_description";
                attributes[3].value = soft_des;

                attributes[4] = new associativeEntity();
                attributes[4].key = "ram";
                attributes[4].value = ram;

                attributes[5] = new associativeEntity();
                attributes[5].key = "hdd";
                attributes[5].value = hdd;

                attributes[6] = new associativeEntity();
                attributes[6].key = "os";
                attributes[6].value = software;

                attributes[7] = new associativeEntity();
                attributes[7].key = "creation_date";
                attributes[7].value = create_date;

                attributes[8] = new associativeEntity();
                attributes[8].key = "wireless";
                attributes[8].value = Wireless;

                attributes[9] = new associativeEntity();
                attributes[9].key = "incl";
                attributes[9].value = include;

                attributes[10] = new associativeEntity();
                attributes[10].key = "brand";
                attributes[10].value = brand;

                attributes[11] = new associativeEntity();
                attributes[11].key = "grade";
                attributes[11].value = grade;

                attributes[12] = new associativeEntity();
                attributes[12].key = "wcoa";
                attributes[12].value = "empty";

                attributes[13] = new associativeEntity();
                attributes[13].key = "ocoa";
                attributes[13].value = "empty";

                attributes[14] = new associativeEntity();
                attributes[14].key = "video";
                attributes[14].value = video;

                attributes[15] = new associativeEntity();
                attributes[15].key = "display";
                attributes[15].value = screen;

                attributes[16] = new associativeEntity();
                attributes[16].key = "computer";
                attributes[16].value = computerType;

                attributes[17] = new associativeEntity();
                attributes[17].key = "optical";
                attributes[17].value = optical;

                

                catalogProductAdditionalAttributesEntity additionalAttributes = new catalogProductAdditionalAttributesEntity();
                additionalAttributes.single_data = attributes;
                create.additional_attributes = additionalAttributes;

                mservice.catalogProductCreate(
    mlogin, "simple", "4", sku, create, "1");

                mservice.Dispose();

                //update inventory 
                catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();

               
                qty_update.qty = qty;
                qty_update.is_in_stock = 1;
                qty_update.manage_stock = 1;

                mservice.catalogInventoryStockItemUpdate(
    mlogin, sku, qty_update);

                message = "Listing Created";
            }
           catch ( Exception e) {
                message = "FAILED : " + e.Message.ToString();

            }
            

            return Json(new { result =  message});



        }
        
        public void get_catory_tree ()
        {

            string[] arr1 = new string[] {  };
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123");
            List<SelectListItem> cat_list = new List<SelectListItem>();
          var tree = mservice.catalogCategoryTree(mlogin, "2", "1");
            //var tree = mservice.catalogCategoryInfo(mlogin, 2, "1",arr1);

            foreach (var t in tree.children)
            {
                cat_list.Add(new SelectListItem() { Value = t.category_id.ToString(), Text = t.name.ToString() });
            }
            //int childen = tree.children.Count();


            //for (int i = 0; i < childen; i++)
            //{



            //}
            magentoView.cat = new SelectList(cat_list, "Value", "Text");
        }
        
        public List<Models.laptop> json_to_list(string url) {

            List<Models.laptop> laptop_inv = new List<Models.laptop>() ;
            var w = new WebClient();
            var json_data = w.DownloadString(url);
            if (!string.IsNullOrEmpty(json_data))
            {
                var result = JsonConvert.DeserializeObject<List<Models.laptop>>(json_data);
                // laptop_inv = result;
                laptop_inv = result;
            }

            return laptop_inv;
        }
        
        public string round_hdd(string hdd)
        {
            
            int temp_hdd = 0;
            string result = "";

            hdd = hdd.Replace("GB", "");
            temp_hdd = int.Parse(hdd);

            if (temp_hdd >= 0 && temp_hdd <= 60)
            {
                result = "60GB";
            }
            else if (temp_hdd >= 61 && temp_hdd <= 80)
            {
                result = "80GB";
            }
            else if (temp_hdd >= 81 && temp_hdd <= 120)
            {
                result = "120GB";
            }
            else if (temp_hdd >= 121 && temp_hdd <= 160)
            {
                result = "160GB";
            }
            else if (temp_hdd >= 161 && temp_hdd <= 250)
            {
                result = "250GB";
            }
            else if (temp_hdd >= 251 && temp_hdd <= 320)
            {
                result = "320GB";
            }
            else if (temp_hdd >= 321 && temp_hdd <= 500)
            {
                result = "500GB";
            }
            else if (temp_hdd >= 501 && temp_hdd <= 610)
            {
                result = "610GB";
            }
            else if (temp_hdd >= 611 && temp_hdd <= 750)
            {
                result = "750GB";
            }
            else if (temp_hdd >= 751 && temp_hdd <= 1000)
            {
               
                result = "1TB";
            }
            else if (temp_hdd >= 1001 && temp_hdd <= 1500)
            {
               
                result = "1.5TB";
            }
            else if (temp_hdd >= 1501 && temp_hdd <= 2000)
            {
                
                result = "2TB";
            }

            return result + " Hard Drive";
        }
        public string round_ram(string ram)
        {

           
            string result = "";

            ram = ram.Replace("MB", "");
             double temp_ram = double.Parse(ram);
            temp_ram = temp_ram / 1000;
            result = Math.Round(temp_ram).ToString() + "GB";

            return result + " RAM";
        }
        public void smart_inventory ()
        {

          //  magentoView.model
        }
        //post method to generate magento post
       
        [HttpPost]
        public ActionResult magento (string asset, int grade_selectedId,bool is_cat)
        {
           if(is_cat == true)
            {
                get_catory_tree();
            }
           else
            {
                List<SelectListItem> cat_list = new List<SelectListItem>();
                cat_list.Add(new SelectListItem() { Value = "1", Text = "Empty"});
                magentoView.cat = new SelectList(cat_list, "Value", "Text");

            }
            var url = "http://connectall.org/desktop.php";
            //for all enabled latop product
            var desktop = json_to_list(url);
             url = "http://connectall.org/get_enable.php";
            //for all enabled latop product
           var laptop = json_to_list(url);
            List<SelectListItem> desktop_listing = new List<SelectListItem>();
            foreach (var item in desktop)
            {
                desktop_listing.Add(new SelectListItem() { Value = item.entity_id, Text = item.name });
            }
            magentoView.desktop_listing = new SelectList(desktop_listing, "Value", "Text");
            List<SelectListItem> laptop_listing = new List<SelectListItem>();
            foreach (var item in laptop)
            {
                laptop_listing.Add(new SelectListItem() { Value = item.entity_id, Text = item.name });
            }
            magentoView.laptop_listing = new SelectList(laptop_listing, "Value", "Text");
           // get_catory_tree();
            fetch_latest_sku();
            List<SelectListItem> wireless_list = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1", Text="Yes"},
                new SelectListItem(){ Value="2", Text="No"},
               
            };
            magentoView.wireless = new SelectList(wireless_list, "Value", "Text");
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1", Text="Desktop"},
                new SelectListItem(){ Value="2", Text="Laptop"},
                new SelectListItem(){ Value="3", Text="MAC"},
            };
            magentoView.type = new SelectList(list, "Value", "Text");

            List<SelectListItem> taxable_list = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1", Text="Taxable Goods"},
                new SelectListItem(){ Value="2", Text="Shipping"},
                new SelectListItem(){ Value="3", Text="None"},
            };
            magentoView.tax_class = new SelectList(taxable_list, "Value", "Text");
          

            
            string result = "";
            int ictags = int.Parse(asset);
            var get_serial = ( from b in db.rediscovery where b.ictag == ictags select b);
            foreach (var item in get_serial)
            {
               
                result = item.serial;
            }
            

            //inner join linq to join both table's common coloum
            var spec = (from t in db.production_log where t.serial == result from h in db.rediscovery where t.serial == h.serial select new Models.magentoViewModel { hdd = t.HDD , cpu = t.CPU, brand = t.Manufacture, ram = t.RAM, ictags = ictags, model = t.Model,serial = h.serial, screen_size =t.screen_size , video_card = t.video_card, optical = h.optical_drive, pallet_name = h.pallet}).ToList();
            ViewBag.spec = spec;
            string temp_pallet = "";
            foreach (var item in spec)
            {
                temp_pallet = item.pallet_name;
                magentoView.brand = item.brand;
                magentoView.cpu = item.cpu;
                magentoView.hdd = item.hdd;
             //   string rounded_hdd = round_hdd(item.hdd);
                magentoView.ram = item.ram;
            //    string rounded_ram = round_ram(item.ram);
                magentoView.screen_size = item.screen_size;
               
            }
            
            



            string_rebuild new_string = new string_rebuild();
            
            onenote description = new onenote();
            price price = new price();
            //rebuild brand string 
            magentoView.brand = new_string.brand(magentoView.brand);
            magentoView.cpu = new_string.CPU(magentoView.cpu); magentoView.hdd =new_string.hdd(magentoView.hdd);
            magentoView.ram = new_string.ram(magentoView.ram);
            magentoView.creat_date = DateTime.Now.ToString();
            magentoView.sku = sku_builder(temp_pallet);
            
            if (magentoView.screen_size == "NA")
            {
                magentoView.screen_size = "0";
            }
             Double temp_size = double.Parse(magentoView.screen_size);
            int real_size = Convert.ToInt32(temp_size);
            magentoView.screen_size = real_size.ToString();
            
            pc_type_selection(temp_pallet);
            software_selection(temp_pallet);
            string grade = "";
           if (grade_selectedId == 1)
            {
                grade = "Grade A";
            }
           else
            {
                grade = "Grade B";
            }
            magentoView.description = description.grade_filter(temp_pallet,grade) + description.cpu_filter(magentoView.cpu) + description.hdd_filter(magentoView.hdd) + description.ram_filter(magentoView.ram) + description.ic_filter();

            magentoView.price = price.base_spec(temp_pallet, magentoView.ram, magentoView.hdd, magentoView.cpu, grade,magentoView.screen_size);
            return PartialView("_magentoTable", magentoView);
        }

        public JsonResult get_desktop_status(string id)
        {
            var url = "http://connectall.org/desktop.php";
            //for all enabled latop product
            var desktop = json_to_list(url);
            var detail = from t in desktop where t.entity_id == id select t;

            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getstatus(string id)
        {
            var url = "http://connectall.org/get_enable.php";
            //for all enabled latop product
            var laptop = json_to_list(url);
            var detail = from t in laptop where t.entity_id == id select t;
            
            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        //get method for magento page
        public ActionResult magento ()
        {
           // get_catory_tree();
            //for all product qty > 1
            //var url = "http://connectall.org/get_product.php";
            var url = "http://connectall.org/get_enable.php";
            //for all enabled latop product
            // ViewBag.laptop =  read_json(url);
           url = "http://connectall.org/desktop.php";
           // ViewBag.desktop = read_json(url);

            fetch_latest_sku();
            List<SelectListItem> grade_list = new List<SelectListItem>()
            {
                new SelectListItem(){ Value="1", Text="Grade A"},
                new SelectListItem(){ Value="2", Text="Grade B"},

            };
            magentoView.grade = new SelectList(grade_list, "Value", "Text");
            return View(magentoView);
        }

        // GET: production_log
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ChannelSortParm = String.IsNullOrEmpty(sortOrder) ? "channel_desc" : "";
            ViewBag.ModelSortParm = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var machine = (from t in db.production_log
                           where (t.time >= startDateTime && t.time <= endDateTime)
                           select t);
            if (!String.IsNullOrEmpty(searchString))
            {
                string temp_searchString = searchString;
                machine = (from t in db.production_log
                           where (t.ictags == temp_searchString)
                           select t);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    machine = machine.OrderByDescending(s => s.ictags);
                    break;
                case "channel_desc":
                    machine = machine.OrderByDescending(s => s.location);
                    break;
                case "model_desc":
                    machine = machine.OrderByDescending(s => s.Model);
                    break;
                case "Date":
                    machine = machine.OrderBy(s => s.time);
                    break;
                case "date_desc":
                    machine = machine.OrderByDescending(s => s.time);
                    break;
                default:  // Name ascending 
                    machine = machine.OrderBy(s => s.ictags);
                    break;
            }




            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(machine.ToPagedList(pageNumber, pageSize));
        }

        // GET: production_log/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            production_log production_log = db.production_log.Find(id);
            if (production_log == null)
            {
                return HttpNotFound();
            }
            return View(production_log);
        }

        // GET: production_log/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: production_log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "time,ictags,wcoa,ocoa,Manufacture,Model,CPU,RAM,HDD,serial,channel,pre_coa,location,video_card,screen_size")] production_log production_log)
        {
            if (ModelState.IsValid)
            {
                db.production_log.Add(production_log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(production_log);
        }

        // GET: production_log/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            production_log production_log = db.production_log.Find(id);
            if (production_log == null)
            {
                return HttpNotFound();
            }
            return View(production_log);
        }

        // POST: production_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "time,ictags,wcoa,ocoa,Manufacture,Model,CPU,RAM,HDD,serial,channel,pre_coa,location,video_card,screen_size")] production_log production_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(production_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(production_log);
        }

        // GET: production_log/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            production_log production_log = db.production_log.Find(id);
            if (production_log == null)
            {
                return HttpNotFound();
            }
            return View(production_log);
        }

        // POST: production_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            production_log production_log = db.production_log.Find(id);
            db.production_log.Remove(production_log);
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
