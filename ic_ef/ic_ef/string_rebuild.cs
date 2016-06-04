using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef
{
    public class string_rebuild
    {
        public string brand (string brand)
        {
            

           switch (brand)
            {
                case "Hewlett-Packard":
                    return "HP";
                case "Dell Inc.":
                    return "Dell";
                case "ASUSTeK Computer Inc.":
                    return "Asus";
                case "HP-Pavilion":
                    return "HP";
                case "Sony Corporation":
                    return "Sony";
                case "SAMSUNG ELECTRONICS CO., LTD.":
                    return "Samsung";
                case "Acer, inc.":
                    return "Acer";
                default:
                    return "Other";
            }

        }
        public string CPU(string value)
        {
            string temp_value = value;
            
            temp_value = temp_value.Replace("Intel(R)", "");
            string front = temp_value.Substring(0,temp_value.IndexOf('@'));
            string back = temp_value.Substring(temp_value.LastIndexOf('@') + 1);

            if (front.Contains("i3"))
            {
                value = "Intel Core " + "i3 " + "(" +back + " )";
            }
            else if (front.Contains("i5"))
            {
                value = "Intel Core " + "i5 " + "(" + back + " )";
            }
            else if (front.Contains("i7"))
            {
                value = "Intel Core " + "i7 " + "(" + back + " )";
            }
            else if (front.Contains("2 Duo"))
            {
                value = "Intel Core " + "2 Duo " + "(" + back + " )";
            }
            else if (front.Contains("Pentium(R) CPU"))
            {
                value = "Intel Pentium(R) " + "(" + back + " )";
            }
            else if (front.Contains("2 Quad"))
            {
                value = "Intel Core " + "2 Quad " + "(" + back + " )";
            }
            else if (front.Contains("2 Quad"))
            {
                value = "Intel Core " + "2 Quad " + "(" + back + " )";
            }
            else if (front.Contains("Pentium(R) Dual-Core"))
            {
                value = "Intel Pentium(R) Dual-Core " + "(" + back + " )";
            }
            else if (front.Contains("Genuine"))
            {
                value = "Intel Core Duo  " + "(" + back + " )";
            }


            return value;
     
        }
        public string hdd(string value)
        {
            value = value.Replace("GB", "");
            int temp_hdd = int.Parse(value);

            if (temp_hdd >= 0 && temp_hdd <= 80)
            {
                value = "80GB";
            }
            else if (temp_hdd >= 81 && temp_hdd <= 120)
            {
                value = "120GB";
            }
            else if (temp_hdd >= 121 && temp_hdd <= 160)
            {
                value = "160GB";
            }
            else if (temp_hdd >= 161 && temp_hdd <= 250)
            {
                value = "250GB";
            }
            else if (temp_hdd >= 251 && temp_hdd <= 320)
            {
                value = "320GB";
            }
            else if (temp_hdd >= 321 && temp_hdd <= 500)
            {
                value = "500GB";
            }
            else if (temp_hdd >= 501 && temp_hdd <= 610)
            {
                value = "610GB";
            }
            else if (temp_hdd >= 611 && temp_hdd <= 750)
            {
                value = "750GB";
            }
            else if (temp_hdd >= 751 && temp_hdd <= 1000)
            {
               // hdd_price = "1000GB";
                value = "1TB";
            }
            else if (temp_hdd >= 1001 && temp_hdd <= 1500)
            {
              //  hdd_price = "1500GB";
                value = "1.5TB";
            }
            else if (temp_hdd >= 1501 && temp_hdd <= 2000)
            {
              //  hdd_price = "2000GB";
                value = "2TB";
            }
            value = value.Replace("GB","GB Hard Drive" );
            value = value.Replace("TB","TB Hard Drive");
            return value;
        }
         public string ram(string value)
        {
            value = value.Replace("MB", "");
            int temp_ram = int.Parse(value);

            if (temp_ram < 1000)
            {
                value = "1GB Memory";
            } 
            else if (temp_ram > 1000 & temp_ram <= 2300)
            {
                value = "2GB Memory";
            }
            else if (temp_ram > 2000 & temp_ram <= 3300)
            {
                value = "3GB Memory";
            }
            else if (temp_ram > 3000 & temp_ram <= 4300)
            {
                value = "4GB Memory";
            }
            else if (temp_ram > 4000 & temp_ram <= 6300)
            {
                value = "6GB Memory";
            }
            else if (temp_ram > 6000 & temp_ram <= 8300)
            {
                value = "8GB Memory";
            }
            else if (temp_ram > 11000 & temp_ram <= 12300)
            {
                value = "12GB Memory";
            }
            else if (temp_ram > 15000 & temp_ram <= 16300)
            {
                value = "16GB Memory";
            }
            return value;
        }
    }
}