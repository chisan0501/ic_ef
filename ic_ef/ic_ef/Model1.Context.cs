﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_a094d4_icdbEntities : DbContext
    {
        public db_a094d4_icdbEntities()
            : base("name=db_a094d4_icdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aspnetroles> aspnetroles { get; set; }
        public virtual DbSet<aspnetuserclaims> aspnetuserclaims { get; set; }
        public virtual DbSet<aspnetuserlogins> aspnetuserlogins { get; set; }
        public virtual DbSet<aspnetusers> aspnetusers { get; set; }
        public virtual DbSet<asset_tag_counter> asset_tag_counter { get; set; }
        public virtual DbSet<coas> coas { get; set; }
        public virtual DbSet<coas_history> coas_history { get; set; }
        public virtual DbSet<discovery> discovery { get; set; }
        public virtual DbSet<ebay> ebay { get; set; }
        public virtual DbSet<label_menu> label_menu { get; set; }
        public virtual DbSet<mac_log> mac_log { get; set; }
        public virtual DbSet<monitor_log> monitor_log { get; set; }
        public virtual DbSet<monitor_setting> monitor_setting { get; set; }
        public virtual DbSet<pallet> pallet { get; set; }
        public virtual DbSet<pallet_master> pallet_master { get; set; }
        public virtual DbSet<production_log> production_log { get; set; }
        public virtual DbSet<rediscovery> rediscovery { get; set; }
        public virtual DbSet<retail> retail { get; set; }
        public virtual DbSet<setting> setting { get; set; }
        public virtual DbSet<setting_bol> setting_bol { get; set; }
        public virtual DbSet<station_setting> station_setting { get; set; }
        public virtual DbSet<temp_label> temp_label { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<picklist> picklist { get; set; }
        public virtual DbSet<pull_log> pull_log { get; set; }
        public virtual DbSet<retail_report> retail_report { get; set; }
        public virtual DbSet<magento_validation_log> magento_validation_log { get; set; }
    }
}
