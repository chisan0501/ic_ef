using HtmlAgilityPack;
using ic_ef.org.connectall;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ic_ef.Views
{
    public partial class magento : System.Web.UI.Page
    {

        string model;
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
        string hdd_price = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            //obtain the last SKU is being used in each channel
           
          //  fetch_latest_sku();
        }

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
                    oem_lap = oem_lap.Replace("ICRL", "");
                    int temp = int.Parse(oem_lap);
                    temp = temp + 1;
                    oem_lap = temp.ToString();
                    oem_lap = "ICRL" + oem_lap;
                }
                else if (titleText.Contains("ICMA"))
                {
                    apple_current = titleText;
                }
                else
                {

                }
            }

        }
        protected void channel_filter()
        {
            if (sku_family.Text.Contains("MARDK"))
            {
                sku.Text = mar_des;
            }
            else if (sku_family.Text.Contains("MARLP"))
            {
                sku.Text = mar_lap;
            }
            else if (sku_family.Text.Contains("OEMDK"))
            {
                sku.Text = oem_des;
            }
            else if (sku_family.Text.Contains("OEMLP"))
            {
                sku.Text = oem_lap;
            }
            else
            {
                sku.Text = "error";
            }
        }

        //login to magento with soap_api 
            protected void get_magento_detail ()
        {
            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123");
            //retriving list of product from storeview 
            //para @sessionid = login info
            //@ filter = searchfilter @store id = see below
            //store id : 1 = Main Website Store, 2= Low Income Store, 3 = Retail Store
      


            var result = mservice.catalogProductInfo(mlogin, "ICL1296", "1",null, null);
           
           
            mservice.Dispose();
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

        protected void get_magento_list(string source_sku)
        {

            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("chisan", "uwoqyx4y87ymx47jcwswgy7alkzpeub8");
            //retriving list of product from storeview 
            //para @sessionid = login info
            //@ filter = searchfilter @store id = see below
            //store id : 1 = Main Website Store, 2= Low Income Store, 3 = Retail Store

            string search_family = "";
            string brand_sql = "";
            string temp_serial = "";

            //************************
            //filter server selection*
            //************************

                MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new
MySql.Data.MySqlClient.MySqlConnection();
                mySqlConnection.ConnectionString = "server=us-cdbr-azure-west-c.cloudapp.net;uid=b9067d3066d876;pwd=4d1a509d;database=ic_database";

            MySqlCommand cmd2 = new MySqlCommand("select (select pallet from rediscovery where ictag='" + asset_tag_text.Text + "') as pallet,(select serial from rediscovery where ictag='" + asset_tag_text.Text + "') as serial,(select Manufacture from production_log where serial='" + temp_serial + "') as Manufacture ", mySqlConnection);

                mySqlConnection.Open();
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read()) 
                {
                    search_family = reader["pallet"].ToString();
                    temp_serial = reader["serial"].ToString();
                brand_sql = reader["Manufacture"].ToString();
            }
                mySqlConnection.Close();
            


            search_family = search_family.Substring(0, search_family.IndexOf("/") + 1);
            if (brand_sql == "Hewlett-Packard")
            {
                brand_sql = "HP ";
            }
            brand_sql = brand_sql.Substring(0, brand_sql.IndexOf(" ") + 1);
            brand_sql = brand_sql.Replace(" ", "");
            related_sale(mservice, mlogin, brand_sql, search_family, source_sku);
            cross_sale(mservice, mlogin, search_family, source_sku);
            upsell_sale(mservice, mlogin, brand_sql, search_family, source_sku);
            related_text.Text = "";


        }
        protected void cross_sale(MagentoService mservice, string mlogin, string search_family, string source_sku)
        {
            string storeview = "1";
            if (search_family.Contains("OEM"))
            {
                storeview = "3";
            }

            filters filter = new filters();

            filter = addFilter(filter, "sku_family", "like", "%" + search_family + "%");

            filter = addFilter(filter, "status", "eq", "1");
            var result = mservice.catalogProductList(mlogin, filter, storeview);

            string[] sku_arr = new string[result.Length];

            int i = 0;
            //get result sku
            foreach (catalogProductEntity product in result)
            {
                sku_arr[i] = product.sku;

                i += 1;
            }
            //get stock level
            var result2 = mservice.catalogInventoryStockItemList(mlogin, sku_arr);
            foreach (catalogInventoryStockItemEntity product in result2)
            {
                string qty_sku = product.sku;
                Double qty = double.Parse(product.qty);
                if (qty > 0)
                {
                    catalogProductLinkEntity assign = new catalogProductLinkEntity();
                    assign.position = "1";

                    // inject related listing 
                    mservice.catalogProductLinkUpdate(mlogin, "cross_sell", source_sku, qty_sku, assign, "sku");
                }
            }

            mservice.Dispose();
        }
        protected void upsell_sale(MagentoService mservice, string mlogin, string brand_sql, string search_family, string source_sku)
        {
            string storeview = "1";
            if (search_family.Contains("OEM"))
            {
                storeview = "3";
            }
            string sku_family = search_family;
            if (sku_family.Contains("DK"))
            {
                search_family = "DK";
            }
            else if (sku_family.Contains("LP"))
            {
                search_family = "LP";
            }

            filters filter = new filters();
            filter = addFilter(filter, "sku_family", "like", "%" + search_family + "%");
            filter = addFilter(filter, "status", "eq", "1");
            var result = mservice.catalogProductList(mlogin, filter, storeview);

            string[] sku_arr = new string[result.Length];

            int i = 0;
            //get result sku
            foreach (catalogProductEntity product in result)
            {
                sku_arr[i] = product.sku;

                i += 1;
            }
            //get stock level
            var result2 = mservice.catalogInventoryStockItemList(mlogin, sku_arr);
            foreach (catalogInventoryStockItemEntity product in result2)
            {
                string qty_sku = product.sku;
                Double qty = double.Parse(product.qty);
                if (qty > 0)
                {
                    catalogProductLinkEntity assign = new catalogProductLinkEntity();
                    assign.position = "1";

                    // inject related listing 
                    mservice.catalogProductLinkUpdate(mlogin, "up_sell", source_sku, qty_sku, assign, "sku");
                }
            }

            mservice.Dispose();
        }
        protected void related_sale(MagentoService mservice, string mlogin, string brand_sql, string search_family, string source_sku)
        {
            filters filter = new filters();
            string storeview = "1";
            if (search_family.Contains("OEM"))
            {
                storeview = "3";
            }
            filter = addFilter(filter, "brand", "eq", brand_sql);
            filter = addFilter(filter, "status", "eq", "1");

            var result = mservice.catalogProductList(mlogin, filter, storeview);

            string[] sku_arr = new string[result.Length];

            int i = 0;
            //get result sku
            foreach (catalogProductEntity product in result)
            {
                sku_arr[i] = product.sku;

                i += 1;
            }
            //get stock level
            var result2 = mservice.catalogInventoryStockItemList(mlogin, sku_arr);
            foreach (catalogInventoryStockItemEntity product in result2)
            {
                string qty_sku = product.sku;
                Double qty = double.Parse(product.qty);
                if (qty > 0)
                {
                    catalogProductLinkEntity assign = new catalogProductLinkEntity();
                    assign.position = "1";

                    // inject related listing 
                    mservice.catalogProductLinkUpdate(mlogin, "related", source_sku, qty_sku, assign, "sku");
                }
            }

            mservice.Dispose();
        }

        protected void pc_type_selection()
        {

            if (sku_family.Text.Contains("DK"))
            {
                pc_type.SelectedIndex = 1;
                wireless.SelectedIndex = 2;
                includes.Text = "Power Cord, Keyboard, Mouse";
            }
            else if (sku_family.Text.Contains("LP"))
            {
                pc_type.SelectedIndex = 2;
                wireless.SelectedIndex = 1;
                includes.Text = "AC Adapter";
            }
            else
            {
                pc_type.SelectedIndex = 3;
                wireless.SelectedIndex = 1;
                includes.Text = "No Accessories include";
            }
        }

        protected void load_info_Click(object sender, EventArgs e)
        {

            Label1.Text = "";
            onenote one = new onenote();
            string txt_grade = "";
            string txt_cpu = "";
            string txt_hdd = "";
            string txt_ic = "";
            string txt_ram = "";

            des.Text = "";
            soft_des.Text = "";
            short_des.Text = "";
            string serial = "";
            if (Page.IsValid)
            {

            


             
                    MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new
MySql.Data.MySqlClient.MySqlConnection();
                    mySqlConnection.ConnectionString = "server=us-cdbr-azure-west-c.cloudapp.net;uid=b9067d3066d876;pwd=4d1a509d;database=ic_database";

                MySqlCommand cmd2 = new MySqlCommand("select * from rediscovery where ictag='" + asset_tag.Text + "'", mySqlConnection);


                    mySqlConnection.Open();
                    MySqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        sku_family.Text = reader["pallet"].ToString();
                        serial = reader["serial"].ToString();
                    }
                    mySqlConnection.Close();

                    if (serial != "")
                    {
                        // connection to next database file
                        mySqlConnection.Open();

                        cmd2 = new MySqlCommand("select * from production_log where serial='" + serial + "'", mySqlConnection);

                        reader = cmd2.ExecuteReader();

                        while (reader.Read()) //read every data
                        {
                            wcoa.Text = reader["wcoa"].ToString();
                            ocoa.Text = reader["ocoa"].ToString();
                            video_card.Text = reader["video_card"].ToString();
                            screen.Text = (reader["screen_size"].ToString());
                            model = reader["Model"].ToString();
                            Session["model"] = model;
                            brand.Text = reader["Manufacture"].ToString();
                            cpu.Text = reader["CPU"].ToString();
                            //ram info roundup
                            ram.Text = reader["RAM"].ToString();
                            ram.Text = ram.Text.Replace("MB", "");
                            double temp_ram = double.Parse(ram.Text);
                            temp_ram = temp_ram / 1000;
                            ram.Text = Math.Round(temp_ram).ToString() + "GB";
                            //hdd info roundup
                            hdd.Text = reader["HDD"].ToString();
                            hdd.Text = hdd.Text.Replace("GB", "");
                            int temp_hdd = int.Parse(hdd.Text);
                            if (temp_hdd >= 0 && temp_hdd <= 80)
                            {
                                hdd.Text = "80GB";
                            }
                            else if (temp_hdd >= 81 && temp_hdd <= 120)
                            {
                                hdd.Text = "120GB";
                            }
                            else if (temp_hdd >= 121 && temp_hdd <= 160)
                            {
                                hdd.Text = "160GB";
                            }
                            else if (temp_hdd >= 161 && temp_hdd <= 250)
                            {
                                hdd.Text = "250GB";
                            }
                            else if (temp_hdd >= 251 && temp_hdd <= 320)
                            {
                                hdd.Text = "320GB";
                            }
                            else if (temp_hdd >= 321 && temp_hdd <= 500)
                            {
                                hdd.Text = "500GB";
                            }
                            else if (temp_hdd >= 501 && temp_hdd <= 610)
                            {
                                hdd.Text = "610GB";
                            }
                            else if (temp_hdd >= 611 && temp_hdd <= 750)
                            {
                                hdd.Text = "750GB";
                            }
                            else if (temp_hdd >= 751 && temp_hdd <= 1000)
                            {
                                hdd_price = "1000GB";
                                hdd.Text = "1TB";
                            }
                            else if (temp_hdd >= 1001 && temp_hdd <= 1500)
                            {
                                hdd_price = "1500GB";
                                hdd.Text = "1.5TB";
                            }
                            else if (temp_hdd >= 1501 && temp_hdd <= 2000)
                            {
                                hdd_price = "2000GB";
                                hdd.Text = "2TB";
                            }
                        }
                        mySqlConnection.Close();
                    }
                


                else
                {
                    Label1.Text = "Fail to Query Database serial is empty or null";
                    return;
                }


            }
            pc_type_selection();
            software_selection();

            txt_grade = one.grade_filter(sku_family.Text, grade.SelectedValue);
            txt_cpu = one.cpu_filter(cpu.Text);
            txt_hdd = one.hdd_filter(hdd.Text);
            txt_ram = one.ram_filter(ram.Text);
            txt_ic = one.ic_filter();

            des.Text = txt_grade + txt_cpu + txt_hdd + txt_ram + txt_ic;
            meta_info.Text = name.Text + " with " + software.Text;
            channel_filter();
            price price = new price();
            if (hdd.Text.Contains("TB"))
            {
                if (screen.Text == "") { screen.Text = "0"; }
                price_control.Text = price.base_spec(sku_family.Text, ram.Text, hdd_price, cpu.Text, grade.SelectedItem.Value, screen.Text);
            }
            else
            {
                if (screen.Text == "") { screen.Text = "0"; }
                if (screen.Text == "NA") { screen.Text = "0"; }
                double temp_screen = double.Parse(screen.Text);
                screen.Text = Math.Round(temp_screen).ToString();
                price_control.Text = price.base_spec(sku_family.Text, ram.Text, hdd.Text, cpu.Text, grade.SelectedItem.Value, screen.Text);
            }
           // fetch_photo();


        }
        private void software_selection()
        {
            cpu.Text = cpu.Text.Replace("(R)", "");
            cpu.Text = cpu.Text.Replace("(TM)", "");
            cpu.Text = cpu.Text.Replace("CPU", "");
            cpu.Text = cpu.Text.Replace("   ", "");
            try
            {


                model = model.Replace("   ", "");
            }
            catch (NullReferenceException n)
            {

            }
            name.Text = brand.Text + " " + model + " (" + cpu.Text + ", " + ram.Text + " RAM, " + hdd.Text + " HDD)";

            meta_description.Text = "US based nonprofits and low-income individuals can get a great deal on a refurbished laptop right here on the InterConnection Online Store. This is a Windows 7 machine that has been tested and IC Certified by our in-house technicians.";
            if (sku_family.Text.Contains("MAR"))
            {

                soft_des.Text = "<p>Windows 7 Professional, 64 bit</p><p>Microsoft Office 2010 Home and Business with Office, Excel, Word, Outlook and Power Point</p><p><strong>InterConnection exclusive:</strong>&nbsp;all of our computers come with a recovery partition, allowing users to restore the PCs operating system to like-new condition&nbsp;<em>without an installation disk.</em></p>";
                software.Text = "Windows 7 Professional & Microsoft Office Home and Business 2010";
                if (pc_type.SelectedIndex == 1)
                {
                    short_des.Text = "Get a great price on a great quality refurbished desktop right here at InterConnection. This desktop comes with a " + cpu.Text + " processor, " + ram.Text + " of Memory, " + hdd.Text + " Hard Drive, with Windows 7 Pro and Microsoft Office 2010 Home & Business.We back all of our products with a 1 year warranty.";
                }
                else if (pc_type.SelectedIndex == 2)
                {
                    short_des.Text = "Get a great price on a great quality refurbished laptop right here at InterConnection. This laptop comes with a " + cpu.Text + " processor, " + ram.Text + " of Memory, " + hdd.Text + " Hard Drive, with Windows 7 Pro and Microsoft Office 2010 Home & Business.We back all of our products with a 1 year warranty.";
                }

            }
            else if (sku_family.Text.Contains("OEM"))
            {

                soft_des.Text = "<p>Windows 7 Home Premium, 64 bit</p><p><strong>InterConnection exclusive:</strong>&nbsp;all of our computers come with a recovery partition, allowing users to restore the PCs operating system to like-new condition&nbsp;<em>without an installation disk.</em></p>";
                software.Text = "Windows 7 Home Premium";
                if (pc_type.SelectedIndex == 1)
                {
                    short_des.Text = "Get a great price on a great quality refurbished desktop right here at InterConnection. This desktop comes with an " + cpu.Text + " processor, " + ram.Text + " of Memory, " + hdd.Text + " Hard Drive, with Windows 7 Home Premium.  We back all of our products with a 1 year warranty.";
                }
                else if (pc_type.SelectedIndex == 2)
                {
                    if (brand.Text.Contains("Hewlett-Packard"))
                    {
                        short_des.Text = "Get a great price on a great quality refurbished HP laptop right here at InterConnection. This HP laptop comes with an " + cpu.Text + " processor, " + ram.Text + " of Memory, " + hdd.Text + " Hard Drive, with Windows 7 Home Premium. We back all of our products with a 1 year warranty.";
                    }
                    else if (brand.Text.Contains("Lenovo"))
                    {
                        short_des.Text = "Get a great price on a great quality refurbished Lenovo laptop right here at InterConnection. This Lenovo laptop comes with an " + cpu.Text + " processor, " + ram.Text + " of Memory, " + hdd.Text + " Hard Drive, with Windows 7 Home Premium.  We back all of our products with a 1 year warranty.";
                    }
                    else
                    {
                        short_des.Text = "Get a great price on a great quality refurbished laptop right here at InterConnection. This laptop comes with an " + cpu.Text + " processor, " + ram.Text + " of Memory, " + hdd.Text + " Hard Drive, with Windows 7 Home Premium. We back all of our products with a 1 year warranty.";
                    }


                }

            }

        }
        protected void create_date_Load(object sender, EventArgs e)
        {
            create_date.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
        protected void export_Click(object sender, EventArgs e)
        {

            MagentoService mservice = new MagentoService();
            String mlogin = mservice.login("admin", "Interconnection123");

            string[] arr1 = new string[] { "1218"};

            var item = mservice.catalogInventoryStockItemList(mlogin, arr1);


            //working up_sell function
            //catalogProductLinkEntity assign = new catalogProductLinkEntity();
            //assign.position = "1";


            //mservice.catalogProductLinkUpdate(mlogin, "related", "1030", "1029", assign, "product_id");



            catalogProductCreateEntity create = new catalogProductCreateEntity();
            var inv = new catalogProductTierPriceEntity();

            create.name = name.Text;
            create.price = price_control.Text;
            create.description = des.Text;
            create.short_description = short_des.Text;
            create.tax_class_id = "2";
            create.meta_title = meta_info.Text;
            create.visibility = "4";
            create.weight = "0";
            create.status = "2";
            create.meta_description = meta_description.Text;
            inv.qty = 0;

            associativeEntity[] attributes = new associativeEntity[18];
            attributes[0] = new associativeEntity();
            attributes[0].key = "asset_tag";
            attributes[0].value = asset_tag.Text;

            attributes[1] = new associativeEntity();
            attributes[1].key = "sku_family";
            attributes[1].value = sku_family.Text;

            attributes[2] = new associativeEntity();
            attributes[2].key = "cpu";
            attributes[2].value = cpu.Text;

            attributes[3] = new associativeEntity();
            attributes[3].key = "software_description";
            attributes[3].value = soft_des.Text;

            attributes[4] = new associativeEntity();
            attributes[4].key = "ram";
            attributes[4].value = ram.Text + " RAM";

            attributes[5] = new associativeEntity();
            attributes[5].key = "hdd";
            attributes[5].value = hdd.Text + " HDD";

            attributes[6] = new associativeEntity();
            attributes[6].key = "os";
            attributes[6].value = software.Text;

            attributes[7] = new associativeEntity();
            attributes[7].key = "creation_date";
            attributes[7].value = create_date.Text;

            attributes[8] = new associativeEntity();
            attributes[8].key = "wireless";
            attributes[8].value = wireless.SelectedItem.ToString();

            attributes[9] = new associativeEntity();
            attributes[9].key = "incl";
            attributes[9].value = includes.Text;

            attributes[10] = new associativeEntity();
            attributes[10].key = "brand";
            attributes[10].value = brand.Text;

            attributes[11] = new associativeEntity();
            attributes[11].key = "grade";
            attributes[11].value = grade.SelectedItem.Value;

            attributes[12] = new associativeEntity();
            attributes[12].key = "wcoa";
            attributes[12].value = wcoa.Text;

            attributes[13] = new associativeEntity();
            attributes[13].key = "ocoa";
            attributes[13].value = ocoa.Text;

            attributes[14] = new associativeEntity();
            attributes[14].key = "video";
            attributes[14].value = video_card.Text;

            attributes[15] = new associativeEntity();
            attributes[15].key = "display";
            attributes[15].value = screen.Text;

            attributes[16] = new associativeEntity();
            attributes[16].key = "computer";
            attributes[16].value = is_computer.SelectedValue;

            attributes[17] = new associativeEntity();
            attributes[17].key = "optical";
            attributes[17].value = optical.Text;

            catalogProductAdditionalAttributesEntity additionalAttributes = new catalogProductAdditionalAttributesEntity();
            additionalAttributes.single_data = attributes;
            create.additional_attributes = additionalAttributes;

            mservice.catalogProductCreate(
mlogin, "simple", "4", sku.Text, create, "1");

            mservice.Dispose();

            //update inventory 
            catalogInventoryStockItemUpdateEntity qty_update = new catalogInventoryStockItemUpdateEntity();


            qty_update.qty = qty.Text;
            qty_update.is_in_stock = int.Parse(stock_Availability.SelectedValue);

            mservice.catalogInventoryStockItemUpdate(
mlogin, sku.Text, qty_update);



            get_magento_list(sku.Text);
            ClearTextBoxes(Page);
            //string content = "admin,,Default,simple,,,0,"+status.SelectedItem.ToString()+ ",No,\"Catalog, Search\",Yes,"+tax_class.SelectedItem.ToString()+ ",No,No,Yes,No,No,\"" + name.Text+ "\",,," + software.Text+ ","+cpu.Text+","+hdd.Text+",,"+ram.Text+ ",Use config,Use config,\"" + meta_info.Text+ "\",\"" + meta_description.Text+ "\",,,,,No layout updates,Block after Info Column,No,,,,,,"+brand.Text+ ",\"" + includes.Text+ "\"," + grade.SelectedItem.ToString()+",,,,,,,,,,,,,,,,,,"+wireless.SelectedItem.ToString()+ ",,,,,,,,,,,,,," + des.Text + ",\"" + short_des.Text+ "\",,,\"" + soft_des.Text+ "\",,,,,,,,,,,1,0,1,0,0,1,1,1,0,1,0,,,1,0,1,0,1,0,1,0,0,0,1,\"" + name.Text+ "\",0,simple,,,,,,,,,,," + asset_tag.Text+","+sku_family.Text+","+create_date.Text+"";
        }
        protected void related_update_Click(object sender, EventArgs e)
        {
            get_magento_list(related_text.Text);
        }
        protected void ClearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox t = ctrl as TextBox;

                    if (t != null)
                    {
                        t.Text = String.Empty;
                    }
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearTextBoxes(ctrl);
                    }
                }
            }
        }
    }

}