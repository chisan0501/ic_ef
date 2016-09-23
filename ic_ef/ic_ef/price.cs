using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef
{
    public class price
    {
        int screen_price = 0;
        int ram_price = 0;
        int hdd_price = 0;
        int default_hdd = 0;
        int default_ram = 0;
        double final_price = 0;
        string sku_family = "";

        public string base_spec(string input, string ram, string hdd, string cpu, string grade, string screen_size)
        {
            
            string output = "";
            ram = ram.Replace("GB", "");
            ram = ram.Replace("Memory", "");
            hdd = hdd.Replace("GB", "");
            hdd = hdd.Replace("Hard Drive", "");

            string cut_cpu = cpu.Substring(cpu.IndexOf('(') + 1);
                cut_cpu = cut_cpu.Substring(cut_cpu.IndexOf('(') + 1);
            cut_cpu = cut_cpu.Replace("GHz", "");
            cut_cpu = cut_cpu.Replace(")", "");
            if (cut_cpu == "") { cut_cpu = "0"; }
            double temp_cpu = double.Parse(cut_cpu);

            int temp_ram = int.Parse(ram);
            
            int temp_hdd = int.Parse(hdd);


            if (input.Contains("SSD"))
            {
                if (input.Contains("OEM"))
                {
                    output = oemssd(temp_cpu, temp_ram, temp_hdd, grade, input, screen_size).ToString();
                }
                else if (input.Contains("MAR"))
                {
                    output = marssd(temp_cpu, temp_ram, temp_hdd, grade, input, screen_size).ToString();
                }

            }
            else
            {
                if (input.Contains("MARDKI5"))
                {
                    output = mardki5(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                else if (input.Contains("MARDKI7"))
                {
                    output = mardki7(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                else if (input.Contains("MARDKi3"))
                {
                    output = mardki3(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                else if (input.Contains("MARDKC2D"))
                {
                    output = mardkc2d(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                if (input.Contains("MARLPI5"))
                {
                    output = marlpi5(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("MARLPI7"))
                {
                    output = marlpi7(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("MARLPi3"))
                {
                    output = marlpi3(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("MARLPC2D"))
                {
                    output = marlpc2d(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                if (input.Contains("OEMDKI5"))
                {
                    output = oemdki5(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                else if (input.Contains("OEMDKI7"))
                {
                    output = oemdki7(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                else if (input.Contains("OEMDKI3"))
                {
                    output = oemdki3(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }
                else if (input.Contains("OEMDKC2D"))
                {
                    output = oemdkc2d(temp_cpu, temp_ram, temp_hdd, grade).ToString();
                }

                else if (input.Contains("OEMLPI5"))
                {
                    output = oemlpi5(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("OEMLPI7"))
                {
                    output = oemlpi7(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("OEMLPi3"))
                {
                    output = oemlpi3(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("OEMLPI3"))
                {
                    output = oemlpi3(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
                else if (input.Contains("OEMLPC2D"))
                {
                    output = oemlpc2d(temp_cpu, temp_ram, temp_hdd, grade, screen_size).ToString();
                }
            }


            return output;
        }

        private double mardkc2d(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
            

            sku_family = "MARDKC2D/all";
            final_price = 79;
            if (ram == 2 && hdd == 80)
            {
                final_price = 79;
                //default spec
            }
            else
            {
                default_ram = 2;
                default_hdd = 80;
                ram_price = mar_cal_ram(ram, default_ram);
                hdd_price = mar_cal_hdd(hdd, default_hdd);
            }
            final_price = final_price + ram_price + hdd_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double mardki3(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
           

            sku_family = "MARDKi3/all-250";
            final_price = 179;
            if (ram == 4 && hdd == 250)
            {
                final_price = 179;
                //default spec
            }
            else
            {
                default_ram = 4;
                default_hdd = 250;
                ram_price = mar_cal_ram(ram, default_ram);
                hdd_price = mar_cal_hdd(hdd, default_hdd);
            }
            final_price = final_price + ram_price + hdd_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double mardki5(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
            
            if (cpu <= 2.4)
            {
                sku_family = "MARDKI5/1st-250";
                final_price = 199;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 199;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);
                }
                final_price = final_price + ram_price + hdd_price;
            }
            else
            {
                sku_family = "MARDKI5/3rd-500";
                final_price = 249;
                if (ram == 4 && hdd == 500)
                {

                    final_price = 249;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);

                }
                final_price = final_price + ram_price + hdd_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double mardki7(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
            
            if (cpu <= 2.4)
            {
                sku_family = "MARDKI7/1st-250";
                final_price = 299;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 299;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);
                }
                final_price = final_price + ram_price + hdd_price;
            }
            else
            {
                sku_family = "MARDKI7/3rd-500";
                final_price = 349;
                if (ram == 4 && hdd == 500)
                {

                    final_price = 349;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);

                }
                final_price = final_price + ram_price + hdd_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double marlpc2d(double cpu, int ram, int hdd, string grade, string size)
        {


            sku_family = "MARLPC2D/all";
            final_price = 99;
            if (ram == 2 && hdd == 80)
            {
                final_price = 99;
                //default spec
            }
            else
            {
                default_ram = 2;
                default_hdd = 80;
                ram_price = mar_cal_ram(ram, default_ram);
                hdd_price = mar_cal_hdd(hdd, default_hdd);
                Double temp_size = double.Parse(size);
                int real_size = Convert.ToInt32(temp_size);
               
                screen_price = screen(real_size);
            }
            final_price = final_price + ram_price + hdd_price + screen_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double marlpi3(double cpu, int ram, int hdd, string grade, string size)
        {


            sku_family = "MARLPi3/all-250";
            final_price = 179;
            if (ram == 4 && hdd == 250)
            {
                final_price = 179;
                //default spec
            }
            else
            {
                default_ram = 4;
                default_hdd = 250;
                ram_price = mar_cal_ram(ram, default_ram);
                hdd_price = mar_cal_hdd(hdd, default_hdd);
                screen_price = screen(int.Parse(size));
            }
            final_price = final_price + ram_price + hdd_price + screen_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double marlpi5(double cpu, int ram, int hdd, string grade, string size)
        {

            if (cpu <= 2.4)
            {
                sku_family = "MARLPI5/1st-250";
                final_price = 229;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 229;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }
            else
            {
                sku_family = "MARLPI5/3rd-500";
                final_price = 279;
                if (ram == 4 && hdd == 500)
                {

                    final_price = 279;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double marlpi7(double cpu, int ram, int hdd, string grade, string size)
        {
            if (cpu <= 2.4)
            {
                sku_family = "MARLPI7/1st-250";
                final_price = 329;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 329;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }
            else
            {
                sku_family = "MARLPI7/3rd-500";
                final_price = 379;
                if (ram == 4 && hdd == 500)
                {

                    final_price = 379;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = mar_cal_ram(ram, default_ram);
                    hdd_price = mar_cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double marssd(double cpu, int ram, int hdd, string grade, string skufamily, string size)
        {

            string sku_family = skufamily;
            if (skufamily.Contains("MARLPI5/all-SSD"))
            {
                sku_family = "MARLPI5/all-SSD";
                final_price = 239;
                if (ram == 4 && hdd == 120)
                {
                    final_price = 239;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 120;
                    ram_price = cal_ram(ram, default_ram);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }
            else
            {
                sku_family = "MARLPI7/all-SSD";
                final_price = 339;
                if (ram == 4 && hdd == 120)
                {

                    final_price = 339;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 120;
                    ram_price = cal_ram(ram, default_ram);
                    screen_price = screen(int.Parse(size));

                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }



        private double oemdkc2d(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
            

            sku_family = "OEMDKC2D/all";
            final_price = 124;
            if (ram == 2 && hdd == 80)
            {
                final_price = 124;
                //default spec
            }
            else
            {
                default_ram = 2;
                default_hdd = 80;
                ram_price = cal_ram(ram, default_ram);
                hdd_price = cal_hdd(hdd, default_hdd);
            }
            final_price = final_price + ram_price + hdd_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemdki3(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
          

            sku_family = "OEMDKi3/all-250";
            final_price = 199;
            if (ram == 4 && hdd == 250)
            {
                final_price = 199;
                //default spec
            }
            else
            {
                default_ram = 4;
                default_hdd = 250;
                ram_price = cal_ram(ram, default_ram);
                hdd_price = cal_hdd(hdd, default_hdd);
            }
            final_price = final_price + ram_price + hdd_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemdki5(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
            //string sku_family = "";
            if (cpu <= 2.4)
            {
                sku_family = "OEMDKI5/1st-250";
                final_price = 219;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 219;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);
                }
                final_price = final_price + ram_price + hdd_price;
            }
            else
            {
                sku_family = "OEMDKI5/3rd-500";
                final_price = 269;
                if (ram == 4 && hdd == 500)
                {

                    final_price = 269;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);

                }
                final_price = final_price + ram_price + hdd_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemdki7(double cpu, int ram, int hdd, string grade)
        {
            int ram_price = 0;
            int hdd_price = 0;
            int default_hdd = 0;
            int default_ram = 0;
            double final_price = 0;
           
            if (cpu <= 2.4)
            {
                sku_family = "OEMDKI7/1st-250";
                final_price = 349;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 219;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);
                }
                final_price = final_price + ram_price + hdd_price;
            }
            else
            {
                sku_family = "OEMDKI7/3rd-500";
                final_price = 399;
                if (ram == 4 && hdd == 500)
                {


                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);

                }
                final_price = final_price + ram_price + hdd_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemlpc2d(double cpu, int ram, int hdd, string grade, string size)
        {


            sku_family = "OEMLPC2D/all";
            final_price = 129;
            if (ram == 2 && hdd == 80)
            {
                final_price = 124;
                //default spec
            }
            else
            {
                default_ram = 2;
                default_hdd = 80;
                ram_price = cal_ram(ram, default_ram);
                hdd_price = cal_hdd(hdd, default_hdd);
                screen_price = screen(int.Parse(size));
            }
            final_price = final_price + ram_price + hdd_price + screen_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemlpi3(double cpu, int ram, int hdd, string grade, string size)
        {


            sku_family = "OEMLPi3/all-250";
            final_price = 199;
            if (ram == 4 && hdd == 250)
            {
                final_price = 199;
                //default spec
            }
            else
            {
                default_ram = 4;
                default_hdd = 250;
                ram_price = cal_ram(ram, default_ram);
                hdd_price = cal_hdd(hdd, default_hdd);
                screen_price = screen(int.Parse(size));
            }
            final_price = final_price + ram_price + hdd_price + screen_price;


            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemlpi5(double cpu, int ram, int hdd, string grade, string size)
        {


            if (cpu <= 2.4)
            {
                sku_family = "OEMLPI5/1st-250";
                final_price = 249;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 249;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }
            else
            {
                sku_family = "OEMLPI5/3rd-500";
                final_price = 299;
                if (ram == 4 && hdd == 500)
                {

                    final_price = 299;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemlpi7(double cpu, int ram, int hdd, string grade, string size)
        {

            if (cpu <= 2.4)
            {
                sku_family = "OEMLPI7/1st-250";
                final_price = 349;
                if (ram == 4 && hdd == 250)
                {
                    final_price = 349;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 250;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }
            else
            {
                sku_family = "OEMLPI7/3rd-500";
                final_price = 399;
                if (ram == 4 && hdd == 500)
                {


                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 500;
                    ram_price = cal_ram(ram, default_ram);
                    hdd_price = cal_hdd(hdd, default_hdd);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }
        private double oemssd(double cpu, int ram, int hdd, string grade, string skufamily, string size)
        {

            string sku_family = skufamily;
            if (skufamily.Contains("OEMLPI5/all-SSD"))
            {
                sku_family = "OEMLPI5/all-SSD";
                final_price = 249;
                if (ram == 4 && hdd == 120)
                {
                    final_price = 249;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 120;
                    ram_price = cal_ram(ram, default_ram);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }
            else
            {
                sku_family = "OEMLPI7/all-SSD";
                final_price = 349;
                if (ram == 4 && hdd == 120)
                {

                    final_price = 349;
                    //default spec
                }
                else
                {
                    default_ram = 4;
                    default_hdd = 120;
                    ram_price = cal_ram(ram, default_ram);
                    screen_price = screen(int.Parse(size));
                }
                final_price = final_price + ram_price + hdd_price + screen_price;
            }

            switch (grade)
            {
                case "A":
                    {
                        return final_price;
                    }
                case "B":
                    {
                        return final_price - 20;
                    }

            }
            return final_price;
        }

        //cal ram price
        private int cal_ram(int ram, int default_ram)
        {
            int ramm = 0;
            int ram_default = 0;
            int spec = 0;
            Dictionary<int, int> ram_pricing =
          new Dictionary<int, int>();
            ram_pricing.Add(2, 0);
            ram_pricing.Add(4, 1);
            ram_pricing.Add(6, 2);
            ram_pricing.Add(8, 3);
            ram_pricing.Add(12, 5);
            ram_pricing.Add(16, 6);
            ram_default = ram_pricing[default_ram];
            spec = ram_pricing[ram];

            if (spec == ram_default)
            {
                ramm = 0;
            }
            else if (spec > ram_default)
            {
                ramm = (spec - ram_default) * 25;
            }
            else if (spec < ram_default)
            {
                ramm = -(spec - ram_default) * 25;
            }


            return ramm;
        }

        //cal harddrive price;
        private int cal_hdd(int hdd, int default_hdd)
        {
            int hddd = 0;
            int hdd_default = 0;
            int spec = 0;
            Dictionary<int, int> hdd_pricing =
          new Dictionary<int, int>();
            hdd_pricing.Add(80, 0);
            hdd_pricing.Add(120, 10);
            hdd_pricing.Add(160, 15);
            hdd_pricing.Add(200, 20);
            hdd_pricing.Add(250, 25);
            hdd_pricing.Add(320, 30);
            hdd_pricing.Add(500, 35);
            hdd_pricing.Add(600, 40);
            hdd_pricing.Add(610, 40);
            hdd_pricing.Add(750, 50);
            hdd_pricing.Add(1000, 70);
            hdd_pricing.Add(1500, 100);
            hdd_pricing.Add(2000, 130);

            hdd_default = hdd_pricing[default_hdd];
            spec = hdd_pricing[hdd];


            if (spec == hdd_default)
            {
                hddd = 0;
            }
            else if (spec > hdd_default)
            {
                hddd = spec;
            }
            else if (spec < hdd_default)
            {
                hddd = -(hdd_default - spec);
            }


            return hddd;
        }

        private int mar_cal_ram(int ram, int default_ram)
        {
            int ramm = 0;
            int ram_default = 0;
            int spec = 0;
            Dictionary<int, int> ram_pricing =
          new Dictionary<int, int>();
            ram_pricing.Add(2, 0);
            ram_pricing.Add(3, 1);
            ram_pricing.Add(4, 2);
            ram_pricing.Add(6, 3);
            ram_pricing.Add(8, 4);
            ram_pricing.Add(12, 5);

            ram_default = ram_pricing[default_ram];
            spec = ram_pricing[ram];

            if (spec == ram_default)
            {
                ramm = 0;
            }
            else if (spec > ram_default)
            {
                ramm = (spec - ram_default) * 20;
            }
            else if (spec < ram_default)
            {
                ramm = -(spec - ram_default) * 20;
            }


            return ramm;
        }

        //cal harddrive price;
        private int mar_cal_hdd(int hdd, int default_hdd)
        {
            int hddd = 0;
            int hdd_default = 0;
            int spec = 0;
            Dictionary<int, int> hdd_pricing =
          new Dictionary<int, int>();
            hdd_pricing.Add(80, 0);
            hdd_pricing.Add(120, 5);
            hdd_pricing.Add(160, 10);
            hdd_pricing.Add(200, 15);
            hdd_pricing.Add(250, 20);
            hdd_pricing.Add(320, 25);
            hdd_pricing.Add(500, 30);
            hdd_pricing.Add(600, 35);
            hdd_pricing.Add(610, 35);
            hdd_pricing.Add(750, 40);
            hdd_pricing.Add(1000, 50);
            hdd_pricing.Add(1500, 80);
            hdd_pricing.Add(2000, 110);

            hdd_default = hdd_pricing[default_hdd];
            spec = hdd_pricing[hdd];


            if (spec == hdd_default)
            {
                hddd = 0;
            }
            else if (spec > hdd_default)
            {
                hddd = spec;
            }
            else if (spec < hdd_default)
            {
                hddd = -(hdd_default - spec);
            }


            return hddd;
        }
        private int screen(int size)
        {
            
            int screen_price = 0;

            if (size == 14 | size == 0)
            {
                screen_price = 0;
            }
            else if (size < 14)
            {
                screen_price = -10;
            }
            else if (size >= 15)
            {
                screen_price = 10;
            }



            return screen_price;
        }
    }
}