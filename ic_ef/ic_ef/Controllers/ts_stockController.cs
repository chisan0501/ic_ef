using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ic_ef;
using Newtonsoft.Json;
using System.Collections;

namespace ic_ef.Controllers
{
    public class ts_stockController : Controller
    {
        private db_a094d4_icdbEntities1 db = new db_a094d4_icdbEntities1();

        // GET: ts_stock


       public ActionResult stock()
        {

            return View();
        }
        
        public ActionResult Index()
        {
            
              
            

            
            return View(db.ts_stock.ToList());
        }


        public ActionResult all_time ()
        {


            return View();
        }
        public JsonResult get_addstcok()
        {
            var today = DateTime.Today.AddDays(1);
            var ytd = DateTime.Today;
            var result = (from t in db.ts_stock where t.update_time == null && t.time < today && t.time > ytd select t).ToList();



            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult  open_order ()
        {
            return View();
        }


        public JsonResult get_month_data ()
        {
            
            var last_month = db.Database.SqlQuery<Models.weekly>("select count(*) as count_num from ship_log where store_id = '12171' and shipdate BETWEEN DATE_FORMAT(NOW() - INTERVAL 1 MONTH, '%Y-%m-01 00:00:00') AND DATE_FORMAT(LAST_DAY(NOW() - INTERVAL 1 MONTH), '%Y-%m-%d 23:59:59')").FirstOrDefault() ;
            var this_month = db.Database.SqlQuery<Models.weekly>("select count(*) as count_num from ship_log where store_id = '12171' and shipdate BETWEEN DATE_FORMAT(NOW(), '%Y-%m-01 00:00:00') AND DATE_FORMAT(LAST_DAY(NOW() ), '%Y-%m-%d 23:59:59')").FirstOrDefault();
            var diff = this_month.count_num - last_month.count_num ;
            var result = db.Database.SqlQuery<Models.Techsoup_Model>("select date_format(shipdate,'%Y-%m') as date,Count(sku) as numberofitem  from ship_log where store_id = '12171' AND shipdate >= DATE_SUB(CURDATE(), INTERVAL 1 year)  group by date_format(shipdate,'%Y-%m') order by date").ToList();
            int average = 0;
            foreach (var item in result)
            {

                average += item.numberofitem;
            }
            average = average / result.Count();
            var daily_avg = this_month.count_num / 30;
            return Json(new { daily = daily_avg ,result = result, average = average,diff = diff },JsonRequestBehavior.AllowGet);
        }

        public async System.Threading.Tasks.Task<JsonResult> get_open_order()
        {
          
            List<Models.get_open_order> item_list_sku = new List<Models.get_open_order>();
            List<Models.get_open_order> item_list = new List<Models.get_open_order>();
            var ss = new ShipStationController();
            var result = await ss.open_order();

            for (int i = 0; i < result.orders.Length; i++)
            {
                
               
                var temp = new Models.get_open_order();
                temp.orderID = result.orders[i].orderId;
                temp.orderStatus = result.orders[i].orderStatus;
                temp.orderDate = result.orders[i].modifyDate.ToString();
                temp.item_num = result.orders[i].items.Length;
                
                for (int j = 0; j < temp.item_num; j++)
                {
                    var sku_temp = new Models.get_open_order();
                 
                    sku_temp.sku = result.orders[i].items[j].sku;
             
                    
                    sku_temp.count_qty = result.orders[i].items[j].quantity;
               
                    temp.sku += result.orders[i].items[j].sku + "<br/>";
                    temp.item_name += result.orders[i].items[j].name + "<br/>";
                    temp.qty += +result.orders[i].items[j].quantity + "<br/>";
                   
                    item_list_sku.Add(sku_temp);
                }
            
                item_list.Add(temp);
            }


            //in stock product
            var stock_breakdown = (from t in db.ts_stock where t.status == "Ready_for_Shipping" group t by t.sku into g select new { sku = g.Key, count_qtys = (from t in g select t.sku).Count() }).ToList();

            //open orders
            var online_breakdown = (from t in item_list_sku group t by t.sku into g select new { sku = g.Key, count_qty = (from t in g select t.count_qty).Sum() }).ToList();

            var online_sku_only = (from t in item_list_sku select t.sku).Distinct();

            //combine 
            var item_breakdown =( from t in online_sku_only join t2 in stock_breakdown on t equals t2.sku select new { sku = t,t2.count_qtys }).ToList();

            var monthly_data = db.Database.SqlQuery<Models.Techsoup_Model>("select Count(sku) as numberofitem  from ship_log where sku like '%HW-%' AND shipdate >= DATE_SUB(CURDATE(), INTERVAL 30 day)  group by date_format(shipdate,'%Y-%m-%d %H')").ToList();
            var number = (from t in monthly_data select t.numberofitem).ToList();

            //   var pallet_count = from l in db.pallet group l by l.pallet_name into g select new { pallet_name = g.Key, Count = (from l in g select l.pallet_name).Distinct().Count() };
            // return Json(new {order = result.orders}, JsonRequestBehavior.AllowGet);
            return Json(new { month_graph = number, item_list  = item_list, online_breakdown = online_breakdown, stock_breakdown = stock_breakdown, item_breakdown= item_breakdown } , JsonRequestBehavior.AllowGet);
        }

        //dropdown menu from in stock detail
        public async System.Threading.Tasks.Task<JsonResult> get_sku_detail(string sku)
        {
            var production_sku = sku.Replace("HW-", "TSG_");
            var ss = new ShipStationController();
            var result = await ss.open_order();
            List<Models.get_open_order> item_list = new List<Models.get_open_order>();
            List<Models.get_open_order> item_list_sku = new List<Models.get_open_order>();
            for (int i = 0; i < result.orders.Length; i++)
            {
                
                var temp = new Models.get_open_order();
                temp.orderID = result.orders[i].orderId;
                temp.orderStatus = result.orders[i].orderStatus;
                temp.orderDate = result.orders[i].modifyDate.ToString();
                temp.item_num = result.orders[i].items.Length;

                for (int j = 0; j < temp.item_num; j++)
                {
                    var sku_temp = new Models.get_open_order();
                    sku_temp.sku = result.orders[i].items[j].sku;
                    sku_temp.count_qty = result.orders[i].items[j].quantity;
                    temp.sku += result.orders[i].items[j].sku + "<br/>";
                    temp.item_name += result.orders[i].items[j].name + "<br/>";
                    temp.qty += result.orders[i].items[j].quantity + "<br/>";
                    item_list_sku.Add(sku_temp);
                }

                item_list.Add(temp);
            }


           //open orders
            var online_breakdown = (from t in item_list_sku group t by t.sku into g select new { sku = g.Key, count_qty = (from t in g select t.count_qty).Sum() }).ToList();


            var today = DateTime.Today.AddDays(1);
            var month_ago = DateTime.Today.AddDays(-30);
            var yesterday_sametime = DateTime.Now.AddHours(-24);
            var now = DateTime.Now;
            var pending = (from t in online_breakdown where t.sku == sku select t.count_qty);
            var onhand = (from t in db.ts_stock where t.sku == sku && t.status == "Ready_for_Shipping" select t).Count();
            var avg_month = (from a in db.ship_log where a.sku == sku && a.shipdate < today && a.shipdate > month_ago select a).ToList();
            var pull_last_month = (from a in db.ts_stock where a.update_time != null && a.update_time < today && a.update_time > month_ago && a.sku == sku select a).Count();
            var item_name = (from t in item_list where t.sku == sku select t.item_name).FirstOrDefault();
            var production = (from t in db.production_log where t.channel == production_sku && t.time < now && t.time > yesterday_sametime select t).Count();
            var production_detail = (from t in db.production_log where t.channel == production_sku && t.time < now && t.time > yesterday_sametime select t).ToList();
            var monthly_data = db.Database.SqlQuery<Models.Techsoup_Model>("select Count(sku) as numberofitem  from ship_log where sku='"+sku+"' AND shipdate >= DATE_SUB(CURDATE(), INTERVAL 30 day)  group by date_format(shipdate,'%Y-%m-%d %H')").ToList();
            var number = (from t in monthly_data select t.numberofitem).ToList();


            return Json(new { month_graph_sku = number,pulled = pull_last_month,production_detail = production_detail ,production = production,name = item_name,pending_order = pending, onhand = onhand, avg_month = avg_month}, JsonRequestBehavior.AllowGet);
        }


        public JsonResult time_data(DateTime date_from, DateTime date_to)
        {


           

            var result = (from t in db.ship_log where t.shipdate > date_from && t.shipdate < date_to && t.store_id == 12171  group t by t.sku into g  select new { sku = g.Key, count_qty = (from t in g select t.sku).Count() }).ToList();


            return Json(result,JsonRequestBehavior.AllowGet);
        }

        // GET: ts_stock/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ts_stock ts_stock = db.ts_stock.Find(id);
            if (ts_stock == null)
            {
                return HttpNotFound();
            }
            return View(ts_stock);
        }


        public async System.Threading.Tasks.Task<JsonResult> get_ts_stat()
        {
            var ss = new ShipStationController();


            var weekly = db.Database.SqlQuery<ss_order.weekly_order>("select date_format(shipdate,'%Y-%m-%d') as date,Count(shipdate) as numberofitem  from ship_log where sku like '%HW%' and shipdate >= DATE_SUB(CURDATE(), INTERVAL 7 day) group by date_format(shipdate,'%Y-%m-%d')"
               ).ToList();
            
            var result = await ss.open_order();
            var orders = result.total;
            //    var result = JsonConvert.DeserializeObject<Models.ShipStation_orderID.open_order.Rootobject>(open_order.ToString());
            var today = DateTime.Today;
            var tmr = DateTime.Today.AddDays(1);
            var ship_today = (from t in db.ship_log where t.status == "Shipped" && t.sku.Contains("HW-") && t.shipdate == today select t).Count();
            var add_today = (from t in db.ts_stock where t.time > DateTime.Today && t.time < tmr select t).Count();
            var shipped = (from t in db.ship_log where t.status == "Shipped" && t.sku.Contains("HW-") select t).Count();
            var instock = (from t in db.ts_stock where t.status == "Ready_for_Shipping" select t).Count();
            var validated = (from t in db.process_run_time where t.process == "ShipStation" orderby t.time descending select t.time).FirstOrDefault();

            validated = validated.AddMinutes(60);

            return Json(new { weekly = weekly, orders = orders ,shipped = shipped, instock = instock, validated = validated, ship_today = ship_today, add_today = add_today }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult remove_ts_stock(string asset,string sku)
        {
            string message = "";

            try
            {
                var ts_stock = new ts_stock { ictag = asset };
                db.ts_stock.Attach(ts_stock);
                db.ts_stock.Remove(ts_stock);
                db.SaveChanges();
                message = "Asset " + asset + " Removed";
            }
         catch(Exception e)
            {
                message = "Error";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_channel_dropdown()
        {

            var result = (from t in db.label_menu where t.name.Contains("TechSoup") select t.product).ToList();

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        //add ts stock from production
        public JsonResult ts_add_stock(string[] asset)
        {
            
            string message = "";
            string current_asset = "";
            //format the tsg sku to the ss format

           
                
               // channel = channel.Replace("TSG_", "HW-");
                
                

                for (int i = 0; i < asset.Length; i++)
                {
                        string temp_asset  = asset[i];
                var db = new db_a094d4_icdbEntities1();
                var exisit = (from t in db.ts_stock where t.ictag == temp_asset select t).FirstOrDefault();

                if (exisit == null)
                {
                    int int_asset = int.Parse(temp_asset);
                    var channel = (from t in db.rediscovery where t.ictag == int_asset select t.pallet).FirstOrDefault();
                    if (!channel.Contains("TSG_"))
                    {
                        message += "<p style='color:red'>" + asset[i] + " has a non-TechSoup SKU</p>";
                        continue;
                    }
                    channel = channel.Replace("TSG_", "HW-");
                    ts_stock ts = new ts_stock();
                    ts.ictag = temp_asset;
                    current_asset = temp_asset;
                    ts.sku = channel;
                    ts.status = "Ready_for_Shipping";
                    db.ts_stock.Add(ts);

                    db.SaveChanges();
                   message += "<p style='color:green'>" + asset[i] + " Sucessfully Added to "+channel+"</p>";
                }
                else {
                    message += "<p style='color:red'>" + asset[i] + " Failed to Import (possible duplicate entry)</p>";

                }
                    
                    
                    
                }







            
            
           
           
            db.Dispose();

            return Json(message,JsonRequestBehavior.AllowGet);
        }

        //techSoup stock Recon
        public JsonResult ts_stock_recon(string channel, string[] asset) {
             
            //format the tsg sku to the ss format
            
                channel = channel.Replace("TSG_", "HW-");

            //get the current stock in the cage
            var current_stock = (from t in db.ts_stock where t.sku == channel select t).ToList();

            //compare current_stock with asset array (scanned item)
            //output the result


            return Json(JsonRequestBehavior.AllowGet);
        }


        public ActionResult add_stock() {




            return View();
        }

        public ActionResult stock_recon ()
        {


            return View();
        }

        // GET: ts_stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ts_stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "last_manual_count,last_auto_count,qty,sku,ictag")] ts_stock ts_stock)
        {
            if (ModelState.IsValid)
            {
                db.ts_stock.Add(ts_stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ts_stock);
        }

        // GET: ts_stock/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ts_stock ts_stock = db.ts_stock.Find(id);
            if (ts_stock == null)
            {
                return HttpNotFound();
            }
            return View(ts_stock);
        }

        // POST: ts_stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "last_manual_count,last_auto_count,qty,sku,ictag")] ts_stock ts_stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ts_stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ts_stock);
        }

        // GET: ts_stock/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ts_stock ts_stock = db.ts_stock.Find(id);
            if (ts_stock == null)
            {
                return HttpNotFound();
            }
            return View(ts_stock);
        }

        // POST: ts_stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ts_stock ts_stock = db.ts_stock.Find(id);
            db.ts_stock.Remove(ts_stock);
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
