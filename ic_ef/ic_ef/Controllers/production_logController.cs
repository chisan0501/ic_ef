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
using System.Collections;
using System.Web.Script.Serialization;
using System.IO;
using ic_ef.org.interconnection.dev;

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

        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();
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

            HtmlDocument page = new HtmlWeb().Load("http://connectall.org/example/amasty/");
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

        public ActionResult bin_overview()
        {
            return View();

        }
        //get current inventory in bins 
        public JsonResult get_current_inv()
        {
            var result = (from t in db.production_log where t.status == null && t.bin_location != null orderby t.bin_location select t).ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        //get top 50 result from production_log and post back as json format 
        public JsonResult get_productionData ()
        {
           
            var result = (from t in db.production_log orderby t.time descending select t).Take(50).ToList();
            foreach ( var item in result)
            {
                item.location = item.time.ToString();
              
            }
            return Json(result ,JsonRequestBehavior.AllowGet);
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
        
       

//        [HttpPost]
//        public ActionResult laptop_inv_update (string qty, string laptop_listing)
//        {
//            MagentoService mservice = new MagentoService();
//            String mlogin = mservice.login("admin", "Interconnection123");
//            string message = "";
//            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


//            qty_update.qty = qty;
  
//            mservice.catalogInventoryStockItemUpdate(
//mlogin, laptop_listing, qty_update);
//            message = "Update Complete";
//            return Json(new { result = message });
//        }
//        [HttpPost]
//        public ActionResult desktop_inv_update(string qty, string desktop_listing)
//        {
//            MagentoService mservice = new MagentoService();
//            String mlogin = mservice.login("admin", "Interconnection123");
//            string message = "";
//            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


//            qty_update.qty = qty;

//            mservice.catalogInventoryStockItemUpdate(
//mlogin, desktop_listing, qty_update);
//            message = "Update Complete";
//            return Json(new { result = message });
//        }
//        [HttpPost]
//        public ActionResult export(string qty, string price, string listingid, string tax_class,string des, string short_des, string meta_des, string soft_des,string software, string name, string create_date, string Wireless, string type, string include, string sku, string optical,string video, string serial, string screen,string model, string ram, string hdd, string cpu, string pallet, string brand,string asset, string grade,string computerType, string laptop_listing, string desktop_listing)
//        {
//            MagentoService mservice = new MagentoService();
//           String mlogin = mservice.login("admin", "Interconnection123");

//            //            string[] arr1 = new string[] { "1218" };

//            //           // var item = mservice.catalogInventoryStockItemList(mlogin, arr1);
            

//            //working up_sell function
//            //            //catalogProductLinkEntity assign = new catalogProductLinkEntity();
//            //            //assign.position = "1";


//            //            //mservice.catalogProductLinkUpdate(mlogin, "related", "1030", "1029", assign, "product_id");

//           string message = "";

//    //        if(laptop_listing != "" || desktop_listing != "")
//    //        {
//    //            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


//    //            qty_update.qty = qty;
//    //            qty_update.is_in_stock = 1;
//    //            qty_update.manage_stock = 1;

//    //            mservice.catalogInventoryStockItemUpdate(
//    //mlogin, sku, qty_update);
//    //            message = "Update Complete";
//    //            return Json(new { result = message });
//    //        }
//            try
//            {
//                catalogProductCreateEntity create = new catalogProductCreateEntity();
//                var inv = new catalogProductTierPriceEntity();
                
//                create.name = name;
//                create.price = price;
//                create.description = des;
//                create.short_description = short_des;
//                create.tax_class_id = tax_class;
//                // create.meta_title = 
//                create.visibility = "4";
//                create.weight = "0";
//                create.status = "2";
//                create.meta_description = meta_des;
//                inv.qty = int.Parse(qty);

//                associativeEntity[] attributes = new associativeEntity[18];
//                attributes[0] = new associativeEntity();
//                attributes[0].key = "asset_tag";
//                attributes[0].value = asset;

//                attributes[1] = new associativeEntity();
//                attributes[1].key = "sku_family";
//                attributes[1].value = pallet;

//                attributes[2] = new associativeEntity();
//                attributes[2].key = "cpu";
//                attributes[2].value = cpu;

//                attributes[3] = new associativeEntity();
//                attributes[3].key = "software_description";
//                attributes[3].value = soft_des;

//                attributes[4] = new associativeEntity();
//                attributes[4].key = "ram";
//                attributes[4].value = ram;

//                attributes[5] = new associativeEntity();
//                attributes[5].key = "hdd";
//                attributes[5].value = hdd;

//                attributes[6] = new associativeEntity();
//                attributes[6].key = "os";
//                attributes[6].value = software;

//                attributes[7] = new associativeEntity();
//                attributes[7].key = "creation_date";
//                attributes[7].value = create_date;

//                attributes[8] = new associativeEntity();
//                attributes[8].key = "wireless";
//                attributes[8].value = Wireless;

//                attributes[9] = new associativeEntity();
//                attributes[9].key = "incl";
//                attributes[9].value = include;

//                attributes[10] = new associativeEntity();
//                attributes[10].key = "brand";
//                attributes[10].value = brand;

//                attributes[11] = new associativeEntity();
//                attributes[11].key = "grade";
//                attributes[11].value = grade;

//                attributes[12] = new associativeEntity();
//                attributes[12].key = "wcoa";
//                attributes[12].value = "empty";

//                attributes[13] = new associativeEntity();
//                attributes[13].key = "ocoa";
//                attributes[13].value = "empty";

//                attributes[14] = new associativeEntity();
//                attributes[14].key = "video";
//                attributes[14].value = video;

//                attributes[15] = new associativeEntity();
//                attributes[15].key = "display";
//                attributes[15].value = screen;

//                attributes[16] = new associativeEntity();
//                attributes[16].key = "computer";
//                attributes[16].value = computerType;

//                attributes[17] = new associativeEntity();
//                attributes[17].key = "optical";
//                attributes[17].value = optical;

                

//                catalogProductAdditionalAttributesEntity additionalAttributes = new catalogProductAdditionalAttributesEntity();
//                additionalAttributes.single_data = attributes;
//                create.additional_attributes = additionalAttributes;

//                mservice.catalogProductCreate(
//    mlogin, "simple", "4", sku, create, "1");

//                mservice.Dispose();

//                //update inventory 
//                catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();

               
//                qty_update.qty = qty;
//                qty_update.is_in_stock = 1;
//                qty_update.manage_stock = 1;

//                mservice.catalogInventoryStockItemUpdate(
//    mlogin, sku, qty_update);

//                message = "Listing Created";
//            }
//           catch ( Exception e) {
//                message = "FAILED : " + e.Message.ToString();

//            }
            

//            return Json(new { result =  message});



//        }
        
        /// <summary>
        /// 
        /// </summary>
        //public void get_catory_tree ()
        //{
           
        //    string[] arr1 = new string[] {  };
        //    MagentoService mservice = new MagentoService();
        //    String mlogin = mservice.login("admin", "Interconnection123");
        //    List<SelectListItem> cat_list = new List<SelectListItem>();
        //  var tree = mservice.catalogCategoryTree(mlogin, "2", "1");
        //    //var tree = mservice.catalogCategoryInfo(mlogin, 2, "1",arr1);

        //    foreach (var t in tree.children)
        //    {
        //        cat_list.Add(new SelectListItem() { Value = t.category_id.ToString(), Text = t.name.ToString() });
        //    }
        //    //int childen = tree.children.Count();


        //    //for (int i = 0; i < childen; i++)
        //    //{



        //    //}
        //    magentoView.cat = new SelectList(cat_list, "Value", "Text");
        //}


        public ActionResult qc()
        {
            return View();
        }
        public ActionResult magento_retail_view ()
        {
           

            return View();
        }
        public ActionResult magento_retail ()
        {
            return View();
        }
        public Models.sku_model latest (string url)
        {
            Models.sku_model sku = new Models.sku_model();
            var w = new WebClient();
            var json_data = w.DownloadString(url);
            if (!string.IsNullOrEmpty(json_data))
            {

                JavaScriptSerializer oJS = new JavaScriptSerializer();
                Models.latestsku.RootObject oRootObject = new Models.latestsku.RootObject();
                oRootObject = oJS.Deserialize<Models.latestsku.RootObject>(json_data);
                sku.ICD_sku = oRootObject.ICD[0].sku ;
                 sku.ICL_sku = oRootObject.ICL[0].sku;
                sku.ICMA_sku = oRootObject.ICMA[0].sku;
                sku.ICRD_sku = oRootObject.ICRD[0].sku ;
                 sku.ICRL_sku = oRootObject.ICRL[0].sku;
                // laptop_inv = result;

            }
            return sku;
        }

        public List<Models.laptop> json_to_list(string url) {

            List<Models.laptop> laptop_inv = new List<Models.laptop>() ;
            try
            {
                var w = new WebClient();
                var json_data = w.DownloadString(url);
                if (!string.IsNullOrEmpty(json_data))
                {
                    var result = JsonConvert.DeserializeObject<List<Models.laptop>>(json_data);
                    // laptop_inv = result;
                    laptop_inv = result;
                }

            }
            catch (Exception) {
                laptop_inv = null;
                }
            return laptop_inv;
        }

        public List<Models.csv_import> json_to_customer(string url)
        {

            List<Models.csv_import> laptop_inv = new List<Models.csv_import>();
            var w = new WebClient();
            var json_data = w.DownloadString(url);
            if (!string.IsNullOrEmpty(json_data))
            {
                var result = JsonConvert.DeserializeObject<List<Models.csv_import>>(json_data);
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

        public JsonResult qc_update (string asset_tag)
        {

            try
            {
                mage mage = new mage();
                var sku = (from t in db.production_log where t.ictags == asset_tag select t.channel).FirstOrDefault();

                if (string.IsNullOrEmpty(sku))
                {
                    int temp_asset = int.Parse(asset_tag);
                    var serial = (from t in db.rediscovery where t.ictag == temp_asset orderby t.time descending select t.serial).FirstOrDefault();
                    sku = (from t in db.production_log where t.serial == serial select t.channel).FirstOrDefault();
                }



                var product = mage.check_product(sku);
                var pid = product[0].product_id;
                var pqty = product[0].qty;
                double temp_qty = Double.Parse(pqty);
                temp_qty -= 1;
                pqty = temp_qty.ToString();
                smart_inventory(pid, pqty);

                //update qty for classic magento inventory entity

                MagentoService mservice = new MagentoService();
                String mlogin = mservice.login("admin", "Interconnection123!");
                catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();

                qty_update.manage_stockSpecified = true;


                qty_update.qty = pqty;

                mservice.catalogInventoryStockItemUpdate(
     mlogin, sku, qty_update);

                return Json(new { success = true, message = "<p style='color:green'>Qty Update Sucessfully</p>", status = "<p style='color:green'>SKU: " + sku +  " QTY in stock: " + pqty +"</p>"}, JsonRequestBehavior.AllowGet);

            }
           catch(Exception e)
            {
                return Json(new { success = false, message = "<p style='color:red'>FAILED : " + e.Message +"</p>"}, JsonRequestBehavior.AllowGet);
            }
        }

        public void smart_inventory (string p_id, string qty)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://dev.interconnection.org/update.php?product_id=" +p_id + "&qty=" + qty);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.UserAgent = ".NET Framework Test Client";
                WebResponse wr = myHttpWebRequest.GetResponse();
            }
            catch (WebException wex)
            {
                var pageContent = new StreamReader(wex.Response.GetResponseStream())
                                      .ReadToEnd();
            }
            //  magentoView.model
        }
        //post method to generate magento post
       
        
            public JsonResult channel_list ()
        {
            mage mage = new mage();
            var result = mage.pallet_list();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_bin_location(string id)
        {
            var result = (from t in db.production_log where t.bin_location == id && t.status != "pulled" select t.ictags).ToList();
            return Json(new { result = result},JsonRequestBehavior.AllowGet);
        }
        //reset the location of the asset tag
        public JsonResult reset_location (string asset)
        {
            string message = "";
            var record = (from t in db.production_log where t.ictags == asset select t.wcoa).FirstOrDefault();
            try
            {
                var original = db.production_log.Find(record.ToString());
                if (original != null)
                {
                    original.bin_location = null;
                    db.SaveChanges();
                    db.Dispose();
                    message = "<p style='color:green'>Asset " + asset + " is now reset to null</p>";
                }
               
            }
            catch
            {
                message = "<p style='color:red'>Asset " + asset + " Fail to reset</p>";
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult asso (string asset, string serial)
        {
            string message = "";
            var record = (from t in db.production_log where t.serial == serial select t.wcoa).FirstOrDefault();
            try
            {
                var original = db.production_log.Find(record.ToString());
                if (original != null)
                {
                    original.ictags = asset;
                    db.SaveChanges();
                    db.Dispose();
                    message = "<p style='color:green'>Asset " + asset + " is now associate with " + serial + "</p>";
                }
            }


            catch
            {
                message = "<p style='color:red'>Cant Find Asset " + asset + " please Contact your Supervisor" + "</p>";
            }
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult write_bin_location (string asset,string id)

        {
            string message = "";
            if (!String.IsNullOrEmpty(asset))
            {
                var record = (from t in db.production_log where t.ictags == asset select t.wcoa).FirstOrDefault();
                try
                {
                    var original = db.production_log.Find(record.ToString());
                    if (original != null)
                    {
                        original.bin_location = id;
                        db.SaveChanges();
                        db.Dispose();
                        message = "<p style='color:green'>Asset " + asset + " is now assigned to Bin " + id + "</p>";
                    }
                }
                
               
                catch
                {
                     message = "<p style='color:red'>Cant Find Asset " + asset + " Please check Data Inputed or try to Associate Serial # with Asset tag Below. If Problem Still Occur, please Contact your Supervisor" + "</p>";
                }
            }
            
          
            
           
            return Json(new { message = message}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult bin_location(string id)
        {

            ViewData["id"] = id;
            return View();
        }

        [HttpGet]
        public ActionResult get_list(string id)
        {
            var list_item = (from t in db.picklist where t.id == id select t).ToList();
            ViewBag.item = MvcHtmlString.Create(list_item[0].item);
            return View();
        }
    
        public JsonResult write_list (string id, string name, string item, DateTime date)
        {
          // item =  System.Web.HttpUtility.HtmlEncode(item);
            var write_table = new picklist();
            write_table.id = id;
            write_table.name = name ;
            write_table.item = item;
            write_table.date = date;
            db.picklist.Add(write_table);
            db.SaveChanges();
            db.Dispose();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Upload()
        {
           
            try
            {
               
                    var file = Request.Files[0];

                    var fileName = Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                
                return Json(new { success = true, path = path });
            }
          catch
            {
                return Json(new { success = false });
            }
        }


        //import sku for retail POS
        //update QTY in main website 
        public JsonResult retail_quick_import(string price, string name, string sku, string weight, string desc, string short_desc, string qty, string[] websites, string stock, string status, string visible, string attr, string type, string tax, string img_path)
        {
            string message = "";
            //check to see if inventory has that item before adding it to the list
            var quick_check = (from t in db.production_log where t.channel == sku && t.bin_location != null select t).ToList();

            if(quick_check.Count == 0)
            {
                message = "<h3 style='color:red'>" + sku + " is not yet avaiable in Inventory, please contact administrator for more information</h3>";
                return Json(new { message = message }, JsonRequestBehavior.AllowGet);
            }


           
           
               
            Models.retail_quick_import retail_model = new Models.retail_quick_import();
            retail_model.price = price;
            retail_model.name = name;
            retail_model.sku = sku;
            string original_sku = retail_model.sku;
            retail_model.weight = weight;
            retail_model.desc = desc;
            retail_model.short_desc = short_desc;
            retail_model.qty = qty;
            retail_model.webistes = websites;
            retail_model.stock = stock;
            retail_model.status = status;
            retail_model.visible = visible;
            retail_model.attr = attr;
            retail_model.type = type;
            retail_model.tax_id = tax;

            //format a sku just for retail
            string to_be_import_sku = retail_model.sku + "_retail";

            mage mage = new mage();
            string path = img_path;


            //create sku if product is not exisited
            var original_product = mage.check_product(retail_model.sku);
            var added_retail_product = mage.check_product(to_be_import_sku);
            if (added_retail_product.Length == 0)
            {
                //if product not exisit create a new listing and set qty to 0
                retail_model.sku = to_be_import_sku;
                mage.retail_quick_import(retail_model);
                added_retail_product = mage.check_product(retail_model.sku);
                //get the product id for the new retail listing 
                retail_model.p_id = added_retail_product[0].product_id;
                //set the QTY to 1
                retail_model.qty = "1";
                double temp_qty = double.Parse(retail_model.qty);
                
                retail_model.qty = temp_qty.ToString();
                //update qty for inventory module
                smart_inventory(retail_model.p_id, retail_model.qty);

                //update qty for classic magento inventory entity
                mage.quick_update(retail_model.qty, retail_model.p_id,path);
            }
            else
            {
                //if sku is exisited
                //just update the qty
                double temp_added_retail_qty = double.Parse(added_retail_product[0].qty);
                temp_added_retail_qty += 1;
                added_retail_product[0].qty = temp_added_retail_qty.ToString();


                //update qty for inventory module
                smart_inventory(added_retail_product[0].product_id, added_retail_product[0].qty);

                //update qty for classic magento inventory entity
                mage.quick_update(added_retail_product[0].qty, added_retail_product[0].product_id,path);
            }


                //qty -1 on the original sku
                double update_qty = double.Parse(original_product[0].qty);
                update_qty -= 1;
            //update inventroy module
            smart_inventory(original_product[0].product_id, update_qty.ToString());
                //update the core magento table
                mage.quick_update(update_qty.ToString(), original_product[0].product_id, path);

            //assign one asset to user
            var location = (from t in db.production_log where t.channel == original_sku && t.status !="pulled" && t.bin_location != null  select t).FirstOrDefault();
            //update the asset to sold
            if (location != null)
            {
                var original = db.production_log.Find(location.wcoa);
                if (original != null)
                {
                    original.status = "pulled";
                    db.SaveChanges();
                    db.Dispose();
                    message = "<h3 style='color:green'>Product Imported Successfully</h3>";
                }
            }
            else { message = "<h3 style='color:red'>Magento Inventory and Warehouse Inventory does not match, Contact your Supervisor</h3>"; }



            
            
            //catch (Exception e)
            //{
            //    message = "<h3 style='color:red'>Product Imported Fail </p><p style='red'>" + e.Message + "</h3>";
            //}

            return Json(new { message = message , location = location.bin_location,asset = location.ictags }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult online_store_picklist(string sku)
        {
         
            string result = "";
            var location = (from t in db.production_log where t.channel == sku && t.status != "pulled" && t.bin_location != null select t).FirstOrDefault();
            //update the asset to sold
            if (location != null)
            {
                var original = db.production_log.Find(location.wcoa);
                if (original != null)
                {
                    original.status = "pulled";
                    db.SaveChanges();
                    db.Dispose();
                    result = "<div class='ui positive icon message'><i class='inbox icon'></i><div class='content'><div class='header'>Asset :" + location.ictags+"</div><p>Location : "+location.bin_location+"</p></div></div>";
                    
                }
            }
            else {
                result = "<div class='ui negative message'><i class='inbox icon'></i><div class='content'><div class='header'><p style='color:red'>Magento Inventory and Warehouse Inventory does not match, Contact your Supervisor</p></div></div></div>";
                    }

            return Json(new {result = result} ,JsonRequestBehavior.AllowGet);
        }


        public ActionResult asset_locator ()
        {

            return View();
        }

        public JsonResult quick_import (string price, string name, string sku, string weight, string desc, string short_desc,string qty, string[] websites, string stock, string status, string visible, string attr, string type, string tax, string img_path)
        {
            string message = "";
            Models.retail_quick_import retail_model = new Models.retail_quick_import();
            retail_model.price = price;
            retail_model.name = name;
            retail_model.sku = sku;
            retail_model.weight = weight;
            retail_model.desc = desc;
            retail_model.short_desc = short_desc;
            retail_model.qty = qty;
            retail_model.webistes = websites;
            retail_model.stock = stock;
            retail_model.status = status;
            retail_model.visible = visible;
            retail_model.attr = attr;
            retail_model.type = type;
            retail_model.tax_id = tax;
            
            mage mage = new mage();
            string path = img_path;

            try
            {
                //add a SKU checking function   
                //if sku already exisit jump to quick_update
                var product = mage.check_product(retail_model.sku);

                if (product.Length == 0)
                {
                    //if product not exisit 
                    //set qty to 0;
                    mage.retail_quick_import(retail_model);
                    product = mage.check_product(retail_model.sku);
                    retail_model.p_id = product[0].product_id;
                    retail_model.qty = "0";
                }
                else
                {
                    //if product exisit
                    //use exisiting qty
                    retail_model.p_id = product[0].product_id;
                    retail_model.qty = product[0].qty;
                }
                //convert qty from string to dobule and add 1
                double temp_qty = double.Parse(retail_model.qty);
                //temp_qty += 1;
                retail_model.qty = temp_qty.ToString();

                //update qty for inventory module
                smart_inventory(retail_model.p_id, retail_model.qty);

                //update qty for classic magento inventory entity
                mage.quick_update(retail_model.qty,retail_model.sku, path);

                //update shelf qty 
                //obtain old sku data
                var old_product = mage.check_product(sku);
                double temp_old_qty = double.Parse(old_product[0].qty);
                temp_old_qty -= 1;
                old_product[0].qty = temp_old_qty.ToString();
                retail_model.qty = old_product[0].qty;
                retail_model.sku = old_product[0].sku;

                smart_inventory(retail_model.sku, retail_model.qty);
                mage.quick_update(retail_model.qty, retail_model.sku, path);


                message = "<h3 style='color:green'>Product Imported Successfully</h3>";
            }
           catch(Exception e)
            {
                message = "<h3 style='color:red'>Product Imported Fail </p><p style='red'>"+e.Message+"</h3>";
            }

            return Json(new {message= message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost] 
        public JsonResult hardware_spec (string asset)
        {

            try
            {
                int int_asset = int.Parse(asset);
                mage mage = new mage();
                var result = mage.get_spec(int_asset);
                result[0].ram = Levenshtein.ram_format(result[0].ram, false);
              
                result[0].brand = Levenshtein.compute_difference(result[0].brand,Levenshtein.brand_name());
                if(result[0].brand == "Hewlett Packard")
                {
                    result[0].brand = "HP";
                }
                if (result.Count == 0)
                {
                   var results = mage.rediscovery(int_asset);
                    return Json(results, JsonRequestBehavior.AllowGet);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
            catch
            {
                return Json(new { success = false, responseText = "There seem to be an error" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        //public ActionResult magento (string asset, int grade_selectedId,bool is_cat)
        //{
        //   int  int_asset = int.Parse(asset);
        //    mage mage = new mage();
        //    ViewBag.spec = mage.get_spec(int_asset);
            


        //    if (is_cat == true)
        //    {
        //        get_catory_tree();
        //    }
        //   else
        //    {
        //        List<SelectListItem> cat_list = new List<SelectListItem>();
        //        cat_list.Add(new SelectListItem() { Value = "1", Text = "Empty"});
        //        magentoView.cat = new SelectList(cat_list, "Value", "Text");

        //    }
        //    var url = "http://connectall.org/desktop.php";
        //    //for all enabled latop product
        //    var desktop = json_to_list(url);
        //     url = "http://connectall.org/get_enable.php";
        //    //for all enabled latop product
        //   var laptop = json_to_list(url);
        //    List<SelectListItem> desktop_listing = new List<SelectListItem>();
        //    foreach (var item in desktop)
        //    {
        //        desktop_listing.Add(new SelectListItem() { Value = item.entity_id, Text = item.name });
        //    }
        //    magentoView.desktop_listing = new SelectList(desktop_listing, "Value", "Text");
        //    List<SelectListItem> laptop_listing = new List<SelectListItem>();
        //    foreach (var item in laptop)
        //    {
        //        laptop_listing.Add(new SelectListItem() { Value = item.entity_id, Text = item.name });
        //    }
        //    magentoView.laptop_listing = new SelectList(laptop_listing, "Value", "Text");
        //   // get_catory_tree();
        // //   fetch_latest_sku();
        //    List<SelectListItem> wireless_list = new List<SelectListItem>()
        //    {
        //        new SelectListItem(){ Value="1", Text="Yes"},
        //        new SelectListItem(){ Value="2", Text="No"},
               
        //    };
        //    magentoView.wireless = new SelectList(wireless_list, "Value", "Text");
        //    List<SelectListItem> list = new List<SelectListItem>()
        //    {
        //        new SelectListItem(){ Value="1", Text="Desktop"},
        //        new SelectListItem(){ Value="2", Text="Laptop"},
        //        new SelectListItem(){ Value="3", Text="MAC"},
        //    };
        //    magentoView.type = new SelectList(list, "Value", "Text");

        //    List<SelectListItem> taxable_list = new List<SelectListItem>()
        //    {
        //        new SelectListItem(){ Value="1", Text="Taxable Goods"},
        //        new SelectListItem(){ Value="2", Text="Shipping"},
        //        new SelectListItem(){ Value="3", Text="None"},
        //    };
        //    magentoView.tax_class = new SelectList(taxable_list, "Value", "Text");
          

            
        //    string result = "";
        //    int ictags = int.Parse(asset);
        //    var get_serial = ( from b in db.rediscovery where b.ictag == ictags select b);
        //    foreach (var item in get_serial)
        //    {
               
        //        result = item.serial;
        //    }
            

        //    //inner join linq to join both table's common coloum
        //    var spec = (from t in db.production_log where t.serial == result from h in db.rediscovery where t.serial == h.serial select new Models.magentoViewModel { hdd = t.HDD , cpu = t.CPU, brand = t.Manufacture, ram = t.RAM, ictags = ictags, model = t.Model,serial = h.serial, screen_size =t.screen_size , video_card = t.video_card, optical = h.optical_drive, pallet_name = h.pallet}).ToList();
        //    ViewBag.spec = spec;
        //    string temp_pallet = "";
        //    foreach (var item in spec)
        //    {
        //        temp_pallet = item.pallet_name;
        //        magentoView.brand = item.brand;
        //        magentoView.cpu = item.cpu;
        //        magentoView.hdd = item.hdd;
        //     //   string rounded_hdd = round_hdd(item.hdd);
        //        magentoView.ram = item.ram;
        //    //    string rounded_ram = round_ram(item.ram);
        //        magentoView.screen_size = item.screen_size;
               
        //    }
            
            



        //    string_rebuild new_string = new string_rebuild();
            
        //    onenote description = new onenote();
        //    price price = new price();
        //    //rebuild brand string 
        //    magentoView.brand = new_string.brand(magentoView.brand);
        //    magentoView.cpu = new_string.CPU(magentoView.cpu); magentoView.hdd =new_string.hdd(magentoView.hdd);
        //    magentoView.ram = new_string.ram(magentoView.ram);
        //    magentoView.creat_date = DateTime.Now.ToString();
        //    magentoView.sku = sku_builder(temp_pallet);
            
        //    if (magentoView.screen_size == "NA")
        //    {
        //        magentoView.screen_size = "0";
        //    }
        //     Double temp_size = double.Parse(magentoView.screen_size);
        //    int real_size = Convert.ToInt32(temp_size);
        //    magentoView.screen_size = real_size.ToString();
            
            
          
        //    return PartialView("_magentoTable", magentoView);
        //}


        public JsonResult get_sku_family_table (string sku_family , DateTime target)
        {
            DateTime startDatetime = target.AddDays(-3).AddTicks(-1);
            DateTime endDateTime = target.AddDays(3).AddTicks(-1);

            var result = (from t in db.rediscovery where t.pallet == sku_family && t.status != "shipped" && (t.time >= startDatetime && t.time <= endDateTime) select t).ToList();
          
            return Json(result , JsonRequestBehavior.AllowGet);
        }

        public ActionResult magento_main ()
        {
            return View();

        }
        public JsonResult retail_inv ()
        {
            var url = "http://dev.interconnection.org/get_product.php";
            var retail_inv = json_to_list(url);
            return Json(retail_inv, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_latest_sku (string sku_family)
        {
            var url = "http://connectall.org/json_php/get_sku.php";
            var sku = latest(url);

            return Json(sku, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_status(string sku_family)
        {
            var url = "http://connectall.org/desktop.php";   
            var desktop = json_to_list(url);
            var detail = from t in desktop where t.sku_family == sku_family select t;
            if(detail.Count() == 0)
            {
                url = "http://connectall.org/laptop.php";
                //for all enabled latop product
                var laptop = json_to_list(url);
                 detail = from t in laptop where t.sku_family == sku_family select t;
            }


            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult magento_create()
        {
           
            return View();
        }


        //public JsonResult qty(string sku,string qty)
        //{
        //    if (string.IsNullOrEmpty(qty))
        //    {
        //        qty = "0";
        //    }
        //    double qty32 = double.Parse(qty);
        //    qty32 += 1.00;
        //    qty = qty32.ToString();
        //    MagentoService mservice = new MagentoService();
        //    String mlogin = mservice.login("admin", "Interconnection123!");




        //    string product = sku;
        //    catalogInventoryStockItemUpdateEntity update_item = new catalogInventoryStockItemUpdateEntity();
        //    update_item.qty = qty;
        //    update_item.is_in_stock = 1;
        //    var update = mservice.catalogInventoryStockItemUpdate(mlogin, product, update_item);

        //    return Json(new { success = true, responseText = "Qty Updated" }, JsonRequestBehavior.AllowGet);
        //}
     

        [HttpPost]
        public JsonResult order_json_ts (Models.ts_order[] tsorder)
        {
            foreach (var item in tsorder)
            {
                //split full name into first and last
                var names = item.Org_Contact_Name.Split(' ');
                item.first_name = names[0];
                item.last_name = names[1];
                //remove the \n stirng from address 
                item.Org_Street_Address = item.Org_Street_Address.Replace("\n", "");
            }
            mage mage = new mage();
           string result =  mage.create_order_ts(tsorder);
            return Json(new { scuess = true , message = result}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult order_json_g360(Models.g360_order[] g360order)
        {
            foreach (var item in g360order)
            {
                //split full name into first and last
                
                //remove the \n stirng from address 
                
            }
            mage mage = new mage();
            string result = mage.create_order_g360(g360order);
            return Json(new { scuess = true, message = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult mage_order_import ()
        {
           string url = "http://www.dev.interconnection.org/qbjson.json";
         //   var retail_inv = json_to_customer(url);
            mage mage = new mage();
           // mage.create_customer(retail_inv);
            // mage mage = new mage();
            //mage.create_order();
            return View();
        }

        public JsonResult detail_status(string hdd, string ram, string cpu, string sku_family)
        {
            List<Models.laptop> result = new List<Models.laptop>();
            if (sku_family.Contains("DK"))
            {
                var url = "http://connectall.org/desktop.php";
                 result = json_to_list(url);
              
            }
            else
            {
                var url = "http://connectall.org/laptop.php";
                 result = json_to_list(url);
            }

            for (int i = 0; i < result.Count(); i++)
            {
                string temp_cpu = result[i].cpu;
                string formatted_cpu = Levenshtein.comput_title(temp_cpu);
                result[i].cpu = formatted_cpu;
            }
            for (int i = 0; i < result.Count(); i++)
            {
                string temp_hdd = result[i].hdd;
                
                result[i].hdd = Levenshtein.hdd_format(temp_hdd,true);
            }
            for (int i = 0; i < result.Count(); i++)
            {
                string temp_ram = result[i].ram;
                result[i].ram = Levenshtein.ram_format(temp_ram, true);
            }
                //for all enabled latop product
                cpu = Levenshtein.comput_title(cpu);
            hdd = Levenshtein.hdd_format(hdd,false);
            ram = Levenshtein.ram_format(ram, false);
            var detail = from t in result where t.cpu == cpu && t.hdd == hdd && t.ram == ram select t;

            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        //get method for magento page
        public ActionResult magento ()
        {
           // get_catory_tree();
            //for all product qty > 1
            //var url = "http://connectall.org/get_product.php";
            //var url = "http://connectall.org/get_enable.php";
            //for all enabled latop product
            // ViewBag.laptop =  read_json(url);
          var url = "http://connectall.org/desktop.php";
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
