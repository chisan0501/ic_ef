using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ic_ef.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient.Memcached;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text;
using System.Reflection;

namespace ic_ef.Controllers
{

    [Authorize]
    public class ManageController : Controller
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;

        public ManageController()
        {
            context = new ApplicationDbContext();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = User.Identity;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var s = UserManager.GetRoles(user.GetUserId());

            ViewBag.User_role = s[0].ToString();

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }
     
        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }


 

      //  GET: /Manage/palletmanager
      // [HttpPost]
        public ActionResult inventoryManage()
        {
            db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();

            var data = (from p in db.pallet_master
                        select new Models.inventoryManageViewModel
                        {
                            pallet = p.pallet_id,

                        });

            SelectList list = new SelectList(data, "pallet", "pallet");
            ViewBag.Roles = list;
            ViewBag.data = data;



            //Loop through the forms
            for (int i = 0; i <= Request.Form.Count; i++)
            {
                var ictags = Request.Form["ictag[" + i + "]"];
               
                var pallet_name = Request.Form["pallet_name[" + i + "]"];

            var note = Request.Form["note[" + i + "]"];

                if ((ictags != null) && (pallet_name != null))
                {
                    db.pallet.Add(new pallet { ictags = int.Parse(ictags), pallet_name = pallet_name , note = note});
                    db.SaveChanges();
                }
            }

            return View();
        }

   

        public void ExportCSV<T>(IEnumerable<T> list, string strFileName )
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
        // GET: /Manage/report
        public ActionResult report(string fromDate, string toDate, string imaging_csv,string refrub_csv,string discover_csv)
        {
            int station_1 = 0;
            int station_2= 0;
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
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

                var discover = (from m in db.discovery where m.time > begin && m.time < end select m);
                var refrub = (from r in db.rediscovery where r.time > begin && r.time < end select r) ;
                var imaging = (from i in db.production_log where i.time > begin && i.time < end select i);
               
                foreach (var b in imaging)
                {
                    if (b.location == "Station 1")
                    {
                        station_1 += 1;
                    }
                    else if (b.location == "Station 2")
                    {
                        station_2 += 1;
                    }
               
                }
                ViewBag.s1 = station_1;
                ViewBag.s2 = station_2;
                ViewBag.discover_Count = discover.Count();
                ViewBag.refrub_count = refrub.Count();
                ViewBag.imaging_count = imaging.Count();
                if (imaging_csv !=null)
                {
                    string strFileName = today + "_imaging.csv";
                    ExportCSV(imaging, strFileName);
                    return new EmptyResult();
                   
                }
                if (discover_csv != null)
                {
                    string strFileName = today + "_discovery.csv";
                    ExportCSV(discover, strFileName);
                    return new EmptyResult();

                }
                if (refrub_csv != null)
                {
                    string strFileName = today + "_refrubisher.csv";
                    ExportCSV(refrub, strFileName);
                    return new EmptyResult();

                }

            }

           
            return View();

        }
        private db_a094d4_icdbEntities db = new db_a094d4_icdbEntities();
        private string countpallet()
        {
            string pallet_total = "";

            List<production_log> classList = db.production_log.ToList();
            int count = classList.Count();

            return pallet_total;
        }

       
        
         public JsonResult GET_counter(string product)
        {
            var p_list = new List<string>();
            var product_list = (from m in db.asset_tag_counter where m.Company == product  select m.count).SingleOrDefault();

           
                p_list.Add("Current Count : " + product_list.ToString());
            

            return Json(p_list, JsonRequestBehavior.AllowGet);
        }

        private static DataTable ProcessCSV(string fileName)
        {
            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray;
            DataTable dt = new DataTable();
            DataRow row;
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            StreamReader sr = new StreamReader(fileName);

            //Read the first line and split the string at , with our regular expression in to an array
            line = sr.ReadLine();
            strArray = r.Split(line);

            //For each item in the new split array, dynamically builds our Data columns. Save us having to worry about it.
            Array.ForEach(strArray, s => dt.Columns.Add(new DataColumn()));

            //Read each line in the CVS file until it’s empty
            while ((line = sr.ReadLine()) != null)
            {
                row = dt.NewRow();

                //add our current value to our data row
                row.ItemArray = r.Split(line);
                dt.Rows.Add(row);
            }

            //Tidy Streameader up
            sr.Dispose();

            //return a the new DataTable
            return dt;

        }

        public ActionResult AdvanceSearch()
        {
            return View();
        }

        public PartialViewResult SearchPeople(string keyword)
        {
            System.Threading.Thread.Sleep(2000);
            var data = db.users.Where(f =>
            f.user_name.StartsWith(keyword)).ToList();
            return PartialView(data);
        }

        [HttpPost]
        public ActionResult AdvanceSearch (string search_cpu)
        {
            var cpus = (from m in db.discovery where m.cpu.Contains(search_cpu)
                         select m).ToList();

            if (!String.IsNullOrEmpty(search_cpu))  
            {
                
            }
            return View();
        }

        [HttpPost]
        public ActionResult csv (HttpPostedFileBase FileUpload, string station)
        {
            
            string path = "";
            DataTable dt = new DataTable();
            
            //check we have a file
            if (FileUpload.ContentLength > 0)
            {

               
                //Workout our file path
                string fileName = Path.GetFileName(FileUpload.FileName);
                 path= Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

  


                //Try and upload
                try
                {
                    FileUpload.SaveAs(path);
                    //Process the CSV file and capture the results to our DataTable holder

                    List<string> temp_line = new List<string>();
                    using (StringReader reader = new StringReader(System.IO.File.ReadAllText(path)))
                    {
                        string line = null;
                        while ((line = reader.ReadLine()) != null)
                            if (!temp_line.Contains(line))
                                temp_line.Add(line);
                    }

                    string check = temp_line[2].ToString();
                    if (check.Contains("Office") && !station.ToString().Contains("ocoa")) {
                        TempData["message"] = "Error ---CSV might not match COA type----";
                        return RedirectToAction("admin", "Manage");
                      
                    }
                    else if (check.Contains("Windows") && !station.ToString().Contains("wcoa"))
                    {
                        TempData["message"] = "Error ---CSV might not match COA type----";
                        return RedirectToAction("admin", "Manage");
                    }
                    using (StreamWriter writer = new StreamWriter(System.IO.File.Open(path, FileMode.Create)))
                        foreach (string value in temp_line)
                            writer.WriteLine(value);





                    dt = ProcessCSV(path);
                    foreach (DataRow r in dt.Rows)
                    {
                        var updateQuery = new coas();
                        updateQuery.Request_ID = int.Parse(r[0].ToString());
                        updateQuery.COA_ID = r[1].ToString();
                        updateQuery.Product_Name = r[2].ToString();
                        updateQuery.Pre_existing_COA_ID = r[3].ToString();
                        updateQuery.License_Type = r[4].ToString();
                        updateQuery.PK = r[5].ToString();
                        updateQuery.PDF_language = r[6].ToString();
                        updateQuery.Recipient_Organization_Name = r[7].ToString();
                        updateQuery.Recipient_City = r[8].ToString();
                        updateQuery.Recipient_Country___Region = r[9].ToString();
                        updateQuery.Recipient_Type = r[10].ToString();
                        updateQuery.location = station.ToString();

                        db.coas.Add(updateQuery);

                    }
                    db.SaveChanges();
                    dt.Dispose();
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                catch (Exception ex)
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    //Catch errors
                    TempData["Feedback"] = ex.ToString();
                   
                }
                
            }
            else
            {
                //Catch errors
                TempData["Feedback"] = "Please select a file";
            }

             
         
                
                   
                  
           



            //Tidy up
          
            return RedirectToAction("admin");
        }
       

     
        [HttpPost]
        public ActionResult admin(int? counter_value)

        {
         
            var channel_list = db.asset_tag_counter
                                .Select(p => p.Company)
                                .ToList();
            SelectList list = new SelectList(channel_list);
            ViewBag.myList = list;

            var updateQuery = new asset_tag_counter() { Company = "interconnection", count = counter_value };
            db.asset_tag_counter.Attach(updateQuery);
            db.Entry(updateQuery).Property(x => x.count).IsModified = true;
            db.SaveChanges();
            ModelState.Clear();

           
            return View("admin");
            
        }
        [HttpPost]
        public ActionResult create(IdentityRole Role)
        {

            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("admin");
        }

        public ActionResult create()
        {
            return View();
        }
        public ActionResult sucess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var account = new AccountController();
            if (user != null)
            {
                UserManager.AddToRole(user.Id, RoleName);

                ViewBag.ResultMessage = "Role created successfully !";
                return View("sucess");
            }

           return RedirectToAction("admin"); 

            
        }


        [HttpPost]
        public ActionResult Search_COA(string coa, string dash_used)
        {
            if (dash_used != "on")
            {
                string temp_new_coa = coa.Insert(5, "-");
                temp_new_coa = temp_new_coa.Insert(9, "-");
                temp_new_coa = temp_new_coa.Insert(13, "-");
                coa = temp_new_coa;
            }
            var newentry = new coas_history();
            var modify = (from m in db.coas where m.COA_ID == coa select m);
            foreach (var m in modify)
            {
                newentry.COA_ID = m.COA_ID;
                newentry.License_Type = m.License_Type;
                newentry.location = m.location;
                newentry.PDF_language = m.PDF_language;
                newentry.PK = m.PK;
                newentry.Pre_existing_COA_ID = m.Pre_existing_COA_ID;
                newentry.Product_Name = m.Product_Name;
                newentry.Recipient_City = m.Recipient_City;
                newentry.Recipient_Country___Region = m.Recipient_Country___Region;
                newentry.Recipient_Organization_Name = m.Recipient_Organization_Name;
                newentry.Recipient_Type = m.Recipient_Type;
                newentry.Request_ID = m.Request_ID;
                newentry.row_id = m.row_id;

            }
            
            //modify.Recipient_Type = "USED";

            // System.Data.Entity.EntityState.Modified;
            
            db.coas_history.Add(newentry);
          //  db.Entry(modify).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            

            coas deleteme = db.coas.Where(u => u.COA_ID.Equals(coa)).FirstOrDefault();
            if (deleteme != null)
            {
                db.coas.Remove(deleteme);
                db.SaveChanges();
                
            }
            db.Dispose();


            ModelState.Clear();
            return RedirectToAction("admin");
        }

        public ActionResult admin_dashboard ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult reuse_COA(string new_coa, string dash)
        {
            
            if (dash != "on")
            {
                string temp_new_coa = new_coa.Insert(5, "-");
                temp_new_coa = temp_new_coa.Insert(9, "-");
                temp_new_coa = temp_new_coa.Insert(13, "-");
                new_coa = temp_new_coa;
            }
            // System.Data.Entity.EntityState.Modified;
            var newentry = new coas() ;
            var modify = (from m in db.coas_history where  m.COA_ID == new_coa select m);
           
            foreach (var m in modify)
            {
                newentry.COA_ID = m.COA_ID;
                newentry.License_Type = m.License_Type;
                newentry.location = m.location;
                newentry.PDF_language = m.PDF_language;
                newentry.PK = m.PK;
                newentry.Pre_existing_COA_ID = m.Pre_existing_COA_ID;
                newentry.Product_Name = m.Product_Name;
                newentry.Recipient_City = m.Recipient_City;
                newentry.Recipient_Country___Region = m.Recipient_Country___Region;
                newentry.Recipient_Organization_Name = m.Recipient_Organization_Name;
                newentry.Recipient_Type = m.Recipient_Type;
                newentry.Request_ID = m.Request_ID;
                newentry.row_id = m.row_id;
              
            }
            //modify = db.coas_history.Where(u => u.COA_ID.Equals(new_coa)).FirstOrDefault();
            //  coas modify = db.coas.Where(u => u.COA_ID.Equals(new_coa)).FirstOrDefault();
            // modify.Recipient_Type = "NOT USED";
            db.coas.Add(newentry);
           // db.coas.Attach(modify);
        //    db.Entry(modify).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.Dispose();
            ModelState.Clear();
            return RedirectToAction("admin");
        }
        [HttpPost]
        //get /magnage/single_barcode
        public ActionResult single_barcode (FormCollection f)
        {
            
            var vCode = f["txtcode"];
            TempData["vcode"] = vCode;
            
            return RedirectToAction("admin");
        }

    


        // GET: /Manage/admin
        public ActionResult admin(string windows_coa, string office_coa)
        {
            if(windows_coa != null)
            {
                
                var coa_result = (from m in db.production_log where m.wcoa == windows_coa select m);
                foreach (var item in coa_result)
                {
                    ViewBag.wcoa = item.wcoa;
                    ViewBag.ocoa = item.ocoa;
                    ViewBag.time = item.time;
                    ViewBag.channel = item.channel;
                    ViewBag.asset = item.ictags;
                }
            }
            if (office_coa != null)
            {

                var coa_result = (from m in db.production_log where m.ocoa == office_coa select m);
                foreach (var item in coa_result)
                {
                    ViewBag.wcoa = item.wcoa;
                    ViewBag.ocoa = item.ocoa;
                    ViewBag.time = item.time;
                    ViewBag.channel = item.channel;
                    ViewBag.asset = item.ictags;
                }
            }
            var channel_list = db.asset_tag_counter
                                 .Select(p => p.Company)
                                 .ToList();
            SelectList list = new SelectList(channel_list);
            ViewBag.myList = list;
            if (TempData["vcode"] == null)
            {
                TempData["vcode"] = "default";
            }
            ViewBag.vCode = TempData["vcode"].ToString();
            var lists = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = lists;

            var station_channel_list = db.station_setting
                                 .Select(p => p.station_dropdown_value)
                                 .ToList();
            SelectList station_list = new SelectList(station_channel_list);
            ViewBag.stationList = station_list;

            using (var context = new ApplicationDbContext())
            {
                // Populate your users and store it in the ViewBag (or other storage)
                ViewBag.Users = context.Users.Select(u => u.UserName).ToList();
            }

            return View();
        }
        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }
      
    
        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}