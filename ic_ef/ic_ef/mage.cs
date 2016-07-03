using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef
{
    public class mage
    {
       
      
        private ic_databaseEntities2 db = new ic_databaseEntities2();
       
        public mage()
        {
          
           

        }
        public List<ic_ef.rediscovery> rediscovery (int asset_tag)
        {
            var result = (from t in db.rediscovery where t.ictag == asset_tag select t).ToList();
            return result;
            
        }
        //get the asset's spec by the machine's serial number 
        public List<Models.magentoViewModel> get_spec (int asset_tag)
        {
            string result = (from b in db.rediscovery where b.ictag == asset_tag select b.serial).SingleOrDefault();

            var spec = (from t in db.production_log where t.serial == result from h in db.rediscovery where t.serial == h.serial select new Models.magentoViewModel { hdd = t.HDD, cpu = t.CPU, brand = t.Manufacture, ram = t.RAM, ictags = asset_tag, model = t.Model, serial = h.serial, screen_size = t.screen_size, video_card = t.video_card, optical = h.optical_drive, pallet_name = h.pallet }).ToList();
           
            return spec;
        }
     
    }
    
}