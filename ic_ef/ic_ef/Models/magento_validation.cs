using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef.Models
{
    public class vendor
    {


        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string sku { get; set; }
        }

    }
    public class magento_validation
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public string entity_id { get; set; }
            public string entity_type_id { get; set; }
            public string attribute_set_id { get; set; }
            public string type_id { get; set; }
            public string vendor_id { get; set; }
            public string sku { get; set; }
            public string vendor_sku { get; set; }
            public string has_options { get; set; }
            public string required_options { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string qty { get; set; }
        }

    }
}