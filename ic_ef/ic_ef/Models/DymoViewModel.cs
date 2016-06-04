using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef.Models
{
    public class DymoViewModel
    {
        public string pre_coa { get; set; }
        public string pallet { get; set; }
        public string time { get; set; }
        public string cpu { get; set; }
        public string ram { get; set; }
        public string hdd { get; set; }
        public string optical_drive { get; set; }
        public string asset { get; set; }
        public string[] barcode_arr2 { get; set; }
        public string[] vcode2 { get; set; }
    }
    public class detailViewModel
    {
        public int ictags { get; set; }
        public string pallet_name { get; set; }
        public string brand { get; set; }
        public string note { get; set; }
        public string model { get; set; }
        public string cpu { get; set; }
        public string ram { get; set; }
        public string hdd { get; set; }
    }
    public class magentoViewModel
    {
        public int ictags { get; set; }
        public string pallet_name { get; set; }
        public string brand { get; set; }
        public string note { get; set; }
        public string model { get; set; }
        public string cpu { get; set; }
        public string ram { get; set; }
        public string hdd { get; set; }
        public string sku { get; set; }
        public string serial { get; set; }
        public string screen_size { get; set; }
        public string video_card { get; set; }
        public string optical { get; set; }
        public string accessory_include { get; set; }
        public int selectedId { get; set; }
        public System.Web.Mvc.SelectList type;
        public System.Web.Mvc.SelectList wireless;
        public int wireless_selectedId { get; set; }
        public System.Web.Mvc.SelectList grade;
        public int grade_selectedId { get; set; }
        public string creat_date { get; set; }
        public string software { get; set; }
        public string meta_title { get; set; }
        public string meta_description { get; set; }
        public string software_description { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public int tax_selectedId { get; set; }
        public int qty { get; set; }
        public System.Web.Mvc.SelectList cat;
        public int cat_selectedId { get; set; }
        public System.Web.Mvc.SelectList tax_class;
        public System.Web.Mvc.SelectList laptop_listing;
        public int laptop_listing_selectedId { get; set; }
        public System.Web.Mvc.SelectList desktop_listing;
        public int desktop_listing_selectedId { get; set; }
    
    }


    public class Rootobject
    {
        public laptop[] Property1 { get; set; }
    }

    public class laptop
    {
        public string entity_id { get; set; }
        public string entity_type_id { get; set; }
        public string attribute_set_id { get; set; }
        public string type_id { get; set; }
        public string sku { get; set; }
        public string has_options { get; set; }
        public string required_options { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string cat_index_position { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string ram { get; set; }
        public string hdd { get; set; }
        public string cpu { get; set; }
        public string qty { get; set; }
        public string status { get; set; }
    }





}