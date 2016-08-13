using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef.Models
{
    public class magento_importModel
    {
        
    }
    public class retail_quick_import
    {
        public string tax_id { get; set; }
        public string price { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public string weight { get; set; }
        public string desc { get; set; }
        public string short_desc { get; set; }
        public string qty { get; set; }
        public string[] webistes { get; set; }
        public string stock { get; set; }
        public string status { get; set; }
        public string visible { get; set; }
        public string attr { get; set; }
        public string type { get; set; }
        public string p_id { get; set; }
    }
    public class ts_order
    {
      public string COGS { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string Admin_Fee { get; set; }
        public string Date_Submitted { get; set; }
        public string Federal_Tax_ID { get; set; }
        public string Org_Contact_Name { get; set; }
        public string Org_Email { get; set; }
        public string Org_Name { get; set; }
        public string Org_Phone { get; set; }
        public string Org_State { get; set; }
        public string Org_Street_Address { get; set; }
        public string Org_Zip_Code { get; set; }
        public string Product_Name { get; set; }
        public string Sales_Tax { get; set; }
        public string TS_Order_Number { get; set; }
        public int Total_Product_Quantity { get; set; }
        public string Item_ID { get; set; }
        public string Program { get; set; }
      public string Org_Country { get; set; }
        public string Org_City { get; set; }


    }
    public class csv_import
    {
        public string Company { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public string Title { get; set; }
        public string Customer_Type { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string Ship_Address { get; set; }
        public string Ship_Full_Name_1 { get; set; }
        public string Ship_Company_1 { get; set; }
        public string Ship_Street_1 { get; set; }
        public string Ship_Street_2 { get; set; }
        public string Phone_1 { get; set; }
        public string Ship_City_1 { get; set; }
        public string Ship_State_1 { get; set; }
        public string Phone_2 { get; set; }
        public string Last_Sale { get; set; }
        public string EMail { get; set; }
        public string Notes { get; set; }


    }
}