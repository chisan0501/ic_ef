//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ic_ef
{
    using System;
    using System.Collections.Generic;
    
    public partial class rma
    {
        public int row_id { get; set; }
        public string serial { get; set; }
        public string rma_number { get; set; }
        public string production_finding { get; set; }
        public string case_number { get; set; }
        public string id { get; set; }
        public string channel { get; set; }
        public Nullable<System.DateTime> date_requested { get; set; }
        public string resolution_code { get; set; }
        public string ictag { get; set; }
        public string description { get; set; }
    }
}
