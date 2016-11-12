﻿using System;
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
using ic_ef.org.connectall;
using System.Net.Http;
using ic_ef.Models;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Magento.RestApi;

namespace ic_ef.Controllers
{


    public class production_logController : AsyncController
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


        

        public JsonResult get_production_data(string asset)
        {
            int int_asset = int.Parse(asset);
            var result = (from t in db.production_log where t.ictags == asset select t).FirstOrDefault();
            var discovery = (from t in db.discovery where t.ictag == int_asset select t).FirstOrDefault();

            return Json(new { production = result, discovery = discovery},JsonRequestBehavior.AllowGet);
        }

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

        //function to return asset to stock
        public JsonResult return_stock_json (string asset, string sku,string source, string bin)
        {
            
            string message = "";
            string to_be_import_sku = sku + "_retail";
            //update both the bin location and status to null 
            var entry = new production_log();
            using (var ctx = new db_a094d4_icdbEntities())
            {
                entry = ctx.production_log.Where(s => s.ictags == asset).FirstOrDefault<production_log>();
                if (entry != null)
                {
                    entry.status = null;
                    entry.bin_location = bin;
                }
            }
            using (var dbCtx = new db_a094d4_icdbEntities())
            {
                //3. Mark entity as modified
                dbCtx.Entry(entry).State = System.Data.Entity.EntityState.Modified;

                //4. call SaveChanges
                dbCtx.SaveChanges();
            }

            //now deduct retail sku qty from magento 
            mage mage = new mage();
            double qty = 0.00;

            if (source == "retail")
            {
                var retail_product = mage.check_product(to_be_import_sku).FirstOrDefault();
                 qty = double.Parse(retail_product.qty);
                qty -= 1;

                smart_inventory(retail_product.product_id, qty.ToString());
                mage.quick_update(qty.ToString(), to_be_import_sku, null);

                //now increment qty of stock SKU

                
            }

            var stock_product = mage.check_product(sku).FirstOrDefault();

            qty = double.Parse(stock_product.qty);
            qty += 1;

            smart_inventory(stock_product.product_id, qty.ToString());
            mage.quick_update(qty.ToString(), sku, null);




            message = "<h3 style='color:green'>Completed</h3>";

            return Json(new { message = message }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult get_bin_report()
        {
            var total = (from p in db.production_log where p.bin_location != null && p.status != "pulled" group p by p.bin_location into g select new { ictag = g.Key, pc = g.ToList() }).ToList();


            return Json(total,JsonRequestBehavior.AllowGet);
        }

       public JsonResult get_inventrory_report ()
        {
            int mar_i5_LP = 0;
            int mar_i3_LP = 0;
            int mar_i7_LP = 0;
            int mar_c2d_LP = 0;
            int mar_amd_LP = 0;
            int oem_i5_LP = 0;
            int oem_i3_LP = 0;
            int oem_i7_LP = 0;
            int oem_c2d_LP = 0;
            int oem_amd_LP = 0;
            int mar_i5_DK = 0;
            int mar_i3_DK = 0;
            int mar_i7_DK = 0;
            int mar_c2d_DK = 0;
            int mar_amd_DK = 0;
            int oem_i5_DK = 0;
            int oem_i3_DK = 0;
            int oem_i7_DK = 0;
            int oem_c2d_DK = 0;
            int oem_amd_DK = 0;
            


            var CPU = (from c in db.production_log where c.bin_location != null && c.status != "pulled" select c.channel).ToList();

            


            foreach ( var item in CPU)
            {
                if (item.Contains("i3") && item.Contains("OEM") && item.Contains("LP")){
                    oem_i3_LP++;
                }
                if (item.Contains("i5") && item.Contains("OEM") && item.Contains("LP"))
                {
                    oem_i5_LP++;
                }
                if (item.Contains("i7") && item.Contains("OEM") && item.Contains("LP"))
                {
                    oem_i7_LP++;
                }
                if (item.Contains("i3") && item.Contains("OEM") && item.Contains("DK"))
                {
                    oem_i3_DK++;
                }
                if (item.Contains("i5") && item.Contains("OEM") && item.Contains("DK"))
                {
                    oem_i5_DK++;
                }
                if (item.Contains("i7") && item.Contains("OEM") && item.Contains("DK"))
                {
                    oem_i7_DK++;
                }
                if (item.Contains("i3") && !item.Contains("OEM") && item.Contains("LP"))
                {
                    mar_i3_LP++;
                }
                if (item.Contains("i5") && !item.Contains("OEM") && item.Contains("LP"))
                {
                    mar_i5_LP++;
                }
                if (item.Contains("i7") && !item.Contains("OEM") && item.Contains("LP"))
                {
                    mar_i7_LP++;
                }
                if (item.Contains("i3") && !item.Contains("OEM") && item.Contains("DK"))
                {
                    mar_i3_DK++;
                }
                if (item.Contains("i5") && !item.Contains("OEM") && item.Contains("DK"))
                {
                    mar_i5_DK++;
                }
                if (item.Contains("i7") && !item.Contains("OEM") && item.Contains("DK"))
                {
                    mar_i7_DK++;
                }
                if (item.Contains("c2d") && !item.Contains("OEM") && item.Contains("DK"))
                {
                    mar_c2d_DK++;
                }
                if (item.Contains("c2d") && item.Contains("OEM") && item.Contains("DK"))
                {
                    oem_c2d_DK++;
                }
                if (item.Contains("c2d") && !item.Contains("OEM") && item.Contains("LP"))
                {
                    mar_c2d_LP++;
                }
                if (item.Contains("c2d") && item.Contains("OEM") && item.Contains("LP"))
                {
                    oem_c2d_LP++;
                }
                if (item.Contains("AMD") && !item.Contains("OEM") && item.Contains("DK"))
                {
                    mar_amd_DK++;
                }
                if (item.Contains("AMD") && item.Contains("OEM") && item.Contains("DK"))
                {
                    oem_amd_DK++;
                }
                if (item.Contains("AMD") && !item.Contains("OEM") && item.Contains("LP"))
                {
                    mar_amd_LP++;
                }
                if (item.Contains("AMD") && item.Contains("OEM") && item.Contains("LP"))
                {
                    oem_amd_LP++;
                }
            }

            int[] cpu_total = { oem_i3_DK, oem_i3_LP, oem_i5_LP , oem_i5_DK , oem_i7_LP , oem_i7_DK , mar_i3_LP , mar_i3_DK , mar_i5_DK ,mar_i5_LP, mar_i7_DK , mar_i7_LP, mar_c2d_DK , mar_c2d_LP, oem_c2d_LP , oem_c2d_DK, oem_amd_DK , oem_amd_LP, mar_amd_DK , mar_amd_LP };
            string[] cpu_name = { "OEM i3 DK", "OEM i3 LP", "OEM i5 LP", "OEM i5 DK", "OEM i7 LP", "OEM i7 DK", "MAR i3 LP", "MAR i3 DK", "MAR i5 DK", "MAR i5 LP", "MAR i7 DK", "MAR i7 LP", "MAR C2D DK", "MAR C2D LP", "OEM C2D LP", "OEM C2D DK", "OEM AMD DK", "OEM AMD LP", "MAR AMD DK", "MAR AMD LP" };

            //  return Json(new { oem_i3_DK = oem_i3_DK, oem_i3_LP  = oem_i3_LP , oem_i5_LP = oem_i5_LP , oem_i5_DK = oem_i5_DK, oem_i7_LP = oem_i7_LP , oem_i7_DK = oem_i7_DK , mar_i3_LP = mar_i3_LP , mar_i3_DK = mar_i3_DK , mar_i5_LP= mar_i5_LP, mar_i5_DK= mar_i5_DK, mar_i7_LP= mar_i7_LP, mar_i7_DK = mar_i7_DK, mar_c2d_DK = mar_c2d_DK, mar_c2d_LP= mar_c2d_LP, oem_c2d_LP = oem_c2d_LP, oem_c2d_DK = oem_c2d_DK, mar_amd_DK= mar_amd_DK, mar_amd_LP= mar_amd_LP,oem_amd_LP = oem_amd_LP, oem_amd_DK = oem_amd_DK },JsonRequestBehavior.AllowGet);

            return Json( new { cpu_total = cpu_total,cpu_name = cpu_name}, JsonRequestBehavior.AllowGet);
        }
            
        public ActionResult inventory_report()
        {
            
            
            return View();
        }

        public ActionResult return_stock ()
        {
            return View();
        }



       public JsonResult full_product_list()
        {
            mage mage = new mage();

            var result = mage.get_all_product();

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        


        //retail pulling asset
        public JsonResult retail_pull(string sku, string asset, string price, string mac)
        {



            if (string.IsNullOrEmpty(price))
            {
                price = "0";
            }

            string message = "";
            //format new retail sku
            string to_be_import_sku = sku + "_retail";
            var entry = new production_log();
            var mac_entry = new mac_log();

            if (mac == "true")
            {

                using (var ctx = new db_a094d4_icdbEntities())
                {
                    int temp_asset = int.Parse(asset);
                    mac_entry = ctx.mac_log.Where(s => s.ictags == temp_asset).FirstOrDefault<mac_log>();
                    if (mac_entry != null)
                    {
                        mac_entry.status = "pulled";
                    }
                }
                using (var dbCtx = new db_a094d4_icdbEntities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(mac_entry).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                pull_logging(asset, sku, "pulled", DateTime.Now, "mac_retail");
            }
            else
            {
                using (var ctx = new db_a094d4_icdbEntities())
                {
                    entry = ctx.production_log.Where(s => s.ictags == asset).FirstOrDefault<production_log>();
                    if (entry != null)
                    {
                        entry.status = "pulled";
                    }
                }
                using (var dbCtx = new db_a094d4_icdbEntities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(entry).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                pull_logging(asset, sku, "pulled", DateTime.Now, "windows_retail");
            }


            //init a new mage object for magento operations
            mage mage = new mage();

            //get the info from original product
            var original_product = mage.check_product(sku);
            
            //get the qty for original product
            var original_qty = original_product.FirstOrDefault().qty;
            var original_pid = original_product.FirstOrDefault().product_id;
    
                //update qty for inventory module
 
            
            double update_qty = double.Parse(original_qty) - 1;
            smart_inventory(original_pid, update_qty.ToString());
            mage.quick_update(update_qty.ToString(), original_pid, null);


            message = "<h4 style='color:green'>Asset " + asset + " Successfully Pulled from Inventory";
            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult bin_overview()
        {
            return View();

        }
        public ActionResult bin_overview_mac()
        {
            return View();

        }
        //get current inventory in bins 
        public JsonResult get_current_inv()
        {
            var result = (from t in db.production_log where t.status == null && t.bin_location != null orderby t.bin_location select t).ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        //get current Apple products inventory in bins 
        public JsonResult get_current_inv_mac()
        {
            var result = (from t in db.mac_log where t.status == null && t.bin_location != null orderby t.bin_location select t).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
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
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://connectall.org/update.php?product_id=" +p_id + "&qty=" + qty);
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

        //get bin details for mac machiens
        public JsonResult mac_get_bin_location (string id)
        {
            var result = (from t in db.mac_log where t.bin_location == id && t.status != "pulled" select t.ictags).ToList();
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }
        //get bin details for windows machines
        public JsonResult get_bin_location(string id)
        {
            var result = (from t in db.production_log where t.bin_location == id && t.status != "pulled" select t.ictags).ToList();
            return Json(new { result = result},JsonRequestBehavior.AllowGet);
        }


        public ActionResult bin_overview_gradec()
        {


            return View();
        }

        public JsonResult mac_reset_location(string asset)
        {
            string message = "";
            int asset_int = int.Parse(asset);
            var record = (from t in db.mac_log where t.ictags == asset_int select t.ictags).FirstOrDefault();
            try
            {
                var original = db.mac_log.Find(record.ToString());
                if (original != null)
                {
                    original.status = null;
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
        //create missing production_log entry for OEM machine from rediscovery 

        public JsonResult oem_missing(string asset, string wcoa_id)
        {
            string message = "";
            
            int temp_asset = int.Parse(asset);
            var asset_detail = (from t in db.rediscovery where t.ictag == temp_asset select t).FirstOrDefault();
            var production_log = new production_log();
            production_log.time = DateTime.Today;
            production_log.ictags = asset;
            production_log.wcoa = wcoa_id;
            production_log.ocoa = "";
            production_log.Manufacture = asset_detail.brand;
            production_log.Model = asset_detail.model;
            production_log.CPU = asset_detail.cpu;
            production_log.RAM = asset_detail.ram;
            production_log.HDD = asset_detail.hdd;
            production_log.serial = asset_detail.serial;
            production_log.channel = asset_detail.pallet;
            production_log.pre_coa = "00999-999-000-999";

            using (var dbCtx = new db_a094d4_icdbEntities())
            {
                //Add Student object into Students DBseta
                dbCtx.production_log.Add(production_log);

                // call SaveChanges method to save student into database
                dbCtx.SaveChanges();
            }
            message = "Completed";

            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }


        //create missing production_log entry from rediscovery table and coas_history table
        public JsonResult missing (string asset,string wcoa_id, string ocoa_id)
        {
            string message = "";
            string ocoa_pk = "";
            var wcoa_detail = (from t in db.coas_history where t.COA_ID == wcoa_id select t).FirstOrDefault();
            if (!string.IsNullOrEmpty(ocoa_id))
            {
                var ocoa_detail = (from t in db.coas_history where t.COA_ID == ocoa_id select t).FirstOrDefault();
                ocoa_pk = ocoa_detail.PK;
            }
            
            int temp_asset = int.Parse(asset);
            var asset_detail = (from t in db.rediscovery where t.ictag == temp_asset select t).FirstOrDefault();
            var production_log = new production_log();
            production_log.time = DateTime.Today;
            production_log.ictags = asset;
            production_log.wcoa = wcoa_detail.PK;
            production_log.ocoa = ocoa_pk;
            production_log.Manufacture = asset_detail.brand;
            production_log.Model = asset_detail.model;
            production_log.CPU = asset_detail.cpu;
            production_log.RAM = asset_detail.ram;
            production_log.HDD = asset_detail.hdd;
            production_log.serial = asset_detail.serial;
            production_log.channel = asset_detail.pallet;
            production_log.pre_coa = "00999-999-000-999";

            using (var dbCtx = new db_a094d4_icdbEntities())
            {
                //Add Student object into Students DBseta
                dbCtx.production_log.Add(production_log);

                // call SaveChanges method to save student into database
                dbCtx.SaveChanges();
            }
            message = "Completed";

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



        public JsonResult mac_write_bin_location(string asset, string id)

        {
            string sku = "";
            string message = "";
            if (!String.IsNullOrEmpty(asset))
            {

                try
                {

               
                int asset_int = int.Parse(asset);

                var entry = new mac_log();
                using (var ctx = new db_a094d4_icdbEntities())
                {
                    entry = ctx.mac_log.Where(s => s.ictags == asset_int).FirstOrDefault<mac_log>();
                        sku = entry.pallet;
                    if (entry != null)
                    {
                        entry.bin_location = id;
                    }
                }
                using (var dbCtx = new db_a094d4_icdbEntities())
                {
                    //3. Mark entity as modified
                    dbCtx.Entry(entry).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }

                //now write to magento 
                

                message = "<p style='color:green'>Asset " + asset + " is now assigned to Bin " + id + "</p>";
                }

                catch
                {
                    message = "<p style='color:red'>Cant Find Asset " + asset + " Please check Data Inputed or try to Associate Serial # with Asset tag Below. If Problem Still Occur, please Contact your Supervisor" + "</p>";
                }
            }




            return Json(new { message = message }, JsonRequestBehavior.AllowGet);
        }


        //scan asset to bin 
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
                        original.status = null;
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
            ViewData["is_mac"] = false;
            int letter_count = Regex.Matches(id, @"[a-zA-Z]").Count;
            if (letter_count > 0)
            {
                id = id.ToUpper();
                if (id.Contains("MAC"))
                {
                    ViewData["is_mac"] = true;
                }
                ViewData["is_desktop"] = true;
            }
           else
            {
                ViewData["is_desktop"] = false;
            }
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

        private filters addFilter(filters filtresIn, string key, string op, string value)
        {
            filters filtres = filtresIn;
            if (filtres == null)
                filtres = new filters();

            complexFilter compfiltres = new complexFilter();
            compfiltres.key = key;
            associativeEntity ass = new associativeEntity();
            ass.key = op;
            ass.value = value;
            compfiltres.value = ass;

            List<complexFilter> tmpLst;
            if (filtres.complex_filter != null)
                tmpLst = filtres.complex_filter.ToList();
            else tmpLst = new List<complexFilter>();

            tmpLst.Add(compfiltres);

            filtres.complex_filter = tmpLst.ToArray();

            return filtres;
        }

        public List<string> order_list(filters filter)
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123!");

            var result = mservice.salesOrderList(mlogin, filter);
            var list = new List<string>();
            foreach (var item in result)
            {

                mlogin = mservice.login("admin", "Interconnection123!");
                var order_sku = mservice.salesOrderInfo(mlogin, item.increment_id);
                for (int i = 0; i < order_sku.items.Count(); i++)
                {
                    
                    if (!string.IsNullOrEmpty(order_sku.items[i].sku) &&!order_sku.items[i].sku.Contains("ICM")  )
                    {
                        list.Add(order_sku.items[i].sku);
                    }
                    
                }


            }

            return list;
        }

        public JsonResult current_order_list ()
        {

            var oneWeekAgo = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd hh:mm:ss");
            var today = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd hh:mm:ss");
            filters filtres = new filters();
            filtres = addFilter(filtres, "status", "eq", "processing");
            filtres = addFilter(filtres, "created_at", "from", oneWeekAgo);
            filtres = addFilter(filtres, "created_at", "to", today);
            filtres = addFilter(filtres, "store_id", "eq", "1");
            var processing_list = order_list(filtres);
            filters filtres2 = new filters();
            filtres2 = addFilter(filtres2, "status", "eq", "pending");
            filtres2 = addFilter(filtres2, "created_at", "from", oneWeekAgo);
            filtres2 = addFilter(filtres2, "created_at", "to", today);
            filtres2 = addFilter(filtres2, "store_id", "eq", "1");
            var pending_list = order_list(filtres2);
            filters filtres3 = new filters();
            filtres2 = addFilter(filtres3, "status", "eq", "pending");
            filtres2 = addFilter(filtres3, "created_at", "from", oneWeekAgo);
            filtres2 = addFilter(filtres3, "created_at", "to", today);
            filtres2 = addFilter(filtres3, "store_id", "eq", "3");
            var retail_pending_list = order_list(filtres3);
            filters filtres4 = new filters();
            filtres2 = addFilter(filtres4, "status", "eq", "pending");
            filtres2 = addFilter(filtres4, "created_at", "from", oneWeekAgo);
            filtres2 = addFilter(filtres4, "created_at", "to", today);
            filtres2 = addFilter(filtres4, "store_id", "eq", "3");
            var retail_processing_list = order_list(filtres4);
           // var arr = processing_list.Union(pending_list).ToList();
            var arr = processing_list.Concat(pending_list)
                                    .Concat(retail_pending_list).Concat(retail_processing_list)
                                    .ToList();
            return Json( new { order_list = arr}, JsonRequestBehavior.AllowGet);
        }

        //import sku for retail POS
        //update QTY in main website 
        public int retail_quick_import(string price, string name, string sku, string weight, string desc, string short_desc, string qty, string[] websites, string stock, string status, string visible, string attr, string type, string tax, string img_path)
        {
            string message = "";
            //check to see if inventory has that item before adding it to the list
           
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

            mage mage = new mage();
            string path = img_path;

            
            //create sku 
            int pid = mage.retail_quick_import(retail_model);

         
            return pid;
        }

    



        //function for online store user to pull asset out from the inventory 
        public JsonResult online_store_picklist(string sku,string asset,string mac)
        {
            string result = "";
          

            
                try
            {

                if (mac == "true")
                {
                    int temp_asset = int.Parse(asset);
                    
                    var mac_entry = new mac_log();
                    using (var ctx = new db_a094d4_icdbEntities())
                    {
                        
                        mac_entry = ctx.mac_log.Where(s => s.ictags == temp_asset).FirstOrDefault<mac_log>();
                        if(mac_entry !=null)
                        {
                            mac_entry.status = "pulled";
                            
                        }
                    }
                    using (var dbCtx = new db_a094d4_icdbEntities())
                    {
                        //3. Mark entity as modified
                        dbCtx.Entry(mac_entry).State = System.Data.Entity.EntityState.Modified;
                        
                        //4. call SaveChanges
                        dbCtx.SaveChanges();
                    }
                    pull_logging(asset, sku, "pulled", DateTime.Now, "mac_online");
                }
                else
                {
                    
                    var entry = new production_log();
                    using (var ctx = new db_a094d4_icdbEntities())
                    {
                      
                        entry = ctx.production_log.Where(s => s.ictags == asset).FirstOrDefault<production_log>();
                        if (entry != null)
                        {
                            entry.status = "pulled";
                           
                        }
                    }
                    using (var dbCtx = new db_a094d4_icdbEntities())
                    {
                        
                        //3. Mark entity as modified
                        dbCtx.Entry(entry).State = System.Data.Entity.EntityState.Modified;

                        //4. call SaveChanges
                        dbCtx.SaveChanges();
                    }
                    pull_logging(asset, sku, "pulled", DateTime.Now, "windows_online");
                }
                
                result = "<h3 style='color:green'>Asset "+asset+" is now Pulled from Inventory</h3>";
            }
                 
    catch(Exception e)
            {
                pull_logging(asset, sku, e.ToString(), DateTime.Now, "error");
                using (var dbCtx = new db_a094d4_icdbEntities())
                {
                    
                    //4. call SaveChanges
                    dbCtx.SaveChanges();
                }
                result = "<h3 style='color:red'>Failed, Please Check Data Input</h3>";
            }
            return Json(new {result = result} ,JsonRequestBehavior.AllowGet);
        }


        //log for all pull activity
        public void pull_logging (string ictag, string sku, string action, DateTime time, string channel)
        {
            var pull_log = new pull_log();
            using (var ctx = new db_a094d4_icdbEntities())
            {
                
                pull_log.time = DateTime.Now;
                pull_log.ictag = ictag;
                pull_log.sku = sku;
                pull_log.action = action;
                pull_log.channel = channel;
                

            }
            using (var dbCtx = new db_a094d4_icdbEntities())
            {
                //3. Mark entity as modified
               
                dbCtx.pull_log.Add(pull_log);
                //4. call SaveChanges
                dbCtx.SaveChanges();
            }

           

        }

        public JsonResult get_serial (int asset)
        {

            var serial_result = (from t in db.rediscovery where t.ictag == asset select t.serial).FirstOrDefault();

            return Json(serial_result, JsonRequestBehavior.AllowGet);
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
            var url = "http://connectall.org/get_product.php";
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
        public async Task<JsonResult> order_json_ts(Models.ts_order[] tsorder)
        {
            await shipstation(tsorder);
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
            string result = mage.create_order_ts(tsorder);

            return Json(new { scuess = true, message = result }, JsonRequestBehavior.AllowGet);
        }

        public static async Task shipstation(Models.ts_order[] tsorder)
        {

            var baseAddress = new Uri("https://ssapi.shipstation.com/");

            foreach (var csv_item in tsorder)
            {

           
           

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");


                shipstation_order_model order = new shipstation_order_model();
                shipstation_order_model.Billto billto = new shipstation_order_model.Billto();
                shipstation_order_model.Shipto shipto = new shipstation_order_model.Shipto();
                shipstation_order_model.Item item = new shipstation_order_model.Item();
                shipstation_order_model.Advancedoptions option = new shipstation_order_model.Advancedoptions();
                    //order info
                    //parse current ts order number to int
                    int ts_order_number = int.Parse(csv_item.TS_Order_Number);
                    order.orderId = ts_order_number;
                order.orderNumber = csv_item.TS_Order_Number ;
                var date = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff");

                order.orderDate = date;
                order.createDate = date;
                order.modifyDate = date;
                order.paymentDate = date;
                order.shipByDate = date;
                order.orderStatus = "awaiting_shipment";
                order.customerEmail = csv_item.Org_Email;

                //bill to info required
                order.billTo = billto;
                    billto.name = csv_item.Org_Contact_Name;
                    billto.street1 = csv_item.Org_Street_Address;
                    //billto.city = "Seattle";
                    //billto.state = "WA";
                    //billto.country = "US";
                    billto.company = csv_item.Org_Name;

                    //ship to info
                    order.shipTo = shipto;
                    shipto.name = csv_item.Org_Contact_Name;
                    shipto.street1 = csv_item.Org_Street_Address;
                    shipto.postalCode = csv_item.Org_Zip_Code;
                    shipto.city = csv_item.Org_City;
                    shipto.state = csv_item.Org_State;
                    shipto.country = csv_item.Org_Country;
                    shipto.company = csv_item.Org_Name;

                    //item info
                    shipstation_order_model.Item[] items = new shipstation_order_model.Item[] {
                    item
                };
                    item.sku = csv_item.Item_ID;
                    item.name = csv_item.Product_Name;
                    item.quantity = csv_item.Total_Product_Quantity;
                

                
                order.items = items;
                order.advancedOptions = option;
                    //store id for TechSoup
                    option.storeId = 12171;
                option.warehouseId = 11239;


                var order_json = new JavaScriptSerializer().Serialize(order);
               

                using (var content = new StringContent(order_json, System.Text.Encoding.Default, "application/json"))

               
                {
                    using (var response = await httpClient.PostAsync("orders/createorder", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                    }
                }

            }
            }
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
            
           string url = "http://www.connectall.org/qbjson.json";
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
