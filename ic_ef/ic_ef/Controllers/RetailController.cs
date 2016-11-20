using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ic_ef.Controllers
{
    public class RetailController : Controller
    {
        db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult reconciliation()
        {
            return View();
        }

        public ActionResult report()
        {
            return View();
        }
       public class date_result {
           public  string count { get; set; }
           public  string date { get; set; }

            }
        //get year to date report

   
        public JsonResult get_yTd_report (string start, string end)
        {

            using (var db = new db_a094d4_icdbEntities())
            {
                int avg_return = 0;
                int avg_alltime = 0;
                int avg_year = 0;
                int avg_year_counter = 0;
                int avg_month = 0;
                int avg_month_counter = 0;
                DateTime date_start;
                DateTime date_end;
               if (start == null && end == null)
                {
                    date_start = new DateTime(2015, 1, 1);
                    date_end = DateTime.Now.Date;
                }
               else
                {
                    date_start = DateTime.Parse(start);
                    date_end = DateTime.Parse(end);
                }

                var alltime_C2DDK_result = (from t in db.retail_report where t.name.Contains("MARDKC2D") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_i3DK_result = (from t in db.retail_report where t.name.Contains("MARDKi3") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_i5DK_result = (from t in db.retail_report where t.name.Contains("MARDKi5") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_i7DK_result = (from t in db.retail_report where t.name.Contains("MARDKi7") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_C2DLP_result = (from t in db.retail_report where t.name.Contains("MARLPC2D") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_i3LP_result = (from t in db.retail_report where t.name.Contains("MARLPi3") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_i5LP_result = (from t in db.retail_report where t.name.Contains("MARLPi5") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_i7LP_result = (from t in db.retail_report where t.name.Contains("MARLPi7") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_mbp_result = (from t in db.retail_report where t.name.Contains("MBP105-109") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_mb_result = (from t in db.retail_report where t.name.Contains("MB105-109") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_idk_result = (from t in db.retail_report where t.name.Contains("IDK105-109") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_c2ddk_result = (from t in db.retail_report where t.name.Contains("OEMDKC2D") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count(); 
                var alltime_oem_i3DK_result = (from t in db.retail_report where t.name.Contains("OEMDKi3") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_i5DK_result = (from t in db.retail_report where t.name.Contains("OEMDKi5") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_i7DK_result = (from t in db.retail_report where t.name.Contains("OEMDKi7") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_C2DLP_result = (from t in db.retail_report where t.name.Contains("OEMLPC2D") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_i3LP_result = (from t in db.retail_report where t.name.Contains("OEMLPi3") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_i5LP_result = (from t in db.retail_report where t.name.Contains("OEMLPi5") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();
                var alltime_oem_i7LP_result = (from t in db.retail_report where t.name.Contains("OEMLPi7") && t.type == "sale" && (t.date_sold >= date_start && t.date_sold <= date_end) select t).Count();



                var result = db.Database.SqlQuery<date_result>("SELECT DISTINCT(date(date_sold)) as date ,count( * ) AS count FROM retail_report where type = 'sale' GROUP BY date_sold ORDER BY date_sold ASC ").ToArray();
                var return_result = db.Database.SqlQuery<date_result>("SELECT DISTINCT(date(date_sold)) as date ,count( * ) AS count FROM retail_report where type = 'return' GROUP BY date_sold ORDER BY date_sold ASC ").ToArray();
                var month = db.Database.SqlQuery<date_result>("SELECT DISTINCT(date(date_sold)) as date ,count( * ) AS count FROM retail_report where type = 'sale' AND YEAR(date_sold) = YEAR(NOW()) AND MONTH(date_sold) = MONTH(NOW()) GROUP BY date_sold ORDER BY date_sold ASC ").ToArray();
                var year = db.Database.SqlQuery<date_result>("SELECT DISTINCT(date(date_sold)) as date ,count( * ) AS count FROM retail_report where type = 'sale' AND YEAR(date_sold) = YEAR(NOW()) GROUP BY date_sold ORDER BY date_sold ASC ").ToArray();
                //   var result2 = db.Database.SqlQuery<string>("SELECT DISTINCT(date(date_sold)) as date FROM retail_report where type = 'sale' GROUP BY date_sold ORDER BY date_sold ASC ").ToArray();
                foreach (var item in month)
                {
                    DateTime dt = Convert.ToDateTime(item.date);
                    item.date = dt.ToString("yyyy-MM-dd");
                }
                foreach (var item in return_result)
                {
                    DateTime dt = Convert.ToDateTime(item.date);
                    item.date = dt.ToString("yyyy-MM-dd");
                    avg_return += int.Parse(item.count);
                    
                }
                foreach (var item in year)
                {
                    DateTime dt = Convert.ToDateTime(item.date);
                    item.date = dt.ToString("yyyy-MM-dd");
                }
                foreach (var item in result)
                {

                    DateTime dt = Convert.ToDateTime(item.date);
                    item.date = dt.ToString("yyyy-MM-dd");
                    var test_year = DateTime.Now.Year.ToString();
                    var test_month = DateTime.Now.Month.ToString();
                    avg_alltime += int.Parse(item.count);
                    if (item.date.Contains(DateTime.Now.Year.ToString()))
                    {
                        avg_year += int.Parse(item.count);
                        avg_year_counter++;
                    }
                    if (item.date.Contains(DateTime.Now.Year.ToString() +"-" +DateTime.Now.Month.ToString()))
                    {
                        avg_month += int.Parse(item.count);
                        avg_month_counter++;
                    }
                    
                  
                }

                avg_month = (avg_month / avg_month_counter);
                avg_alltime = (avg_alltime / result.Length);
                avg_year = (avg_year / avg_year_counter);
                avg_return = (avg_return / return_result.Length);
                return Json(new { oem_C2Dlp_all = alltime_oem_C2DLP_result, oem_i3lp_all = alltime_oem_i3LP_result, oem_i5lp_all = alltime_oem_i5LP_result, oem_i7lp_all = alltime_oem_i7LP_result, oem_C2DDK_all = alltime_oem_c2ddk_result, oem_i3dk_all = alltime_oem_i3DK_result, oem_i5dk_all = alltime_oem_i5DK_result, oem_i7dk_all = alltime_oem_i7DK_result, C2Dlp_all = alltime_C2DLP_result,i3lp_all = alltime_i3LP_result, i5lp_all = alltime_i5LP_result, i7lp_all = alltime_i7LP_result, C2DDK_all = alltime_C2DDK_result,i3dk_all = alltime_i3DK_result,i5dk_all = alltime_i5DK_result,i7dk_all = alltime_i7DK_result,return_result= return_result,avg_return =  avg_return,alltime_result = result, month_result = month,year_result=year,avg_month = avg_month,avg_alltime = avg_alltime,avg_year=avg_year, alltime_mbp = alltime_mbp_result, alltime_mb = alltime_mb_result, alltime_idk =  alltime_idk_result },JsonRequestBehavior.AllowGet);
            }
                 

           
        }

        public JsonResult get_request (string request)
        {
            var ytd = DateTime.Today.AddDays(-1);
             switch (request)
            {
                case "return":

                    var return_ytd = (from t in db.retail_report where t.date_sold == ytd && t.type == "return" select t).ToArray();

                    return Json(return_ytd,JsonRequestBehavior.AllowGet);
                case "sold":
                    var sold_ytd = (from t in db.retail_report where t.date_sold == ytd && t.type == "sale" select t).ToArray();
                    return Json(sold_ytd, JsonRequestBehavior.AllowGet);

                case "current":

                    var in_store = (from t in db.pull_log where t.action == "pulled" && t.channel.Contains("retail") select new { t.ictag,t.sku}).Distinct().ToArray();
                    

                    return Json(in_store, JsonRequestBehavior.AllowGet);



                default:
                    return Json(JsonRequestBehavior.AllowGet);
            }




            
        }
        public JsonResult current_inv_retail()
        {
            var total = (from t in db.retail_report select t).Count();
            var ytd = DateTime.Today.AddDays(-1);
            var in_store = (from t in db.pull_log where t.action == "pulled" && t.channel.Contains("retail") select t.ictag).ToArray();
            
            string[] distinct_in_store = in_store.Distinct().ToArray();
            var sold_ytd = (from t in db.retail_report where t.date_sold == ytd && t.type =="sale" select t).ToArray();
            var return_ytd = (from t in db.retail_report where t.date_sold == ytd && t.type == "return" select t).ToArray();
            var distinct_in_store_num = distinct_in_store.Length;
            var sold_ytd_num = sold_ytd.Length;
            var return_ytd_num = return_ytd.Length;
            return Json(new { in_store = distinct_in_store_num, sold_ytd = sold_ytd_num, overall = total, return_ytd = return_ytd_num },JsonRequestBehavior.AllowGet);
        }
        //compare both array and return the result that was and wasnt found 
        public JsonResult process_array (string[] input)
        { 
            //instore asset array
            var in_store = (from t in db.pull_log where t.action == "pulled" && t.channel.Contains("retail") select t.ictag).ToArray();
            var in_store_count = in_store.Distinct().ToArray().Length;
            var in_stock = in_store;
            string[] same = input.Intersect(in_store).ToArray();

            string valid_html = "";
            string invalid_html = "";
           foreach (var item in same)
            {
                valid_html += "<tr><td>" + item + "</td><td class='positive'><i class='icon check'></i>Valid</td></tr>";
                in_store = in_store.Where(val => val != item).ToArray();
            }
           foreach (var item in in_store.Distinct().ToArray())
            {
                invalid_html += "<tr><td>" + item + "</td><td class='negative'><i class='icon warning'></i>Unresloved</td></tr>";
            }


            //string[] diff = input.Union(in_store).Except(same).Distinct().ToArray();
           
            var input_count = input.Length;
            var same_count = same.Length;
            var diff_count = in_store.Distinct().ToArray().Length;



            var construct_table = "<table id='result_table' class='ui celled table'><thead><tr><th>Asset In Stock</th><th>Result</th></tr></thead><tbody>" + valid_html + invalid_html+ "</tbody></table>";


            return Json(new { table_html = construct_table,in_stock = in_stock, found = same, input_count = input_count, in_store_count = in_store_count, same_count = same_count, diff_count = diff_count, input = input, unresolved = in_store } ,JsonRequestBehavior.AllowGet);
        }
    }
}