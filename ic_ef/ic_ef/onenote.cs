using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ic_ef
{
    public class onenote
    {


        public string grade_filter(string channel, string grade)
        {

            string grade_filter = "";
            if (channel.Contains("MAR"))
            {
                if (grade == "Grade A")
                {
                    grade_filter = "<p><img src=\"{{media url=\"wysiwyg/GradeA.png\"}}\"style=\"PADDING - LEFT: 8px\" align=\"right\" alt=\"Grade A\">This unit meets our highest cosmetic grade. It may have a few minor cosmetic defects including very faint scratches or scuffs. Some laptops may exhibit some shiny areas around the keyboard (space bar, touchpad and/or touchpad buttons), but the keyboard keys will always be in excellent condition. Any blemishes are purely cosmetic and will have no impact to the system’s overall functionality or performance.</p><br><br><hr>";


                }
                else if (grade == "Grade B")
                {
                    grade_filter = "<p><img src=\"{{media url=\"wysiwyg/GradeB.png\"}} \" style=\"PADDING-LEFT: 8px\" align=\"right\" alt=\"Grade B\">Grade B units are fully tested and functional, just like Grade A. They will have more visible blemishes and are discounted accordingly.  These could include slight scratches & scuffs, wear on the palm rest and slight dimples in the top plastic. Any blemishes are purely cosmetic and will have no impact to the system’s overall functionality or performance.</p><br><br><br><hr>";
                }
            }
            else if (channel.Contains("OEM"))
            {
                if (grade == "Grade A")
                {
                    grade_filter = "<p><img src=\"{{media url=\"wysiwyg/GradeA.png\"}}\" style=\"PADDING - LEFT: 8px\" align=\"right\" alt=\"Grade A\">This unit meets our highest cosmetic grade. It may have a few minor cosmetic defects including very faint scratches or scuffs. Some laptops may exhibit some shiny areas around the keyboard (space bar, touchpad and/or touchpad buttons), but the keyboard keys will always be in excellent condition. Any blemishes are purely cosmetic and will have no impact to the system’s overall functionality or performance.<p><br><br><hr>";
                }
                else if (grade == "Grade B")
                {
                    grade_filter = "<p><img src=\"{ { media url = \"wysiwyg/GradeB.png\"} }\" style=\"PADDING - LEFT: 8px\" align=\"right\" alt=\"Grade B\">Grade B units are fully tested and functional, just like Grade A.They will have more visible blemishes and are discounted accordingly.  These could include slight scratches & scuffs, wear on the palm rest and slight dimples in the top plastic.Any cosmetic blemishes will have no impact to the system’s overall functionality or performance.</p><br><br><br><hr>";
                }
            }
            grade_filter = grade_filter.Replace(@"\", "'");


            return grade_filter;

        }
        public string cpu_filter(string cpu)
        {
            string cpu_result = "";

            if (cpu.Contains("2 Duo"))
            {
                cpu_result = "<p><img src = \"{{media url=\"wysiwyg/Core2Duo.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"Core2Duo\">This refurbished computer features an Intel dual core processor. This processor delivers fast performance, great energy efficiency, and responsive multitasking, so you can be more productive.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the Intel Core 2 processor family let you get more done in less time,reducing energy costs by an average of 50 percent.</p><br><hr>";
            }
            else if (cpu.Contains("2 Quad"))
            {
                cpu_result = "<img src=\"{{media url=\"wysiwyg/Core2Quad.png\"}} \" style= \"PADDING-LEFT: 8px\" align = \"right\" alt = \"Core 2 Quad\" ><p> This computer features an Intel Core 2 Quad processor. This processor is built to utilize multi - threaded applications, which is great for editing files, coding, or excessive multi - tasking.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the Intel Core 2 processor family let you get more done in less time, reducing energy costs by an average of 50 percent.</p><br><hr>";
            }

            else if (cpu.Contains("Celeron"))
            {
                cpu_result = "<p><img src = \"{{media url=\"wysiwyg/CeleronM.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"CeleronM\">This refurbished computer features a Celeron M processor.This processor delivers fast performance and steady processing power so you can remain productive.</p><br><hr> ";
            }
            else if (cpu.Contains("X2"))
            {
                cpu_result = "<img src=\"{ { media url = \"wysiwyg/AMDTurionX2.png\"} }\" style=\"PADDING - LEFT: 8px\" align=\"right\" alt=\"AMDTurionX2\"><p> This computer features an AMD Dual Core processor.This processor delivers solid performance, stability, and powerful on - board graphics.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the AMD Turion 64 X2 processor family help you get the most out of whatever virtual setting you're in.</p><br><hr>";
            }
            else if (cpu.Contains("A4"))
            {
                cpu_result = "<img src=\"{ { media url = \"wysiwyg/AMDA4.png\"} }\"style=\"PADDING - LEFT: 8px\" align=\"right\" alt=\"AMDA4\"><p> This computer features an AMD Dual Core processor.This processor delivers solid performance, stability, and powerful on - board graphics.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the AMD A4 processor family help you get the most out of whatever virtual setting you're in.</p><br><hr>";
            }
            else if (cpu.Contains("i3"))
            {
                cpu_result = "<p><img src = \"{{media url=\"wysiwyg/i3.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"Inteili3\">This refurbished computer features an Intel i3 processor.This processor delivers blindingly-fast performance, and multiple power states to ensure you're never wasting power.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the Intel 3 processor family let you excel in whatever task you're working on in a fraction of the time, while managing energy levels automatically.</p><br><hr> ";
            }
            else if (cpu.Contains("i5"))
            {
                cpu_result = "<p><img src = \"{{media url=\"wysiwyg/i5.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"Inteili5\">This refurbished computer features an Intel i5 processor.This processor delivers blindingly-fast performance, multiple power states to ensure you're never wasting power , and a new platform that boosts graphics, battery life and security.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the Intel i5 processor family let you excel in whatever task you're working on in a fraction of the time, while managing energy levels automatically.</p><br><hr> ";
            }
            else if (cpu.Contains("i7"))
            {
                cpu_result = "<p><img src = \"{{media url=\"wysiwyg/i7.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"i7\">This refurbished computer features an Intel i7 processor.This processor delivers blindingly-fast performance, multiple power states to ensure you're never wasting power , and a new platform that boosts graphics, battery life and security.<br><br>Combining mainstream processing speeds with power - saving features, PCs with the Intel i7 processor family let you excel in whatever task you're working on in a fraction of the time, while managing energy levels automatically.</p><br><hr> ";
            }
            cpu_result = cpu_result.Replace(@"\", "");

            return cpu_result;
        }
        public string hdd_filter(string hdd)
        {
            hdd = hdd.Replace("GB", "");
            hdd = hdd.Replace("Hard Drive", "");
            int temp_hdd = int.Parse(hdd);
            string hdd_result = "";

            if (temp_hdd  <= 60)
            {
                hdd_result = " <p><img src = \"{{media url=\"wysiwyg/HDD60GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"60GB HDD\">This refurbished computer comes with a 60GB HDD. A smaller hard drive will save you money, but is not recommended for power users. Computers with smaller hard drives are great for computer labs, libraries, and public use.For power users, we recommend at least a 250GB HDD.<br><br>We hold our hard drives to a higher standard than our competition. All of our drives score 99% or better on a SMART test. They are very reliable and are expected to perform well for years.</p><br><hr>";

            }
            else if (temp_hdd > 60 && temp_hdd <= 80 )
            {
                hdd_result = "<p><img src = \"{{media url=\"wysiwyg/HDD80GB.png\"}} \" style = \"PADDING LEFT: 8px\" align = \"right\" alt = \"80GB HDD\">This refurbished computer comes with a 80GB HDD. A smaller hard drive will save you money, but is not recommended for power users. Computers with smaller hard drives are great for computer labs, libraries, and public use.For power users, we recommend at least a 250GB HDD.<br><br>We hold our hard drives to a higher standard than our competition. All of our drives score 99% or better on a SMART test. They are very reliable and are expected to perform well for years.</p><br><hr>";
            }
            else if (temp_hdd > 80 && temp_hdd <= 100)
            {
                hdd_result = "<p><img src = \"{{media url=\"wysiwyg/HDD100GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"100GB HDD\">This refurbished computer comes with a 100GB HDD. A smaller hard drive will save you money, but is not recommended for power users. Computers with smaller hard drives are great for computer labs, libraries, and public use.For power users, we recommend at least a 250GB HDD.<br><br>We hold our hard drives to a higher standard than our competition. All of our drives score 99% or better on a SMART test. They are very reliable and are expected to perform well for years.</p><br><hr>";
            }
            else if (temp_hdd > 100 && temp_hdd <= 120)
            {
                hdd_result = " <p><img src = \"{{media url=\"wysiwyg/HDD120GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"120GB HDD\">This refurbished computer comes with a 120GB HDD. A smaller hard drive will save you money, but is not recommended for power users. Computers with smaller hard drives are great for computer labs, libraries, and public use.For power users, we recommend at least a 250GB HDD.<br><br>We hold our hard drives to a higher standard than our competition. All of our drives score 99% or better on a SMART test. They are very reliable and are expected to perform well for years.</p><br><hr>";
            }
            else if (temp_hdd > 120 && temp_hdd <= 160)
            {
                hdd_result = " <p><img src = \"{{media url=\"wysiwyg/HDD160GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"160GB HDD\">This refurbished computer comes with a 160GB HDD. A smaller hard drive will save you money, but is not recommended for power users. Computers with smaller hard drives are great for computer labs, libraries, and public use.For power users, we recommend at least a 160GB HDD.<br><br>We hold our hard drives to a higher standard than our competition. All of our drives score 99% or better on a SMART test. They are very reliable and are expected to perform well for years.</p><br><hr>";
            }
            else if (temp_hdd > 160 && temp_hdd <= 200)
            {
                hdd_result = " <p><img src = \"{{media url=\"wysiwyg/HDD200GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"200GB HDD\">This refurbished computer comes with a 200GB HDD. A smaller hard drive will save you money, but is not recommended for power users. Computers with smaller hard drives are great for computer labs, libraries, and public use.For power users, we recommend at least a 250GB HDD.<br><br>We hold our hard drives to a higher standard than our competition. All of our drives score 99% or better on a SMART test. They are very reliable and are expected to perform well for years.</p><br><hr>";
            }
            else if (temp_hdd > 200 && temp_hdd <= 250)
            {
                hdd_result = "<p><img src = \"{{media url=\"wysiwyg/HDD250GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"250GB HDD\">This refurbished computer comes with a 250GB HDD. This is enough space for most users. If you need more space, we recommend using an external hard drive. <br><br> We hold our hard drives to a higher standard than our competition.All of our drives score 99 % or better on a SMART test.They are very reliable and are expected to perform well for years.</p><br><br><hr>";
            }
            else if (temp_hdd > 250 && temp_hdd <= 320)
            {
                hdd_result = " <p><img src = \"{{media url=\"wysiwyg/HDD320GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"320GB HDD\">This refurbished computer comes with a 320GB HDD. This is enough space for most users. If you need more space, we recommend using an external hard drive. <br><br> We hold our hard drives to a higher standard than our competition.All of our drives score 99 % or better on a SMART test.They are very reliable and are expected to perform well for years.</p><br><br><hr> ";
            }
            else if (temp_hdd > 320 && temp_hdd <= 500)
            {
                hdd_result = "<p><img src = \"{{media url=\"wysiwyg/HDD500GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"500GB HDD\">This computer comes with a 500GB HDD. This is enough space for most users. If you need more space, we recommend using an external hard drive. <br><br> We hold our hard drives to a higher standard than our competition.All of our drives score 99 % or better on a SMART test.They are very reliable and should perform well for years.</p><br><br><hr> ";
            }
            else if (temp_hdd > 500 && temp_hdd <= 610)
            {
                hdd_result = "<p><img src = \"{{media url=\"wysiwyg/HDD610GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"610GB HDD\">This computer comes with a 610GB HDD. This is enough space for most users. If you need more space, we recommend using an external hard drive. <br><br> We hold our hard drives to a higher standard than our competition.All of our drives score 99 % or better on a SMART test.They are very reliable and should perform well for years.</p><br><br><hr> ";
            }
            else if (temp_hdd > 610 && temp_hdd <= 640)
            {
                hdd_result = "<p><img src = \"{{media url=\"wysiwyg/HDD640GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"640GB HDD\">This computer comes with a 640GB HDD. This is enough space for most users. If you need more space, we recommend using an external hard drive. <br><br> We hold our hard drives to a higher standard than our competition.All of our drives score 99 % or better on a SMART test.They are very reliable and should perform well for years.</p><br><br><hr> ";
            }
            else if (temp_hdd > 640 && temp_hdd <= 1000)
            {
                hdd_result = " <p><img src = \"{{media url=\"wysiwyg/HDD1TB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"320GB HDD\">This refurbished computer comes with a 1TB HDD. This is a large amount of storage capacity, and should be more than enough for most.If you need more space, we recommend using an external hard drive. <br><br> We hold our hard drives to a higher standard than our competition.All of our drives score 99 % or better on a SMART test.They are very reliable and are expected to perform well for years.</p><br><br><hr> ";
            }

            hdd_result = hdd_result.Replace(@"\", "");
            return hdd_result;
        }
        public string ram_filter(string ram)
        {
            ram = ram.Replace("GB", "");
            ram = ram.Replace("Memory", "");
            int temp_ram = int.Parse(ram);
            string ram_result = "";

            if (temp_ram < 1000) 
            {
                ram_result = "<p><img src = \"{{media url=\"wysiwyg/RAM1GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"1GB RAM\">This refurbished computer comes with 1GB of RAM.This is enough to run the Microsoft Office Suite, Internet browsers, and other basic programs. Power users would be better served by computers with 4GB of RAM or more.With only 1GB of RAM, switching between programs will be slower. <br><br>1GB of RAM is not recommended for photo or video editing.Please check the system requirements of any advanced software you plan on using.</p><br><hr> ";
            }
            else if (temp_ram > 1000 && temp_ram <= 2000)
            {
                ram_result = "<p><img src = \"{{media url=\"wysiwyg/RAM2GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"2GB RAM\">This refurbished computer comes with 2GB of RAM.This is enough to run the Microsoft Office Suite, Internet browsers, and other basic programs. Power users would be better served by computers with 4GB of RAM or more.With only 2GB of RAM, switching between programs will be slightly slower, but working within programs will be exactly the same.<br><br>2GB of RAM is not recommended for photo or video editing.Please check the system requirements of any advanced software you plan on using.</p><br><hr>";
            }
            else if (temp_ram > 2000 && temp_ram <= 3000)
            {
                ram_result = "<p><img src = \"{{media url=\"wysiwyg/RAM3GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"3GB RAM\">This refurbished computer comes with 3GB of RAM.This is enough to run the Microsoft Office Suite, Internet browsers, and other basic programs. Power users would be better served by computers with 4GB of RAM or more.With only 3GB of RAM, switching between programs will be slightly slower, but working within programs will be exactly the same.<br><br>3GB of RAM is not recommended for photo or video editing.Please check the system requirements of any advanced software you plan on using.</p><br><hr>";
            }
            else if (temp_ram > 3000 && temp_ram <= 4000)
            {
                ram_result = "<p><img src = \"{{media url=\"wysiwyg/RAM4GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"4GB RAM\">This refurbished computer comes with 4GB of RAM.This is enough to run pretty much any program without issue. Users that are doing large photo editing jobs or video editing would benefit from additional RAM, but for most users 4GB is sufficient.<br><br>If you’re unsure, please check the system requirements of any software you intend on using with this computer.</p><br><br><hr>";
            }
            else if (temp_ram > 4000 && temp_ram <= 6000)
            {
                ram_result = "<p><img src = \"{{media url=\"wysiwyg/RAM6GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"6GB RAM\">This refurbished computer comes with 6GB of RAM.This is enough to run pretty much any program without issue, including photo editing programs.<br><br>If you’re unsure if it is enough, please check the system requirements of any software you intend on using with this computer.</p><br><br><hr> ";
            }
            else if (temp_ram > 6000 && temp_ram <= 8000)
            {
                ram_result = "<p><img src = \"{{media url=\"wysiwyg/RAM8GB.png\"}} \" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"8GB RAM\">This refurbished computer comes with 8GB of RAM.This is enough to run any program without issue, including photo editing programs.<br><br></p><br><br><br><br><br><hr> ";
            }

            ram_result = ram_result.Replace(@"\", "");
            return ram_result;
        }
        public string ic_filter()
        {
            string ic = "<p><a href=\"http://interconnection.org/store/iccertified/\"><img src=\"{{media url=\"wysiwyg/ICCertified.png\"}}\" style = \"PADDING-LEFT: 8px\" align = \"right\" alt = \"IC Certified\"/></a>InterConnection(IC) is a leading provider of quality refurbished laptops and computers to nonprofit organizations and low - income individuals in the U.S.and to schools and non - governmental organizations around the world. We have developed a computer refurbishing process that ensures all computers pass a series of refurbishment stages and meet the highest quality control standards. All computers that meet our standards are deemed <a href = \"http://interconnection.org/store/iccertified/\" >“IC Certified”</a>, which is our symbol for quality and reliability.</p><br><hr>";

            ic = ic.Replace(@"\", "'");
            return ic;

        }
    }
}