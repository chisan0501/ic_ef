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
    }
}