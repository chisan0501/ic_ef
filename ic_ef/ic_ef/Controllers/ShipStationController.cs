using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<ActionResult> main()
        {
            db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();
            for (int b = 1; b<172;b++) {

                Debug.WriteLine(b);
                var baseAddress = new Uri("https://ssapi.shipstation.com/");
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");


                    var createDateStart = DateTime.Today.AddDays(-5);
                    var createDateEnd = DateTime.Today.AddDays(-3);

                    using (var response = await httpClient.GetAsync("orders?page="+b+""))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();




                        var result = JsonConvert.DeserializeObject<ss_order.ss_order_detail.Rootobject>(responseData);
                        // laptop_inv = result;
                        for (int ii = 0; ii < result.orders.Length; ii++)

                        {
                            try
                            {

                                var order_id = result.orders[ii].orderId.ToString();

                                var order_status = result.orders[ii].orderStatus;


                                for (int i = 1; i < result.orders[ii].items.Length; i++)
                                {




                                    var item_create_date = result.orders[ii].items[i].createDate;

                                    var write_table = new shipstation_log();
                                    write_table.order_id = order_id;
                                    write_table.item_name = result.orders[ii].items[i].name;
                                    write_table.order_status = order_status;
                                    if (result.orders[ii].advancedOptions.customField1 == null)
                                    {
                                        write_table.note1 = "";
                                    }
                                    else
                                    {
                                        write_table.note1 = result.orders[ii].advancedOptions.customField1.ToString();
                                    }
                                    if (result.orders[ii].advancedOptions.customField2 == null)
                                    {
                                        write_table.note2 = "";
                                    }
                                    else
                                    {
                                        write_table.note2 = result.orders[ii].advancedOptions.customField2.ToString();
                                    }

                                    if (result.orders[ii].advancedOptions.customField3 == null)
                                    {
                                        write_table.note3 = "";
                                    }
                                    else
                                    {
                                        write_table.note3 = result.orders[ii].advancedOptions.customField3.ToString();
                                    }

                                    write_table.create_date = item_create_date;
                                    db.shipstation_log.Add(write_table);
                                    db.SaveChanges();

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

                    }





                }

            }
            


            db.Dispose();
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> get_shipment(string id)
        {

            if(id=="interconnection123")
            {
                db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

                var baseAddress = new Uri("https://ssapi.shipstation.com/");
                try
                {




                    using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                    {

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");
                        var DateStart = DateTime.Today;
                        var DateEnd = DateTime.Today.AddDays(+1);

                        using (var response = await httpClient.GetAsync("shipments?includeShipmentItems=true&shipDateStart=" + DateStart + "&shipDateEnd=" + DateEnd + ""))
                        {

                            string responseData = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<ss_order.ss_shipment_detail.Rootobject>(responseData);

                            var total_page = result.pages;

                            for (int i = 0; i < result.shipments.Length; i++)
                            {
                                if (result.shipments[i].shipmentItems != null)
                                {
                                    for (int j = 0; j < result.shipments[i].shipmentItems.Length; j++)
                                    {
                                        var write_table = new shipstation_shipment_log();

                                        write_table.item_name = result.shipments[i].shipmentItems[j].name;
                                        write_table.item_sku = result.shipments[i].shipmentItems[j].sku;
                                        write_table.image_url = result.shipments[i].shipmentItems[j].imageUrl;
                                        write_table.item_qty = result.shipments[i].shipmentItems[j].quantity;
                                        write_table.ship_date = result.shipments[i].shipDate;
                                        write_table.store_id = result.shipments[i].advancedOptions.storeId;
                                        write_table.shipment_id = result.shipments[i].shipmentId;
                                        write_table.order_id = result.shipments[i].orderId;
                                        write_table.order_number = result.shipments[i].orderNumber;
                                        db.shipstation_shipment_log.Add(write_table);
                                        db.SaveChanges();
                                    }

                                }






                            }


                        }

                    }

                    db.Dispose();
                    ViewBag.message = "Completed";
                    return View();
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

            else
            {
                ViewBag.message = "Invalid ID";
                return View();
            }
        }

        
    }
}


     
      
    

        


    
