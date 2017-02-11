using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef.Models
{
    public class latestsku
    {
        public class ICRD
        {
            public string sku { get; set; }
        }

        public class ICL
        {
            public string sku { get; set; }
        }

        public class ICD
        {
            public string sku { get; set; }
        }

        public class ICRL
        {
            public string sku { get; set; }
        }

        public class ICMA
        {
            public string sku { get; set; }
        }

        public class RootObject
        {
            public List<ICRD> ICRD { get; set; }
            public List<ICL> ICL { get; set; }
            public List<ICD> ICD { get; set; }
            public List<ICRL> ICRL { get; set; }
            public List<ICMA> ICMA { get; set; }
        }
    }
    public class sku_model {
        public string ICRD_sku { get; set; }
        public string ICL_sku { get; set; }
        public string ICD_sku { get; set; }
        public string ICRL_sku { get; set; }
        public string ICMA_sku { get; set; }

    } 
    
}