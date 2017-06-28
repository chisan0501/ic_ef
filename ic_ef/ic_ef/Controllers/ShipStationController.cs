using ic_ef;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ic_ef.Controllers
{

    public class ShipStationController : Controller
    {

        shipstation ss = new shipstation();

        // GET: ShipStation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult main()
        {
            
            
            using (var db = new db_a094d4_icdbEntities1())
            {
                
                
              //  var result = (from t in db.shipstation_log where t.store_id == 12171 && t.shipment_date.Contains("2016-01") select t).ToList();
                //distinct customer field 2 list
             //   var q = result.GroupBy(x => x.custom_field_2).Select(group => group.First()).ToList() ;
                //iterate that list
            //    for (int i =0;i < q.Count; i++)
                {
                    //break the fields to indiv item
                    //add number of item to SKU
                    //switch (q[i].sku) {

                    //    case "HW-48678":
                    //        hw48678 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-48680":
                    //        hw48680 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-48986":
                    //        hw48986 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-48993":
                    //        hw48993 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-48997":
                    //        hw48997 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49000":
                    //        hw49000 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49001":
                    //        hw49001 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49110":
                    //        hw49110 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49193":
                    //        hw49193 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49195":
                    //        hw49195 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49347":
                    //        hw49347 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49812":
                    //        hw49812 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49734":
                    //        hw49734 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49704":
                    //        hw49704 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49654":
                    //        hw49654 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49652":
                    //        hw49652 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49649":
                    //        hw49649 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49557":
                    //        hw49557 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //    case "HW-49555":
                    //        hw49555 += count_item(q[i].custom_field_2,int.Parse(q[i].item_qty.ToString()));
                    //        break;
                    //}
                  
                }
           
            }

            return View();
        }

        public async Task<JsonResult> refresh_now()
        {
            string message = "";
            try
            {
                await get_shipment("interconnection123");
                message = "Complete";
            }
            catch
            {
                message = "Could not Complete";
            }

            return Json(message,JsonRequestBehavior.AllowGet);
        }


        public async Task<Models.ShipStation_orderID.open_order.Rootobject> open_order() {

            var baseAddress = new Uri("https://ssapi.shipstation.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("orders?orderStatus=awaiting_shipment&storeID=12171"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Models.ShipStation_orderID.open_order.Rootobject>(responseData);

                    return result;
                }
            }


                    
        }
        public void count_item(shipstation_log entry)
        {

            try
            {
                var write_table = new ship_log();
               
                if (string.IsNullOrEmpty(entry.custom_field_2))
                {

                }
                else
                {
                    try {

                        string[] lines = entry.custom_field_2.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        foreach (var item in lines)
                        {
                            using (db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1())
                            {
                                var exisit = (from t in db.ship_log where t.ictag == item select t).SingleOrDefault();
                                if (exisit == null)
                                {
                                    write_table.ictag = item;
                                    write_table.order_id = entry.order_id;
                                    write_table.order_num = entry.order_num;
                                    write_table.shipdate = entry.shipment_date;
                                    write_table.sku = entry.sku;
                                    write_table.status = entry.status;
                                    write_table.store_id = entry.store_id;
                                    db.ship_log.Add(write_table);
                                    db.SaveChanges();

                                }
                                var update = db.ts_stock.Where(s => s.ictag == item).FirstOrDefault();
                                if (update != null) {
                                    update.status = "Shipped";
                                    update.update_time = DateTime.Now;
                                    using (var dbCtx = new db_a094d4_icdbEntities1())
                                    {
                                        //Mark entity as modified
                                        dbCtx.Entry(update).State = System.Data.Entity.EntityState.Modified;

                                        //call SaveChanges
                                        dbCtx.SaveChanges();
                                    }
                                }

                               
                            }
                        }

                    }

                    catch {

                        entry.custom_field_2 = "";

                    }
                   
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            
               
           
           
        }


        public async Task<ss_order.Order_num_to_orderID.Rootobject> get_orderID(int orderNumber)
        {
            var result = new ss_order.Order_num_to_orderID.Rootobject();
            int orderID = 0;
            var baseAddress = new Uri("https://ssapi.shipstation.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("orders?orderNumber="+orderNumber))
                {

                    string responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ss_order.Order_num_to_orderID.Rootobject>(responseData);
                   // orderID = result.orders[0].orderId;
                }

             

            }
            return result;
        }
        public async Task mark_as_shipped(int orderID)
        {
           
            var baseAddress = new Uri("https://ssapi.shipstation.com");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                Models.mark_shipped mark = new Models.mark_shipped();
                mark.orderId = orderID;
                mark.carrierCode = "6";
                mark.notifyCustomer = false;
                mark.notifySalesChannel = true;
                mark.trackingNumber = orderID.ToString();
                mark.shipDate = DateTime.Today.ToString("yyyy-MM-dd");
               // string json = new JavaScriptSerializer().Serialize(mark);
                string json = JsonConvert.SerializeObject(mark);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");
                

                using (var content = new StringContent(json,Encoding.Default, "application/json"))
                //using (var content = new StringContent(json, System.Text.Encoding.Default,
                //                    "application/json")) 
                {
                    using (var response = await httpClient.PostAsync("orders/markasshipped", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                    }
                }

            }
        }


        //update status of orders 
        public JsonResult cancel_order(string orderId, string status)
        {

            mage mage = new mage();

            var result = mage.cancel_order(orderId,status);


            return Json(JsonRequestBehavior.AllowGet);

        }

        //get detail of orders
        public JsonResult get_detail()
        {
            var mage = new mage();
           
            var result = mage.get_open_orders();


            return Json(JsonRequestBehavior.AllowGet);
        }

        public async Task get_order_detail(int orderID)
        {



            var bb = 0;
            Thread.Sleep(1700);
            //try { 
            var baseAddress = new Uri("https://ssapi.shipstation.com/");
            db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();   
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("orders/" + orderID + ""))
                {
                       
                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Models.ShipStation_orderID.Rootobject>(responseData);
                        for (int i = 0; i < result.items.Length; i++) {

                            var write_table = new shipstation_log();

                            if (result.advancedOptions.customField1 == null)
                            {

                                result.advancedOptions.customField1 = "";


                            }
                            if (result.advancedOptions.customField2 == null)
                            {
                                result.advancedOptions.customField2 = "";
                            }

                            if (result.advancedOptions.customField3 == null)
                            {
                                result.advancedOptions.customField3 = "";
                            }

                            write_table.sku = result.items[i].sku;
                            write_table.store_id = result.advancedOptions.storeId;
                            write_table.item_name = result.items[i].name;
                            write_table.item_qty = result.items[i].quantity;
                            write_table.item_url = result.items[i].imageUrl;
                            write_table.custom_field_1 = result.advancedOptions.customField1.ToString();
                            write_table.custom_field_2 = result.advancedOptions.customField2.ToString();
                            write_table.custom_field_3 = result.advancedOptions.customField3.ToString();
                            write_table.order_id = orderID;
                            write_table.order_num = result.orderNumber;
                            write_table.status = result.orderStatus;
                            if (result.shipDate == null)
                            {
                                write_table.shipment_date = null;
                            }
                            else
                            {
                                write_table.shipment_date = DateTime.Parse(result.shipDate);
                            }
                            
                            db.shipstation_log.Add(write_table);
                            db.SaveChanges();
                            Debug.WriteLine(bb++);
                            count_item(write_table);

                        }
                  

                }


            
                        }

            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //{
            //    Exception raise = dbEx;
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            string message = string.Format("{0}:{1}",
            //                validationErrors.Entry.Entity.ToString(),
            //                validationError.ErrorMessage);
            //            // raise a new exception nesting  
            //            // the current instance as InnerException  
            //            raise = new InvalidOperationException(message, raise);
            //        }
            //    }
            //    throw raise;
            //}

            
        }


        public ActionResult search ()
        {
            return View();
        }

        public JsonResult search_json(string item)
        {
            List<shipstation_log> result;
            using (var db = new db_a094d4_icdbEntities1())
            {
                result = db.shipstation_log.SqlQuery("SELECT * FROM `db_a094d4_icdb`.`shipstation_log` WHERE LOWER(CONVERT(`row_id` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`shipment_date` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`order_id` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`order_num` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`item_name` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`item_qty` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`item_url` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`custom_field_1` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`custom_field_2` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`custom_field_3` USING utf8mb4)) LIKE '%"+item+"%' OR LOWER(CONVERT(`status` USING utf8mb4)) LIKE '%"+item+"%' ;").ToList();
        
            }

            return Json(result,JsonRequestBehavior.AllowGet); 
        }


        public async Task<JsonResult> confrim_mark (int orderID)
        {
            string message = "";
            try
            {
                await mark_as_shipped(orderID);
                message = orderID + " have successfully mark as shipped";
            }
            catch
            {
                message = "Something went wrong with order : " + orderID;
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> mark_ship(int order_num)
        {
            var result = new ss_order.Order_num_to_orderID.Rootobject();
          
            try
            {
              result =  await get_orderID(order_num);
             
            }
            catch (Exception e)
            {
                
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public async Task<ActionResult> get_shipment(string id)
        {
            db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();
            var baseAddress = new Uri("https://ssapi.shipstation.com/");
            if (id=="interconnection123")
            {

                
                    try
                    {




                        using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                        {
                            //Debug.Write(jj);
                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");
                           // var DateStart = new DateTime(2016, 12, 13);
                             var DateStart = DateTime.Today;
                              var DateEnd = DateTime.Today.AddDays(+1);
                           // var DateEnd = new DateTime(2016, 12, 30);
                            using (var response = await httpClient.GetAsync("shipments?includeShipmentItems=true&shipDateStart=" + DateStart + "&shipDateEnd=" + DateEnd))
                            {
                                if (response.IsSuccessStatusCode != true) {
                                    return View(ViewBag.message = response.RequestMessage);
                                    
                                }
                                string responseData = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<ss_order.ss_shipment_detail.Rootobject>(responseData);

                                var total_page = result.pages;

                                for (int i = 0; i < result.shipments.Length; i++)
                                {
                                    if (result.shipments[i].shipmentItems != null)
                                    {
                                        

                                            var order_id = result.shipments[i].orderId;
                                            await get_order_detail(order_id);

                                       

                                    }






                                }


                            }

                        }

                    
                    db.Dispose();

                    using (var db2 = new db_a094d4_icdbEntities1())
                    {
                        db2.Database.ExecuteSqlCommand(
                     "insert into process_run_time (process) values ('ShipStation')");
                    }
                   
                   


                }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting  
                                // the current instance as InnerException  
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                
                ViewBag.message = "Completed";
                return View();
            }

                

                
            

            else
            {
                ViewBag.message = "Invalid ID";
                return View();
            }
        }

        
    }
}


     
      
    

        


    
