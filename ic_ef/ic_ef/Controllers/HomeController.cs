﻿using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using ic_ef.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ic_ef.Core;
using Rotativa;
using System.Text;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using System.Reflection;
using Magento.RestApi;

namespace ic_ef.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext context;

        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        private ic_databaseEntities2 db = new ic_databaseEntities2();

        [AllowAnonymous]
        public async System.Threading.Tasks.Task<ActionResult> Welcome()
        {
   //         var client = new MagentoApi()
   //.Initialize("http://www.dev.interconnection.org/nonprofit/", "e96202a50cf1b8aecb0e7cfba5d63efa", "bb6f8568b476e030c27416751e9ce14a")
   //.AuthenticateAdmin("cyeung", "chisan0501");
   //         //var response = await client.GetProductBySku("ICRL1194");
         
   //         var product = new Magento.RestApi.Models.Product();
          
   //         product.sku = "Test123";
   //         product.type_id = "simple";
   //         product.attribute_set_id = 4;
   //         product.name = "C# test product";
   //         product.status = Magento.RestApi.Models.ProductStatus.Enabled;
   //         product.short_description = "this is a test";
   //         product.description = "full test";
            
   //         product.weight = 13.00;
   //         product.visibility = Magento.RestApi.Models.ProductVisibility.CatalogSearch;
   //         //assign product to nonprofit store
   //         var response2 = await client.AssignWebsiteToProduct(1224, 5);

   //         var repo = await client.CreateNewProduct(product);

   //         var response = client.GetProductBySku("ICM1000").Result;

            ViewBag.Title = "InterConnection.org";
            return View();
        }
        public ActionResult customer_support ()
        { 
            return View();
        }

        public ActionResult advance_search()
        {

            return View();
        }

        [HttpPost]
        public ActionResult advance_search(string cpu)
        {

            return View();
        }
        public ActionResult PrintIndex()
        {
            return new ActionAsPdf("multiple_barcode", new { name = "Home" }) { FileName = "Test.pdf" };
        }

       
        public ActionResult search_result (string search_id)
        {
            
            string serial = "";
            int asset = int.Parse(search_id);
            //search disvoery
            var discovery = from m in db.discovery where m.ictag == asset select m;
            foreach ( var item in discovery )
            {
                serial = item.serial;
                ViewBag.imaging = null;
                ViewBag.discovery = null;
                ViewBag.refrubish = null;
            }
            if (serial == "")
            {

                ViewBag.Status = "Asset Not Found";
                return View();
            }
            //search refrub
            var refrub = from m in db.rediscovery where m.serial == serial select m;
            //search imaging
            var imaging = from m in db.production_log where m.serial == serial select m;
            ViewBag.imaging = imaging;
            ViewBag.discovery = discovery;
            ViewBag.refrubish = refrub;

            ViewBag.id = search_id;
            return View();
        }


        [HttpPost]
        public ActionResult barcode (FormCollection f,DymoViewModel model)
        {
            int update_counter = 0;
            
            //if (f.AllKeys.Contains("txtcode"))
            //{
               
            //    var vCode = f["txtcode"];
            //    string barCode = BarCodeToHTML.get39(vCode, 2, 20);
            //    ViewBag.htmlBarcode = barCode;
            //    ViewBag.vCode = vCode;
            //}
            
            
                var db_counter = (from m in db.asset_tag_counter where m.Company == "interconnection" select m.count).SingleOrDefault();

               
                int i = int.Parse(f["txtcode_num"]);
               
                model.barcode_arr2 = new string[i];
                model.vcode2 = new string[i];
                for (int start = 0; start < i; start++)
                {
                    db_counter = db_counter += 1;
                    var vCode2 = db_counter.ToString();
                    update_counter = int.Parse(vCode2);
                    string barCode2 = BarCodeToHTML.get39(vCode2, 2, 20);
                    model.barcode_arr2[start] = barCode2;
                    //     ViewBag.htmlBarcode = barCode;
                    string html = "<p style=\"page-break-after:always; text-align:center; \"><div style=\"clear: both\" id=\"div_print\"><img id=\"img\"src=\"/Barcode.ashx?m=1&h=60&vCode=" + vCode2 + "\" alt=\"" + vCode2 + "\" /> </div></p>";
                    model.vcode2[start] = html;


                }
                
                ViewBag.test = model.vcode2;
                ViewBag.vCode = "Default";

                //update the barcode counter 
                var updateQuery = new asset_tag_counter() { Company = "interconnection" ,count= update_counter };
                db.asset_tag_counter.Attach(updateQuery);
                db.Entry(updateQuery).Property(x => x.count).IsModified = true;
                db.SaveChanges();
            



            return View();

        }
        public ActionResult barcode()
        {
            ViewBag.Message = "MVC WEB BARCODE!!";
            string vCode = "Default";
            string barCode = BarCodeToHTML.get39(vCode, 2, 20);
            ViewBag.htmlBarcode = barCode;

            ViewBag.vCode = vCode;
            return View();

          
        }
        public ActionResult dashboard()
        {
            ViewBag.something = "1";
            return View();
        }
        public DotNet.Highcharts.Highcharts discover_weekly_graph(graphViewModel model, List<DateTime> allDates)
        {

            Highcharts dis_weekly_chart = new Highcharts("dis_weekly_chart")
                .InitChart(new Chart
                {
                    Width = 500,
                    Height = 350,
                    Type = ChartTypes.Column,
                    Margin = new[] { 75 },
                    Options3d = new ChartOptions3d
                    {
                        Enabled = true,
                        Alpha = 15,
                        Beta = 15,
                        Depth = 50,
                        ViewDistance = 25
                    }
                })
             .SetXAxis(new XAxis { Categories = new[] { allDates[0].ToString("yyyy-MM-dd"), allDates[1].ToString("yyyy-MM-dd"), allDates[2].ToString("yyyy-MM-dd"), allDates[3].ToString("yyyy-MM-dd"), allDates[4].ToString("yyyy-MM-dd"), allDates[5].ToString("yyyy-MM-dd"), allDates[6].ToString("yyyy-MM-dd") } })
                  .SetTitle(new Title { Text = "Discovery Weekly Graph" })
                   .SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Depth = 25 } })
                .SetLegend(new Legend { Enabled = false })
                .SetSeries(new Series { Data = new Data(new object[] { model.dis_day1, model.dis_day2, model.dis_day3, model.dis_day4, model.dis_day5, model.dis_day6, model.dis_day7 }) });

            return dis_weekly_chart;
        }
        public DotNet.Highcharts.Highcharts lastweek_graph(graphViewModel model, List<DateTime> allDates)
        {
            
            Highcharts weekly_chart = new Highcharts("weekly_chart")
                .InitChart(new Chart
                {
                    Width = 500,Height=350,
                    Type = ChartTypes.Column,
                    Margin = new[] { 75 },
                    Options3d = new ChartOptions3d
                    {
                        Enabled = true,
                        Alpha = 15,
                        Beta = 15,
                        Depth = 50,
                        ViewDistance = 25
                    }
                })
             .SetXAxis(new XAxis { Categories = new[] { allDates[0].ToString("yyyy-MM-dd"), allDates[1].ToString("yyyy-MM-dd"), allDates[2].ToString("yyyy-MM-dd"), allDates[3].ToString("yyyy-MM-dd"), allDates[4].ToString("yyyy-MM-dd"), allDates[5].ToString("yyyy-MM-dd"), allDates[6].ToString("yyyy-MM-dd") } })
                  .SetTitle(new Title { Text = "Imaging Weekly Graph" })
                   .SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Depth = 25 } })
                .SetLegend(new Legend { Enabled = false })
                .SetSeries(new Series { Data = new Data(new object[] { model.day1, model.day2,model.day3,model.day4,model.day5,model.day6,model.day7 }) });
          
            return weekly_chart;
        }

        public DotNet.Highcharts.Highcharts set_graph (graphViewModel model)
        {

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
             .SetTitle(new Title { Text = "Production Graph" })
              .SetSubtitle(new Subtitle
              {
                  Text = "Date : " +  DateTime.Today,
                  X = -20
              })
  .SetXAxis(new XAxis
  {
      Categories = new[] { "6am", "7am", "8am", "9am", "10am", "11am", "12pm", "1pm", "2pm", "3pm", "4pm", "5pm", "6pm" }
  })

              .SetSeries(new[]
              {
            new Series
                 {Name = "Laptop",
                     Data = new Data(new object[] { model.laptop_six_am, model.laptop_six_am, model.laptop_seven_am, model.laptop_eight_am, model.laptop_nine_am, model.laptop_ten_am, model.laptop_eleven_am, model.laptop_twelve_pm, model.laptop_one_pm, model.laptop_two_pm, model.laptop_three_pm, model.laptop_four_pm, model.laptop_five_pm })
                 }
                 ,  new Series
                 {Name = "Desktop",
                     Data = new Data(new object[] { model.desktop_six_am, model.desktop_six_am, model.desktop_seven_am, model.desktop_eight_am, model.desktop_nine_am, model.desktop_ten_am, model.desktop_eleven_am, model.desktop_twelve_pm, model.desktop_one_pm, model.desktop_two_pm, model.desktop_three_pm, model.desktop_four_pm, model.desktop_five_pm })}

         }


          );
            chart.InitChart(new Chart { Width = 800, Height = 250 });

            return chart;
        }
     public void process_array (DateTime[] t, DateTime[] t2, graphViewModel model,List<DateTime>allDates)
        {

        
            //station 1
            foreach (var time in t)
            {
                switch (time.ToString("%h"))
                {
                    case "6":
                        model.laptop_six_am += 1;
                        break;
                    case "7":
                        model.laptop_seven_am += 1;
                        break;
                    case "8":
                        model.laptop_eight_am += 1;
                        break;
                    case "9":
                        model.laptop_nine_am += 1;
                        break;
                    case "10":
                        model.laptop_ten_am += 1;
                        break;
                    case "11":
                        model.laptop_eleven_am += 1;
                        break;
                    case "12":
                        model.laptop_twelve_pm += 1;
                        break;
                    case "13":
                        model.laptop_one_pm += 1;
                        break;
                    case "14":
                        model.laptop_two_pm += 1;
                        break;
                    case "15":
                        model.laptop_three_pm += 1;
                        break;
                    case "16":
                        model.laptop_four_pm += 1;
                        break;
                    case "17":
                        model.laptop_five_pm += 1;
                        break;

                }
            }

            //stations 2
            foreach (var time in t2)
            {
                switch (time.ToString("%h"))
                {
                    case "6":
                        model.desktop_six_am += 1;
                        break;
                    case "7":
                        model.desktop_seven_am += 1;
                        break;
                    case "8":
                        model.desktop_eight_am += 1;
                        break;
                    case "9":
                        model.desktop_nine_am += 1;
                        break;
                    case "10":
                        model.desktop_ten_am += 1;
                        break;
                    case "11":
                        model.desktop_eleven_am += 1;
                        break;
                    case "12":
                        model.desktop_twelve_pm += 1;
                        break;
                    case "13":
                        model.desktop_one_pm += 1;
                        break;
                    case "14":
                        model.desktop_two_pm += 1;
                        break;
                    case "15":
                        model.desktop_three_pm += 1;
                        break;
                    case "16":
                        model.desktop_four_pm += 1;
                        break;
                    case "17":
                        model.desktop_five_pm += 1;
                        break;

                }
            }
            //for pass 7 days
            var dateCriteria = DateTime.Now.Date.AddDays(-7);
            //start the query
            var lastweek = (from lastt in db.production_log
                            where (lastt.time >=  dateCriteria)
                            select lastt.time).ToArray();
            
            foreach (var d in lastweek)
            {
                if (d.Date == allDates[0])
                {
                    model.day1 += 1;
                }
                else if (d.Date == allDates[1])
                {
                    model.day2 += 1;
                }
                else if (d.Date == allDates[2])
                {
                    model.day3 += 1;
                }
                else if (d.Date == allDates[3])
                {
                    model.day4 += 1;
                }
                else if (d.Date == allDates[4])
                {
                    model.day5 += 1;
                }
                else if (d.Date == allDates[5])
                {
                    model.day6 += 1;
                }
                else if (d.Date == allDates[6])
                {
                    model.day7 += 1;
                }
                else if (d.Date == allDates[7])
                {
                    model.day8 += 1;
                }

            }

            var dis_lastweek = (from lastt in db.discovery
                                where (lastt.time >= dateCriteria)
                                select lastt.time).ToArray();
          foreach (DateTime d in dis_lastweek)
            {
                if (d.Date == allDates[0])
                {
                    model.dis_day1 += 1;
                }
                else if (d.Date == allDates[1])
                {
                    model.dis_day2 += 1;
                }
                else if (d.Date == allDates[2])
                {
                    model.dis_day3 += 1;
                }
                else if (d.Date == allDates[3])
                {
                    model.dis_day4 += 1;
                }
                else if (d.Date == allDates[4])
                {
                    model.dis_day5 += 1;
                }
                else if (d.Date == allDates[5])
                {
                    model.dis_day6 += 1;
                }
                else if (d.Date == allDates[6])
                {
                    model.dis_day7 += 1;
                }
                else if (d.Date == allDates[7])
                {
                    model.dis_day8 += 1;
                }
            }

        }
        public ActionResult Index_nonadmin()
        {
            return View();
        }



        public void ExportCSV<T>(IEnumerable<T> list, string strFileName)
        {



            //IEnumerable<discovery> result = from p in db.discovery
            //                            select p;

            Response.AppendHeader("content-disposition", "attachment;filename=" + strFileName);
            Response.ContentType = "application/unkown";
            Response.ContentEncoding = Encoding.UTF8;

            GenerateCSV(list);


        }
        private void GenerateCSV<T>(IEnumerable<T> list)
        {
            Type t = typeof(T);

            //write data to front end
            StreamWriter sw = new StreamWriter(Response.OutputStream, Encoding.Default);

            object o = Activator.CreateInstance(t);

            PropertyInfo[] props = o.GetType().GetProperties();

            //output the header
            foreach (PropertyInfo pi in props)
            {
                sw.Write(pi.Name.ToUpper() + ",");
            }
            sw.WriteLine();

            //write out the list
            foreach (T item in list)
            {
                foreach (PropertyInfo pi in props)
                {

                    string whatToWrite =
                 Convert.ToString(item.GetType().GetProperty(pi.Name).GetValue(item, null)).Replace(',', ' ') + ',';
                    sw.Write(whatToWrite);

                }
                sw.WriteLine();

            }

            sw.Close();

        }

        public ActionResult export_coa_csv (string id)
        {
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + "_" + id + "_coa.csv";
            var result = from c in db.coas
                         where c.location == id && c.Recipient_Type != "USED" 

                         select c;


            ExportCSV(result, file_name);
            return new EmptyResult();
        }

        //get
        public ActionResult Index(graphViewModel model, string fromDate, string toDate)
        {
            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var s = UserManager.GetRoles(user.GetUserId());

            if (s[0].ToString() == "Employee")
            {
                return RedirectToAction("Index_nonadmin", "Home");
            }
            var dateCriteria = DateTime.Now.Date.AddDays(-7);
            //save all dates string into this array
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = dateCriteria; date <= DateTime.Now; date = date.AddDays(1))
                allDates.Add(date);

            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var coa_arr = (from m in db.coas where m.Recipient_Type != "USED" select m).ToArray();
            var s1_windows = (from m in coa_arr where m.location == "wcoa_s1" select m).ToArray();
            var s1_office = (from n in coa_arr where n.location == "ocoa_s1" select n).ToArray();
            var s2_windows = (from m in coa_arr where m.location == "wcoa_s2" select m).ToArray();
            var s2_office = (from n in coa_arr where n.location == "ocoa_s2" select n).ToArray();
            int w_g360_count_s1 = 0;
            int w_ts_count_s1 = 0;
            int w_mar_count_s1 = 0;
            int o_g360_count_s1 = 0;
            int o_ts_count_s1 = 0;
            int o_mar_count_s1 = 0;
            int w_g360_count_s2 = 0;
            int w_ts_count_s2 = 0;
            int w_mar_count_s2 = 0;
            int o_g360_count_s2 = 0;
            int o_ts_count_s2 = 0;
            int o_mar_count_s2 = 0;
            foreach (var coa in s1_windows)
            {
                switch (coa.Recipient_Organization_Name)
                {
                    case "Good360":
                        w_g360_count_s1 += 1;
                        break;
                    case "TechSoup.org":
                        w_ts_count_s1 += 1;
                        break;
                    case "Interconnection":
                        w_mar_count_s1 += 1;
                        break;
                }
            }
            foreach (var coa in s1_office)
            {
                switch (coa.Recipient_Organization_Name)
                {
                    case "Good360":
                        o_g360_count_s1 += 1;
                        break;
                    case "TechSoup.org":
                        o_ts_count_s1 += 1;
                        break;
                    case "Interconnection":
                        o_mar_count_s1 += 1;
                        break;
                }
            }
                foreach (var coa2 in s2_windows)
                {
                    switch (coa2.Recipient_Organization_Name)
                    {
                        case "Good360":
                            w_g360_count_s2 += 1;
                            break;
                        case "TechSoup.org":
                            w_ts_count_s2 += 1;
                            break;
                        case "Interconnection":
                            w_mar_count_s2 += 1;
                            break;
                    }
                }
                foreach (var coa2 in s2_office)
                {
                    switch (coa2.Recipient_Organization_Name)
                    {
                        case "Good360":
                            o_g360_count_s2 += 1;
                            break;
                        case "TechSoup.org":
                            o_ts_count_s2 += 1;
                            break;
                        case "Interconnection":
                            o_mar_count_s2 += 1;
                            break;
                    }
                }
            ViewBag.w_g360_count_s1 = w_g360_count_s1;
            ViewBag.w_g360_count_s2 = w_g360_count_s2;
            ViewBag.o_g360_count_s1 = o_g360_count_s1;
            ViewBag.o_g360_count_s2 = o_g360_count_s2;
            ViewBag.w_ts_count_s1 = w_ts_count_s1;
            ViewBag.w_ts_count_s2 = w_ts_count_s2;
            ViewBag.o_ts_count_s1 = o_ts_count_s1;
            ViewBag.o_ts_count_s2 = o_ts_count_s2;
            ViewBag.w_mar_count_s1 = w_mar_count_s1;
            ViewBag.w_mar_count_s2 = w_mar_count_s2;
            ViewBag.o_mar_count_s1 = o_mar_count_s1;
            ViewBag.o_mar_count_s2 = o_mar_count_s2;

            if (ViewBag.fromDate != null)
                {
                    DateTime end;
                    DateTime begin = DateTime.Parse(fromDate);
                    if (toDate == "")
                    {
                        end = begin.AddDays(1).AddTicks(-1);
                    }
                    else
                    {
                        end = DateTime.Parse(toDate);
                    }

                    var fromandto = (from m in db.production_log where m.time > begin && m.time < end && m.location.Contains("Station 1") select m.time).ToArray();

                    var fromandto2 = (from m in db.production_log where m.time > begin && m.time < end && m.location.Contains("Station 2") select m.time).ToArray();

                    process_array(fromandto, fromandto2, model, allDates);
                    int count_stations = fromandto.Count() + fromandto2.Count();
                    ViewBag.fromandto_total = "[" + count_stations + "] Station 1: [" +  fromandto.Count() + "] Station 2: [" + fromandto2.Count() + "]";
                    ViewBag.station1 = fromandto.Count();
                    ViewBag.station2 = fromandto2.Count();
                    ViewBag.total = count_stations;
                }
                else
                {
                    DateTime startDateTime = DateTime.Today; //Today at 00:00:00
                    DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

                    var pallet = (from t in db.production_log
                                  where (t.time >= startDateTime && t.time <= endDateTime) && t.location.Contains("Station 2")
                                  select t.time).ToArray();


                    //int result = pallet.Count();
                    var pallet2 = (from t in db.production_log
                                   where (t.time >= startDateTime && t.time <= endDateTime) && t.location.Contains("Station 1")
                                   select t.time).ToArray();

                    process_array(pallet2, pallet, model, allDates);

                    ViewBag.today_total = "Station 1: [" + pallet2.Count() + "] Station 2: [" + pallet.Count() + "]";
                    ViewBag.station1 = pallet2.Count();
                    ViewBag.station2 = pallet.Count();
                    ViewBag.total = pallet.Count() + pallet2.Count();


                }
                Highcharts discovery_data = discover_weekly_graph(model, allDates);
                Highcharts chartColumn = lastweek_graph(model, allDates);
                ChartsModel charts = new ChartsModel();
                charts.Chart3 = chartColumn;
                charts.Chart2 = discovery_data;
                ViewBag.weekGrahp = lastweek_graph(model, allDates);
                ViewBag.dis_weekGrahp = discover_weekly_graph(model, allDates);
                ViewBag.Model = set_graph(model);

                return View(charts);
            }
        
        public class Entry
        {
            public int year { get; set; }
            public int value { get; set; }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public class ChartsModel
        {
            public Highcharts Chart1 { get; set; }
            public Highcharts Chart2 { get; set; }
            public Highcharts Chart3 { get; set; }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}