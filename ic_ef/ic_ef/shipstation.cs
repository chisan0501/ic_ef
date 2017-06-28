using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ShipStationAccess;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace ic_ef
{
    public class shipstation
    {

        public static void ss_wraper()
        {


        }
        public static async Task basic_call()
        {
            List<Models.shipstationProduct> ss_product = new List<Models.shipstationProduct>();
            var baseAddress = new Uri("https://ssapi.shipstation.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("products"))
                {

                    var responseData = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Models.shipstationProduct.Rootobject oRootObject = new Models.shipstationProduct.Rootobject();
                    oRootObject = oJS.Deserialize<Models.shipstationProduct.Rootobject>(responseData);
                }
            }
        }
        public static async Task shipment()
        {
            var baseAddress = new Uri("https://ssapi.shipstation.com/");

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("shipments"))
                {

                    string responseData = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Models.shipstationShipment.RootObject oRootObject = new Models.shipstationShipment.RootObject();
                    oRootObject = oJS.Deserialize<Models.shipstationShipment.RootObject>(responseData);
                }
            }
        }
        public static async Task shipment_options()
        {
            var baseAddress = new Uri("https://ssapi.shipstation.com/");
            
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");
                
                using (var response = await httpClient.GetAsync("shipments?includeShipmentItems=true&shipDateStart=2016-07-12")) 
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Models.shipstationShipment.RootObject oRootObject = new Models.shipstationShipment.RootObject();
                    oRootObject = oJS.Deserialize<Models.shipstationShipment.RootObject>(responseData);
                }
            }
        }
        public static async Task get_Order()
        {
            var baseAddress = new Uri("https://ssapi.shipstation.com/orders/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("37375537"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Models.shipstationOrder.RootObject oRootObject = new Models.shipstationOrder.RootObject();
                    oRootObject = oJS.Deserialize<Models.shipstationOrder.RootObject>(responseData);
                  //  string asset = oRootObject.advancedOptions.customField2;

                }
            }
        }
        static async Task list_all_Order()
        {
            var baseAddress = new Uri("https://ssapi.shipstation.com/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                using (var response = await httpClient.GetAsync("orders"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    Models.shipstationOrder.RootObject oRootObject = new Models.shipstationOrder.RootObject();
                    oRootObject = oJS.Deserialize<Models.shipstationOrder.RootObject>(responseData);


                }
            }

        }
        public static async Task list_Order_options()
        {
            var baseAddress = new Uri("https://ssapi.shipstation.com/");
            for (int pagei = 148; pagei <= 400; pagei++)
            {
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    Debug.WriteLine("*************I am starting page " + pagei);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Basic ZmU3YzE2MGMyZjE0NDc1ZDljNWQ0ZWI2ZmMzYmRhOWU6YzRiM2RhMjlkZWZlNDgyOWJlZmRlYTExNmU1N2Q5ZTY=");

                    using (var response = await httpClient.GetAsync("orders?createDateStart=2015-07-01 00:00:00&createDateEnd=2016-07-14 00:00:00&page="+pagei+"&pageSize=40"))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        JavaScriptSerializer oJS = new JavaScriptSerializer();
                        Models.shipstationOrderOptions.RootObject oRootObject = new Models.shipstationOrderOptions.RootObject();
                        oRootObject = oJS.Deserialize<Models.shipstationOrderOptions.RootObject>(responseData);

                        for (int i = 0; i < oRootObject.orders.Count(); i++)
                        {
                            string status = oRootObject.orders[i].orderStatus;
                            string orderNum = oRootObject.orders[i].orderNumber;
                            var asset = oRootObject.orders[i].advancedOptions.customField2;

                            if ((!string.IsNullOrEmpty(asset)) && !asset.Contains("http") && asset.Contains("\n"))
                            {
                                Debug.WriteLine("Original " + asset);
                                asset = Regex.Replace(asset, "[^0-9,\n]", "");
                                if (!string.IsNullOrEmpty(asset) )
                                {
                                    if (asset.Length > 8)
                                    {
                                        asset = asset.Replace("\n", ",");

                                        asset = asset.Replace(",,", ",");
                                        while (!char.IsDigit(asset[asset.Length-1]))
                                        {
                                            asset = asset.Remove(asset.Length - 1);
                                        }
                                        if (!char.IsDigit(asset[0])) {
                                            asset = asset.Substring(1);
                                        }
                                        Debug.WriteLine(asset);
                                        var splited_asset = asset.Split(',').Select(Int32.Parse).ToList();
                                        foreach (var item in splited_asset)
                                        {
                                            update_status(item.ToString(), status, orderNum);
                                        }
                                    }
                                    else
                                    {
                                        asset = asset.Replace("\n", "");
                                        update_status(asset, status, orderNum);
                                    }
                                }
                              

                            }
                        }




                    }
                }
            } 
         
           
        }
        private static void update_status(string asset,string status,string orderNum)
        {
            if (!string.IsNullOrEmpty(asset))
            {
                long asset_32 = long.Parse(asset);

                db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();
                var redisco = new rediscovery();
                var target = (from m in db.rediscovery where m.ictag == asset_32 select m);

                foreach (var m in target)
                {
                    m.status = status;
                    m.orderNum = orderNum;
                    m.location = "sold";
                }
                db.SaveChanges();
                db.Dispose();
            }
          
        }
    }
}