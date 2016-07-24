using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ic_ef
{
    public static class Levenshtein
    {
        
        public static string compute_difference(string s, Dictionary<string, int> input)
        {
            //helper method that take a target string compare with a key pair list
            string title = "";
            int init = 0;
            foreach (KeyValuePair<string, int> pair in input)
            {
                int difference = Compute(s, pair.Key);
                if (init == 0)
                {
                    init = difference;
                    title = pair.Key;
                }
                if (difference < init)
                {
                    init = difference;
                    title = pair.Key;
                }

            }

            return title;
        }

        public static int Round(this int i, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (i + 5 * nearest / 10) / nearest * nearest;
        }
        public static Dictionary<string, int> brand_name()
        {
            Dictionary<string, int> brand_name = new Dictionary<string, int>();
            brand_name.Add("Dell", 1);
            brand_name.Add("Hewlett Packard", 2);
            brand_name.Add("Lenovo", 3);
            brand_name.Add("Apple", 4);
            brand_name.Add("Asus", 5);
            brand_name.Add("Acer", 6);
            brand_name.Add("Toshiba", 7);
            brand_name.Add("HP", 8);
            brand_name.Add("Sony@", 9); 
            brand_name.Add("FUJITSU", 10);
            return brand_name;
        }
        public static Dictionary<string, int> c2d_cpu_name_1ghz()
        {
            Dictionary<string, int> c2d_cpu_name_1ghz =
         new Dictionary<string, int>();
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.60GHz", 1);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.61GHz", 2);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.62GHz", 3);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.63GHz", 4);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.64GHz", 5);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.65GHz", 6);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.66GHz", 7);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.67GHz", 8);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.68GHz", 9);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.69GHz", 10);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.70GHz", 11);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.71GHz", 12);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.72GHz", 13);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.73GHz", 14);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.74GHz", 15);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.75GHz", 16);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.76GHz", 17);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.77GHz", 18);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.78GHz", 19);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.79GHz", 20);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.80GHz", 21);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.81GHz", 22);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.82GHz", 23);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.83GHz", 24);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.84GHz", 25);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.85GHz", 26);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.86GHz", 27);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.87GHz", 28);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.88GHz", 29);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.89GHz", 30);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.90GHz", 31);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.91GHz", 32);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.92GHz", 33);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.93GHz", 34);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.94GHz", 35);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.95GHz", 36);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.96GHz", 37);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.97GHz", 38);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.98GHz", 39);
            c2d_cpu_name_1ghz.Add("Core 2 Duo 1.99GHz", 40);
            return c2d_cpu_name_1ghz;
        }
        public static Dictionary<string, int> c2d_cpu_name_2ghz()
        {
            Dictionary<string, int> c2d_cpu_name_2ghz =
     new Dictionary<string, int>();
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.00GHz", 41);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.01GHz", 42);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.02GHz", 43);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.03GHz", 44);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.04GHz", 45);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.05GHz", 46);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.06GHz", 47);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.07GHz", 48);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.08GHz", 49);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.09GHz", 50);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.10GHz", 51);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.11GHz", 52);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.12GHz", 53);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.13GHz", 54);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.14GHz", 55);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.15GHz", 56);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.16GHz", 57);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.17GHz", 58);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.18GHz", 59);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.19GHz", 60);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.20GHz", 61);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.21GHz", 62);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.22GHz", 63);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.23GHz", 64);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.24GHz", 65);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.25GHz", 66);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.26GHz", 67);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.27GHz", 68);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.28GHz", 70);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.29GHz", 71);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.30GHz", 72);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.31GHz", 73);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.32GHz", 74);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.33GHz", 75);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.34GHz", 76);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.35GHz", 77);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.36GHz", 78);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.37GHz", 79);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.38GHz", 80);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.39GHz", 81);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.40GHz", 82);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.41GHz", 83);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.42GHz", 84);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.43GHz", 85);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.44GHz", 86);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.45GHz", 87);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.46GHz", 88);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.47GHz", 89);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.48GHz", 90);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.49GHz", 91);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.50GHz", 92);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.51GHz", 93);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.52GHz", 94);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.53GHz", 95);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.54GHz", 96);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.55GHz", 97);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.56GHz", 98);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.57GHz", 99);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.58GHz", 100);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.59GHz", 101);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.60GHz", 102);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.61GHz", 103);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.62GHz", 104);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.63GHz", 105);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.64GHz", 106);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.65GHz", 107);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.66GHz", 108);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.67GHz", 109);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.68GHz", 110);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.69GHz", 111);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.70GHz", 112);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.71GHz", 113);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.72GHz", 114);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.73GHz", 115);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.74GHz", 116);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.75GHz", 117);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.76GHz", 118);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.77GHz", 119);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.78GHz", 120);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.79GHz", 121);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.80GHz", 122);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.81GHz", 123);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.82GHz", 124);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.83GHz", 125);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.84GHz", 126);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.85GHz", 127);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.86GHz", 128);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.87GHz", 129);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.88GHz", 130);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.89GHz", 131);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.90GHz", 132);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.91GHz", 133);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.92GHz", 134);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.93GHz", 135);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.94GHz", 136);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.95GHz", 137);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.96GHz", 138);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.97GHz", 139);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.98GHz", 140);
            c2d_cpu_name_2ghz.Add("Core 2 Duo 2.99GHz", 141);
            return c2d_cpu_name_2ghz;
        }
        public static Dictionary<string, int> c2d_cpu_name_3ghz()
        {
            Dictionary<string, int> c2d_cpu_name_3ghz =
        new Dictionary<string, int>();


            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.00GHz", 142);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.01GHz", 143);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.02GHz", 144);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.03GHz", 145);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.04GHz", 146);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.05GHz", 147);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.06GHz", 148);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.07GHz", 149);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.08GHz", 150);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.09GHz", 151);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.10GHz", 152);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.11GHz", 153);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.12GHz", 154);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.13GHz", 155);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.14GHz", 156);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.15GHz", 157);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.16GHz", 158);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.17GHz", 159);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.18GHz", 160);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.19GHz", 161);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.20GHz", 162);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.21GHz", 163);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.22GHz", 164);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.23GHz", 165);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.24GHz", 166);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.25GHz", 167);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.26GHz", 168);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.27GHz", 169);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.28GHz", 170);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.29GHz", 171);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.30GHz", 172);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.31GHz", 173);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.32GHz", 174);
            c2d_cpu_name_3ghz.Add("Core 2 Duo 3.33GHz", 175);
            return c2d_cpu_name_3ghz;
        }
        public static string c2d_cpu(string s)
        {
            string title = "";
            Dictionary<string, int> c2d_cpu_speed =
        new Dictionary<string, int>();
            c2d_cpu_speed.Add("Core 2 Duo 1.", 1);
            c2d_cpu_speed.Add("Core 2 Duo 2.", 2);
            c2d_cpu_speed.Add("Core 2 Duo 3.", 3);

            string speed = compute_difference(s, c2d_cpu_speed);
            switch (speed)
            {
                case "Core 2 Duo 1.":
                    {
                        Dictionary<string, int> c2d_1ghz = c2d_cpu_name_1ghz();
                        title = compute_difference(s, c2d_1ghz);
                        break;
                    }
                case "Core 2 Duo 2.":
                    {
                        Dictionary<string, int> c2d_2ghz = c2d_cpu_name_2ghz();
                        title = compute_difference(s, c2d_2ghz);
                        break;
                    }
                case "Core 2 Duo 3.":
                    {
                        Dictionary<string, int> c2d_3ghz = c2d_cpu_name_3ghz();
                        title = compute_difference(s, c2d_3ghz);
                        break;
                    }
            }




            return title;
        }
        public static string i3_cpu(string s)
        {
            string title = "";
            Dictionary<string, int> i3_cpu_name =
       new Dictionary<string, int>();
            i3_cpu_name.Add("Core i3 2.50GHz", 176);
            i3_cpu_name.Add("Core i3 2.51GHz", 177);
            i3_cpu_name.Add("Core i3 2.52GHz", 178);
            i3_cpu_name.Add("Core i3 2.53GHz", 179);
            i3_cpu_name.Add("Core i3 2.54GHz", 180);
            i3_cpu_name.Add("Core i3 2.55GHz", 181);
            i3_cpu_name.Add("Core i3 2.56GHz", 182);
            i3_cpu_name.Add("Core i3 2.57GHz", 183);
            i3_cpu_name.Add("Core i3 2.58GHz", 184);
            i3_cpu_name.Add("Core i3 2.59GHz", 185);
            i3_cpu_name.Add("Core i3 2.60GHz", 186);
            i3_cpu_name.Add("Core i3 2.61GHz", 187);
            i3_cpu_name.Add("Core i3 2.62GHz", 188);
            i3_cpu_name.Add("Core i3 2.63GHz", 189);
            i3_cpu_name.Add("Core i3 2.64GHz", 190);
            i3_cpu_name.Add("Core i3 2.65GHz", 191);
            i3_cpu_name.Add("Core i3 2.66GHz", 192);
            i3_cpu_name.Add("Core i3 2.67GHz", 193);
            i3_cpu_name.Add("Core i3 2.68GHz", 194);
            i3_cpu_name.Add("Core i3 2.69GHz", 195);
            i3_cpu_name.Add("Core i3 2.70GHz", 196);
            i3_cpu_name.Add("Core i3 2.71GHz", 197);
            i3_cpu_name.Add("Core i3 2.72GHz", 198);
            i3_cpu_name.Add("Core i3 2.73GHz", 199);
            i3_cpu_name.Add("Core i3 2.74GHz", 200);
            i3_cpu_name.Add("Core i3 2.75GHz", 201);
            i3_cpu_name.Add("Core i3 2.76GHz", 202);
            i3_cpu_name.Add("Core i3 2.77GHz", 203);
            i3_cpu_name.Add("Core i3 2.78GHz", 204);
            i3_cpu_name.Add("Core i3 2.79GHz", 205);
            i3_cpu_name.Add("Core i3 2.80GHz", 206);
            i3_cpu_name.Add("Core i3 2.81GHz", 207);
            i3_cpu_name.Add("Core i3 2.82GHz", 208);
            i3_cpu_name.Add("Core i3 2.83GHz", 209);
            i3_cpu_name.Add("Core i3 2.84GHz", 210);
            i3_cpu_name.Add("Core i3 2.85GHz", 211);
            i3_cpu_name.Add("Core i3 2.86GHz", 212);
            i3_cpu_name.Add("Core i3 2.87GHz", 213);
            i3_cpu_name.Add("Core i3 2.88GHz", 214);
            i3_cpu_name.Add("Core i3 2.89GHz", 215);
            i3_cpu_name.Add("Core i3 2.90GHz", 216);
            i3_cpu_name.Add("Core i3 2.91GHz", 217);
            i3_cpu_name.Add("Core i3 2.92GHz", 218);
            i3_cpu_name.Add("Core i3 2.93GHz", 219);
            i3_cpu_name.Add("Core i3 2.94GHz", 220);
            i3_cpu_name.Add("Core i3 2.95GHz", 221);
            i3_cpu_name.Add("Core i3 2.96GHz", 222);
            i3_cpu_name.Add("Core i3 2.97GHz", 223);
            i3_cpu_name.Add("Core i3 2.98GHz", 224);
            i3_cpu_name.Add("Core i3 2.99GHz", 225);
            i3_cpu_name.Add("Core i3 3.00GHz", 226);
            i3_cpu_name.Add("Core i3 3.01GHz", 227);
            i3_cpu_name.Add("Core i3 3.02GHz", 228);
            
            i3_cpu_name.Add("Core i3 3.03GHz", 230);
            i3_cpu_name.Add("Core i3 3.04GHz", 231);
            i3_cpu_name.Add("Core i3 3.05GHz", 232);
            i3_cpu_name.Add("Core i3 3.06GHz", 233);
            i3_cpu_name.Add("Core i3 3.07GHz", 234);
            i3_cpu_name.Add("Core i3 3.08GHz", 235);
            i3_cpu_name.Add("Core i3 3.09GHz", 236);
            i3_cpu_name.Add("Core i3 3.10GHz", 237);
            i3_cpu_name.Add("Core i3 3.11GHz", 238);
            i3_cpu_name.Add("Core i3 3.12GHz", 239);
            i3_cpu_name.Add("Core i3 3.13GHz", 240);
            i3_cpu_name.Add("Core i3 3.14GHz", 241);
            i3_cpu_name.Add("Core i3 3.15GHz", 242);
            i3_cpu_name.Add("Core i3 3.16GHz", 243);
            i3_cpu_name.Add("Core i3 3.17GHz", 244);
            i3_cpu_name.Add("Core i3 3.18GHz", 245);
            i3_cpu_name.Add("Core i3 3.19GHz", 246);
            i3_cpu_name.Add("Core i3 3.20GHz", 247);
            i3_cpu_name.Add("Core i3 3.21GHz", 248);
            i3_cpu_name.Add("Core i3 3.22GHz", 249);
            i3_cpu_name.Add("Core i3 3.23GHz", 250);
            i3_cpu_name.Add("Core i3 3.24GHz", 251);
            i3_cpu_name.Add("Core i3 3.25GHz", 252);
            i3_cpu_name.Add("Core i3 3.26GHz", 253);
            i3_cpu_name.Add("Core i3 3.27GHz", 254);
            i3_cpu_name.Add("Core i3 3.28GHz", 255);
            i3_cpu_name.Add("Core i3 3.29GHz", 256);
            i3_cpu_name.Add("Core i3 3.30GHz", 257);
            i3_cpu_name.Add("Core i3 3.31GHz", 258);
            i3_cpu_name.Add("Core i3 3.32GHz", 259);
            i3_cpu_name.Add("Core i3 3.33GHz", 260);
            i3_cpu_name.Add("Core i3 3.34GHz", 261);
            i3_cpu_name.Add("Core i3 3.35GHz", 262);
            i3_cpu_name.Add("Core i3 3.36GHz", 263);
            i3_cpu_name.Add("Core i3 3.37GHz", 264);
            i3_cpu_name.Add("Core i3 3.38GHz", 265);
            i3_cpu_name.Add("Core i3 3.39GHz", 266);
            i3_cpu_name.Add("Core i3 3.40GHz", 267);
            i3_cpu_name.Add("Core i3 3.41GHz", 268);
            i3_cpu_name.Add("Core i3 3.42GHz", 269);
            i3_cpu_name.Add("Core i3 3.43GHz", 270);
            i3_cpu_name.Add("Core i3 3.44GHz", 271);
            i3_cpu_name.Add("Core i3 3.45GHz", 272);
            i3_cpu_name.Add("Core i3 3.46GHz", 273);
            i3_cpu_name.Add("Core i3 3.47GHz", 274);
            i3_cpu_name.Add("Core i3 3.48GHz", 275);
            i3_cpu_name.Add("Core i3 3.49GHz", 276);
            i3_cpu_name.Add("Core i3 3.50GHz", 277);
            i3_cpu_name.Add("Core i3 3.51GHz", 278);
            i3_cpu_name.Add("Core i3 3.52GHz", 279);
            i3_cpu_name.Add("Core i3 3.53GHz", 280);
            i3_cpu_name.Add("Core i3 3.54GHz", 281);
            i3_cpu_name.Add("Core i3 3.55GHz", 282);
            i3_cpu_name.Add("Core i3 3.56GHz", 283);
            i3_cpu_name.Add("Core i3 3.57GHz", 284);
            i3_cpu_name.Add("Core i3 3.58GHz", 285);
            i3_cpu_name.Add("Core i3 3.59GHz", 286);
            i3_cpu_name.Add("Core i3 3.60GHz", 287);
            i3_cpu_name.Add("Core i3 3.61GHz", 288);
            i3_cpu_name.Add("Core i3 3.62GHz", 289);
            i3_cpu_name.Add("Core i3 3.63GHz", 290);
            i3_cpu_name.Add("Core i3 3.64GHz", 291);
            i3_cpu_name.Add("Core i3 3.65GHz", 292);
            i3_cpu_name.Add("Core i3 3.66GHz", 293);
            i3_cpu_name.Add("Core i3 3.67GHz", 294);
            i3_cpu_name.Add("Core i3 3.68GHz", 295);
            i3_cpu_name.Add("Core i3 3.69GHz", 296);
            i3_cpu_name.Add("Core i3 3.70GHz", 297);
            i3_cpu_name.Add("Core i3 3.71GHz", 298);
            i3_cpu_name.Add("Core i3 3.72GHz", 299);
            i3_cpu_name.Add("Core i3 3.73GHz", 300);
            i3_cpu_name.Add("Core i3 3.74GHz", 301);
            i3_cpu_name.Add("Core i3 3.75GHz", 302);
            i3_cpu_name.Add("Core i3 3.76GHz", 303);
            i3_cpu_name.Add("Core i3 3.77GHz", 304);
            i3_cpu_name.Add("Core i3 3.78GHz", 305);
            i3_cpu_name.Add("Core i3 3.79GHz", 306);
            i3_cpu_name.Add("Core i3 3.80GHz", 307);
            i3_cpu_name.Add("Core i3 3.81GHz", 308);
            i3_cpu_name.Add("Core i3 3.82GHz", 309);
            i3_cpu_name.Add("Core i3 3.83GHz", 310);
            i3_cpu_name.Add("Core i3 3.84GHz", 311);
            i3_cpu_name.Add("Core i3 3.85GHz", 312);
            i3_cpu_name.Add("Core i3 3.86GHz", 313);
            i3_cpu_name.Add("Core i3 3.87GHz", 314);
            i3_cpu_name.Add("Core i3 3.88GHz", 315);
            i3_cpu_name.Add("Core i3 3.89GHz", 316);
            i3_cpu_name.Add("Core i3 3.90GHz", 317);
            title = compute_difference(s, i3_cpu_name);
            return title;
        }
        public static string i5_cpu(string s)
        {
            string title = "";
            Dictionary<string, int> i5_cpu_name =
       new Dictionary<string, int>();
            i5_cpu_name.Add("Core i5 2.30GHz", 318);
            i5_cpu_name.Add("Core i5 2.31GHz", 319);
            i5_cpu_name.Add("Core i5 2.32GHz", 320);
            i5_cpu_name.Add("Core i5 2.33GHz", 321);
            i5_cpu_name.Add("Core i5 2.34GHz", 322);
            i5_cpu_name.Add("Core i5 2.35GHz", 323);
            i5_cpu_name.Add("Core i5 2.36GHz", 324);
            i5_cpu_name.Add("Core i5 2.37GHz", 325);
            i5_cpu_name.Add("Core i5 2.38GHz", 326);
            i5_cpu_name.Add("Core i5 2.39GHz", 327);
            i5_cpu_name.Add("Core i5 2.40GHz", 328);
            i5_cpu_name.Add("Core i5 2.41GHz", 329);
            i5_cpu_name.Add("Core i5 2.42GHz", 330);
            i5_cpu_name.Add("Core i5 2.43GHz", 331);
            i5_cpu_name.Add("Core i5 2.44GHz", 332);
            i5_cpu_name.Add("Core i5 2.45GHz", 333);
            i5_cpu_name.Add("Core i5 2.46GHz", 334);
            i5_cpu_name.Add("Core i5 2.47GHz", 335);
            i5_cpu_name.Add("Core i5 2.48GHz", 336);
            i5_cpu_name.Add("Core i5 2.49GHz", 337);
            i5_cpu_name.Add("Core i5 2.50GHz", 338);
            i5_cpu_name.Add("Core i5 2.51GHz", 339);
            i5_cpu_name.Add("Core i5 2.52GHz", 340);
            i5_cpu_name.Add("Core i5 2.53GHz", 341);
            i5_cpu_name.Add("Core i5 2.54GHz", 342);
            i5_cpu_name.Add("Core i5 2.55GHz", 343);
            i5_cpu_name.Add("Core i5 2.56GHz", 344);
            i5_cpu_name.Add("Core i5 2.57GHz", 345);
            i5_cpu_name.Add("Core i5 2.58GHz", 346);
            i5_cpu_name.Add("Core i5 2.59GHz", 347);
            i5_cpu_name.Add("Core i5 2.60GHz", 348);
            i5_cpu_name.Add("Core i5 2.61GHz", 349);
            i5_cpu_name.Add("Core i5 2.62GHz", 350);
            i5_cpu_name.Add("Core i5 2.63GHz", 351);
            i5_cpu_name.Add("Core i5 2.64GHz", 352);
            i5_cpu_name.Add("Core i5 2.65GHz", 353);
            i5_cpu_name.Add("Core i5 2.66GHz", 354);
            i5_cpu_name.Add("Core i5 2.67GHz", 355);
            i5_cpu_name.Add("Core i5 2.68GHz", 356);
            i5_cpu_name.Add("Core i5 2.69GHz", 357);
            i5_cpu_name.Add("Core i5 2.70GHz", 358);
            i5_cpu_name.Add("Core i5 2.71GHz", 359);
            i5_cpu_name.Add("Core i5 2.72GHz", 360);
            i5_cpu_name.Add("Core i5 2.73GHz", 361);
            i5_cpu_name.Add("Core i5 2.74GHz", 362);
            i5_cpu_name.Add("Core i5 2.75GHz", 363);
            i5_cpu_name.Add("Core i5 2.76GHz", 364);
            i5_cpu_name.Add("Core i5 2.77GHz", 365);
         
            i5_cpu_name.Add("Core i5 2.78GHz", 367);
            i5_cpu_name.Add("Core i5 2.79GHz", 368);
            i5_cpu_name.Add("Core i5 2.80GHz", 369);
            i5_cpu_name.Add("Core i5 2.81GHz", 370);
            i5_cpu_name.Add("Core i5 2.82GHz", 371);
            i5_cpu_name.Add("Core i5 2.83GHz", 372);
            i5_cpu_name.Add("Core i5 2.84GHz", 373);
            i5_cpu_name.Add("Core i5 2.85GHz", 374);
            i5_cpu_name.Add("Core i5 2.86GHz", 375);
            i5_cpu_name.Add("Core i5 2.87GHz", 376);
            i5_cpu_name.Add("Core i5 2.88GHz", 377);
            i5_cpu_name.Add("Core i5 2.89GHz", 378);
            i5_cpu_name.Add("Core i5 2.90GHz", 379);
            i5_cpu_name.Add("Core i5 2.91GHz", 380);
            i5_cpu_name.Add("Core i5 2.92GHz", 381);
            i5_cpu_name.Add("Core i5 2.93GHz", 382);
            i5_cpu_name.Add("Core i5 2.94GHz", 383);
            i5_cpu_name.Add("Core i5 2.95GHz", 384);
            i5_cpu_name.Add("Core i5 2.96GHz", 385);
            i5_cpu_name.Add("Core i5 2.97GHz", 386);
            i5_cpu_name.Add("Core i5 2.98GHz", 387);
            i5_cpu_name.Add("Core i5 2.99GHz", 388);
            i5_cpu_name.Add("Core i5 3.00GHz", 389);
            i5_cpu_name.Add("Core i5 3.01GHz", 390);
            i5_cpu_name.Add("Core i5 3.02GHz", 391);
            i5_cpu_name.Add("Core i5 3.03GHz", 392);
            i5_cpu_name.Add("Core i5 3.04GHz", 393);
            i5_cpu_name.Add("Core i5 3.05GHz", 394);
            i5_cpu_name.Add("Core i5 3.06GHz", 395);
            i5_cpu_name.Add("Core i5 3.07GHz", 396);
            i5_cpu_name.Add("Core i5 3.08GHz", 397);
            i5_cpu_name.Add("Core i5 3.09GHz", 398);
            i5_cpu_name.Add("Core i5 3.10GHz", 399);
            i5_cpu_name.Add("Core i5 3.11GHz", 400);
            i5_cpu_name.Add("Core i5 3.12GHz", 401);
            i5_cpu_name.Add("Core i5 3.13GHz", 402);
            i5_cpu_name.Add("Core i5 3.14GHz", 403);
            i5_cpu_name.Add("Core i5 3.15GHz", 404);
            i5_cpu_name.Add("Core i5 3.16GHz", 405);
            i5_cpu_name.Add("Core i5 3.17GHz", 406);
            i5_cpu_name.Add("Core i5 3.18GHz", 407);
            i5_cpu_name.Add("Core i5 3.19GHz", 408);
            i5_cpu_name.Add("Core i5 3.20GHz", 409);
            i5_cpu_name.Add("Core i5 3.21GHz", 410);
            i5_cpu_name.Add("Core i5 3.22GHz", 411);
            i5_cpu_name.Add("Core i5 3.23GHz", 412);
            i5_cpu_name.Add("Core i5 3.24GHz", 413);
            i5_cpu_name.Add("Core i5 3.25GHz", 414);
            i5_cpu_name.Add("Core i5 3.26GHz", 415);
            i5_cpu_name.Add("Core i5 3.27GHz", 416);
            i5_cpu_name.Add("Core i5 3.28GHz", 417);
            i5_cpu_name.Add("Core i5 3.29GHz", 418);
            i5_cpu_name.Add("Core i5 3.30GHz", 419);
            i5_cpu_name.Add("Core i5 3.31GHz", 420);
            i5_cpu_name.Add("Core i5 3.32GHz", 421);
            i5_cpu_name.Add("Core i5 3.33GHz", 422);
            i5_cpu_name.Add("Core i5 3.34GHz", 423);
            i5_cpu_name.Add("Core i5 3.35GHz", 424);
            i5_cpu_name.Add("Core i5 3.36GHz", 425);
            i5_cpu_name.Add("Core i5 3.37GHz", 426);
            i5_cpu_name.Add("Core i5 3.38GHz", 427);
            i5_cpu_name.Add("Core i5 3.39GHz", 428);
            i5_cpu_name.Add("Core i5 3.40GHz", 429);
            i5_cpu_name.Add("Core i5 3.41GHz", 430);
            i5_cpu_name.Add("Core i5 3.42GHz", 431);
            i5_cpu_name.Add("Core i5 3.43GHz", 432);
            i5_cpu_name.Add("Core i5 3.44GHz", 433);
            i5_cpu_name.Add("Core i5 3.45GHz", 434);
            i5_cpu_name.Add("Core i5 3.46GHz", 435);
            i5_cpu_name.Add("Core i5 3.47GHz", 436);
            i5_cpu_name.Add("Core i5 3.48GHz", 437);
            i5_cpu_name.Add("Core i5 3.49GHz", 438);
            i5_cpu_name.Add("Core i5 3.50GHz", 439);
            i5_cpu_name.Add("Core i5 3.51GHz", 440);
            i5_cpu_name.Add("Core i5 3.52GHz", 441);
            i5_cpu_name.Add("Core i5 3.53GHz", 442);
            i5_cpu_name.Add("Core i5 3.54GHz", 443);
            i5_cpu_name.Add("Core i5 3.55GHz", 444);
            i5_cpu_name.Add("Core i5 3.56GHz", 445);
            i5_cpu_name.Add("Core i5 3.57GHz", 446);
            i5_cpu_name.Add("Core i5 3.58GHz", 447);
            i5_cpu_name.Add("Core i5 3.59GHz", 448);
            i5_cpu_name.Add("Core i5 3.60GHz", 449);
            i5_cpu_name.Add("Core i5 3.61GHz", 450);
            i5_cpu_name.Add("Core i5 3.62GHz", 451);
            i5_cpu_name.Add("Core i5 3.63GHz", 452);
            i5_cpu_name.Add("Core i5 3.64GHz", 453);
            i5_cpu_name.Add("Core i5 3.65GHz", 454);
            i5_cpu_name.Add("Core i5 3.66GHz", 455);
            i5_cpu_name.Add("Core i5 3.67GHz", 456);
            i5_cpu_name.Add("Core i5 3.68GHz", 457);
            i5_cpu_name.Add("Core i5 3.69GHz", 458);
            i5_cpu_name.Add("Core i5 3.70GHz", 459);
            i5_cpu_name.Add("Core i5 3.71GHz", 460);
            i5_cpu_name.Add("Core i5 3.72GHz", 461);
            i5_cpu_name.Add("Core i5 3.73GHz", 462);
            i5_cpu_name.Add("Core i5 3.74GHz", 463);
            i5_cpu_name.Add("Core i5 3.75GHz", 464);
            i5_cpu_name.Add("Core i5 3.76GHz", 465);
            i5_cpu_name.Add("Core i5 3.77GHz", 466);
            i5_cpu_name.Add("Core i5 3.78GHz", 467);
            i5_cpu_name.Add("Core i5 3.79GHz", 468);
            i5_cpu_name.Add("Core i5 3.80GHz", 469);
            i5_cpu_name.Add("Core i5 3.81GHz", 470);
            i5_cpu_name.Add("Core i5 3.82GHz", 471);
            i5_cpu_name.Add("Core i5 3.83GHz", 472);
            i5_cpu_name.Add("Core i5 3.84GHz", 473);
            i5_cpu_name.Add("Core i5 3.85GHz", 474);
            i5_cpu_name.Add("Core i5 3.86GHz", 475);
            i5_cpu_name.Add("Core i5 3.87GHz", 476);
            i5_cpu_name.Add("Core i5 3.88GHz", 477);
            title = compute_difference(s, i5_cpu_name);
            return title;
        }
        public static string i7_cpu(string s)
        {
            string title = "";

            Dictionary<string, int> i7_cpu_name =
       new Dictionary<string, int>();
            i7_cpu_name.Add("Core i7 1.60GHz", 667);
            i7_cpu_name.Add("Core i7 1.61GHz", 668);
            i7_cpu_name.Add("Core i7 1.62GHz", 669);
            i7_cpu_name.Add("Core i7 1.63GHz", 670);
            i7_cpu_name.Add("Core i7 1.64GHz", 671);
            i7_cpu_name.Add("Core i7 1.65GHz", 672);
            i7_cpu_name.Add("Core i7 1.66GHz", 673);
            i7_cpu_name.Add("Core i7 1.67GHz", 674);
            i7_cpu_name.Add("Core i7 1.68GHz", 675);
            i7_cpu_name.Add("Core i7 1.69GHz", 676);
            i7_cpu_name.Add("Core i7 1.70GHz", 677);
            i7_cpu_name.Add("Core i7 1.71GHz", 678);
            i7_cpu_name.Add("Core i7 1.72GHz", 679);
            i7_cpu_name.Add("Core i7 1.73GHz", 680);
            i7_cpu_name.Add("Core i7 1.74GHz", 681);
            i7_cpu_name.Add("Core i7 1.75GHz", 682);
            i7_cpu_name.Add("Core i7 1.76GHz", 683);
            i7_cpu_name.Add("Core i7 1.77GHz", 684);
            i7_cpu_name.Add("Core i7 1.78GHz", 685);
            i7_cpu_name.Add("Core i7 1.79GHz", 686);
            i7_cpu_name.Add("Core i7 1.80GHz", 687);
            i7_cpu_name.Add("Core i7 1.81GHz", 688);
            i7_cpu_name.Add("Core i7 1.82GHz", 689);
            i7_cpu_name.Add("Core i7 1.83GHz", 690);
            i7_cpu_name.Add("Core i7 1.84GHz", 691);
            i7_cpu_name.Add("Core i7 1.85GHz", 692);
            i7_cpu_name.Add("Core i7 1.86GHz", 693);
            i7_cpu_name.Add("Core i7 1.87GHz", 694);
            i7_cpu_name.Add("Core i7 1.88GHz", 695);
            i7_cpu_name.Add("Core i7 1.89GHz", 696);
            i7_cpu_name.Add("Core i7 1.90GHz", 697);
            i7_cpu_name.Add("Core i7 1.91GHz", 698);
            i7_cpu_name.Add("Core i7 1.92GHz", 699);
            i7_cpu_name.Add("Core i7 1.93GHz", 700);
            i7_cpu_name.Add("Core i7 1.94GHz", 701);
            i7_cpu_name.Add("Core i7 1.95GHz", 702);
            i7_cpu_name.Add("Core i7 1.96GHz", 703);
            i7_cpu_name.Add("Core i7 1.97GHz", 704);
            i7_cpu_name.Add("Core i7 1.98GHz", 705);
            i7_cpu_name.Add("Core i7 1.99GHz", 706);
            i7_cpu_name.Add("Core i7 2.00GHz", 707);
            i7_cpu_name.Add("Core i7 2.01GHz", 708);
            i7_cpu_name.Add("Core i7 2.02GHz", 709);
            i7_cpu_name.Add("Core i7 2.03GHz", 710);
            i7_cpu_name.Add("Core i7 2.04GHz", 711);
            i7_cpu_name.Add("Core i7 2.05GHz", 712);
            i7_cpu_name.Add("Core i7 2.06GHz", 713);
            i7_cpu_name.Add("Core i7 2.07GHz", 714);
            i7_cpu_name.Add("Core i7 2.08GHz", 715);
            i7_cpu_name.Add("Core i7 2.09GHz", 716);
            i7_cpu_name.Add("Core i7 2.10GHz", 717);
            i7_cpu_name.Add("Core i7 2.11GHz", 718);
            i7_cpu_name.Add("Core i7 2.12GHz", 719);
            i7_cpu_name.Add("Core i7 2.13GHz", 720);
            i7_cpu_name.Add("Core i7 2.14GHz", 721);
            i7_cpu_name.Add("Core i7 2.15GHz", 722);
            i7_cpu_name.Add("Core i7 2.16GHz", 723);
            i7_cpu_name.Add("Core i7 2.17GHz", 724);
            i7_cpu_name.Add("Core i7 2.18GHz", 725);
            i7_cpu_name.Add("Core i7 2.19GHz", 726);
            i7_cpu_name.Add("Core i7 2.20GHz", 727);
            i7_cpu_name.Add("Core i7 2.21GHz", 728);
            i7_cpu_name.Add("Core i7 2.22GHz", 729);
            i7_cpu_name.Add("Core i7 2.23GHz", 730);
            i7_cpu_name.Add("Core i7 2.24GHz", 731);
            i7_cpu_name.Add("Core i7 2.25GHz", 732);
            i7_cpu_name.Add("Core i7 2.26GHz", 733);
            i7_cpu_name.Add("Core i7 2.27GHz", 734);
            i7_cpu_name.Add("Core i7 2.28GHz", 736);
            i7_cpu_name.Add("Core i7 2.29GHz", 737);
            i7_cpu_name.Add("Core i7 2.30GHz", 738);
            i7_cpu_name.Add("Core i7 2.31GHz", 739);
            i7_cpu_name.Add("Core i7 2.32GHz", 740);
            i7_cpu_name.Add("Core i7 2.33GHz", 741);
            i7_cpu_name.Add("Core i7 2.34GHz", 742);
            i7_cpu_name.Add("Core i7 2.35GHz", 743);
            i7_cpu_name.Add("Core i7 2.36GHz", 744);
            i7_cpu_name.Add("Core i7 2.37GHz", 745);
            i7_cpu_name.Add("Core i7 2.38GHz", 746);
            i7_cpu_name.Add("Core i7 2.39GHz", 747);
            i7_cpu_name.Add("Core i7 2.40GHz", 748);
            i7_cpu_name.Add("Core i7 2.41GHz", 749);
            i7_cpu_name.Add("Core i7 2.42GHz", 750);
            i7_cpu_name.Add("Core i7 2.43GHz", 751);
            i7_cpu_name.Add("Core i7 2.44GHz", 752);
            i7_cpu_name.Add("Core i7 2.45GHz", 753);
            i7_cpu_name.Add("Core i7 2.46GHz", 754);
            i7_cpu_name.Add("Core i7 2.47GHz", 755);
            i7_cpu_name.Add("Core i7 2.48GHz", 756);
            i7_cpu_name.Add("Core i7 2.49GHz", 757);
            i7_cpu_name.Add("Core i7 2.50GHz", 758);
            i7_cpu_name.Add("Core i7 2.51GHz", 759);
            i7_cpu_name.Add("Core i7 2.52GHz", 760);
            i7_cpu_name.Add("Core i7 2.53GHz", 479);
            i7_cpu_name.Add("Core i7 2.54GHz", 480);
            i7_cpu_name.Add("Core i7 2.55GHz", 481);
            i7_cpu_name.Add("Core i7 2.56GHz", 482);
            i7_cpu_name.Add("Core i7 2.57GHz", 483);
            i7_cpu_name.Add("Core i7 2.58GHz", 484);
            i7_cpu_name.Add("Core i7 2.59GHz", 485);
            i7_cpu_name.Add("Core i7 2.60GHz", 486);
            i7_cpu_name.Add("Core i7 2.61GHz", 487);
            i7_cpu_name.Add("Core i7 2.62GHz", 488);
            i7_cpu_name.Add("Core i7 2.63GHz", 489);
            i7_cpu_name.Add("Core i7 2.64GHz", 490);
            i7_cpu_name.Add("Core i7 2.65GHz", 491);
            i7_cpu_name.Add("Core i7 2.66GHz", 492);
            i7_cpu_name.Add("Core i7 2.67GHz", 493);
            i7_cpu_name.Add("Core i7 2.68GHz", 494);
            i7_cpu_name.Add("Core i7 2.69GHz", 495);
            i7_cpu_name.Add("Core i7 2.70GHz", 496);
            i7_cpu_name.Add("Core i7 2.71GHz", 497);
            i7_cpu_name.Add("Core i7 2.72GHz", 498);
            i7_cpu_name.Add("Core i7 2.73GHz", 499);
            i7_cpu_name.Add("Core i7 2.74GHz", 500);
            i7_cpu_name.Add("Core i7 2.75GHz", 501);
            i7_cpu_name.Add("Core i7 2.76GHz", 502);
            i7_cpu_name.Add("Core i7 2.77GHz", 503);
            i7_cpu_name.Add("Core i7 2.78GHz", 504);
            i7_cpu_name.Add("Core i7 2.79GHz", 505);
            i7_cpu_name.Add("Core i7 2.80GHz", 506);
            i7_cpu_name.Add("Core i7 2.81GHz", 507);
            i7_cpu_name.Add("Core i7 2.82GHz", 508);
            i7_cpu_name.Add("Core i7 2.83GHz", 509);
            i7_cpu_name.Add("Core i7 2.84GHz", 510);
            i7_cpu_name.Add("Core i7 2.85GHz", 511);
            i7_cpu_name.Add("Core i7 2.86GHz", 512);
            i7_cpu_name.Add("Core i7 2.87GHz", 513);
            i7_cpu_name.Add("Core i7 2.88GHz", 514);
            i7_cpu_name.Add("Core i7 2.89GHz", 515);
            i7_cpu_name.Add("Core i7 2.90GHz", 516);
            i7_cpu_name.Add("Core i7 2.91GHz", 517);
            i7_cpu_name.Add("Core i7 2.92GHz", 518);
            i7_cpu_name.Add("Core i7 2.93GHz", 519);
            i7_cpu_name.Add("Core i7 2.94GHz", 520);
            i7_cpu_name.Add("Core i7 2.95GHz", 521);
            i7_cpu_name.Add("Core i7 2.96GHz", 522);
            i7_cpu_name.Add("Core i7 2.97GHz", 523);
            i7_cpu_name.Add("Core i7 2.98GHz", 524);
            i7_cpu_name.Add("Core i7 2.99GHz", 525);
            i7_cpu_name.Add("Core i7 3.00GHz", 526);
            i7_cpu_name.Add("Core i7 3.01GHz", 527);
            i7_cpu_name.Add("Core i7 3.02GHz", 528);

            i7_cpu_name.Add("Core i7 3.03GHz", 530);
            i7_cpu_name.Add("Core i7 3.04GHz", 531);
            i7_cpu_name.Add("Core i7 3.05GHz", 532);
            i7_cpu_name.Add("Core i7 3.06GHz", 533);
            i7_cpu_name.Add("Core i7 3.07GHz", 534);
            i7_cpu_name.Add("Core i7 3.08GHz", 535);
            i7_cpu_name.Add("Core i7 3.09GHz", 536);
            i7_cpu_name.Add("Core i7 3.10GHz", 537);
            i7_cpu_name.Add("Core i7 3.11GHz", 538);
            i7_cpu_name.Add("Core i7 3.12GHz", 539);
            i7_cpu_name.Add("Core i7 3.13GHz", 540);
            i7_cpu_name.Add("Core i7 3.14GHz", 541);
            i7_cpu_name.Add("Core i7 3.15GHz", 542);
            i7_cpu_name.Add("Core i7 3.16GHz", 543);
            i7_cpu_name.Add("Core i7 3.17GHz", 544);
            i7_cpu_name.Add("Core i7 3.18GHz", 545);
            i7_cpu_name.Add("Core i7 3.19GHz", 546);
            i7_cpu_name.Add("Core i7 3.20GHz", 547);
            i7_cpu_name.Add("Core i7 3.21GHz", 548);
            i7_cpu_name.Add("Core i7 3.22GHz", 549);
            i7_cpu_name.Add("Core i7 3.23GHz", 550);
            i7_cpu_name.Add("Core i7 3.24GHz", 551);
            i7_cpu_name.Add("Core i7 3.25GHz", 552);
            i7_cpu_name.Add("Core i7 3.26GHz", 553);
            i7_cpu_name.Add("Core i7 3.27GHz", 554);
            i7_cpu_name.Add("Core i7 3.28GHz", 555);
            i7_cpu_name.Add("Core i7 3.29GHz", 556);
            i7_cpu_name.Add("Core i7 3.30GHz", 557);
            i7_cpu_name.Add("Core i7 3.31GHz", 558);
            i7_cpu_name.Add("Core i7 3.32GHz", 559);
            i7_cpu_name.Add("Core i7 3.33GHz", 560);
            i7_cpu_name.Add("Core i7 3.34GHz", 561);
            i7_cpu_name.Add("Core i7 3.35GHz", 562);
            i7_cpu_name.Add("Core i7 3.36GHz", 563);
            i7_cpu_name.Add("Core i7 3.37GHz", 564);
            i7_cpu_name.Add("Core i7 3.38GHz", 565);
            i7_cpu_name.Add("Core i7 3.39GHz", 566);
            i7_cpu_name.Add("Core i7 3.40GHz", 567);
            i7_cpu_name.Add("Core i7 3.41GHz", 568);
            i7_cpu_name.Add("Core i7 3.42GHz", 569);
            i7_cpu_name.Add("Core i7 3.43GHz", 570);
            i7_cpu_name.Add("Core i7 3.44GHz", 571);
            i7_cpu_name.Add("Core i7 3.45GHz", 572);
            i7_cpu_name.Add("Core i7 3.46GHz", 573);
            i7_cpu_name.Add("Core i7 3.47GHz", 574);
            i7_cpu_name.Add("Core i7 3.48GHz", 575);
            i7_cpu_name.Add("Core i7 3.49GHz", 576);
            i7_cpu_name.Add("Core i7 3.50GHz", 577);
            i7_cpu_name.Add("Core i7 3.51GHz", 578);
            i7_cpu_name.Add("Core i7 3.52GHz", 579);
            i7_cpu_name.Add("Core i7 3.53GHz", 580);
            i7_cpu_name.Add("Core i7 3.54GHz", 581);
            i7_cpu_name.Add("Core i7 3.55GHz", 582);
            i7_cpu_name.Add("Core i7 3.56GHz", 583);
            i7_cpu_name.Add("Core i7 3.57GHz", 584);
            i7_cpu_name.Add("Core i7 3.58GHz", 585);
            i7_cpu_name.Add("Core i7 3.59GHz", 586);
            i7_cpu_name.Add("Core i7 3.60GHz", 587);
            i7_cpu_name.Add("Core i7 3.61GHz", 588);
            i7_cpu_name.Add("Core i7 3.62GHz", 589);
            i7_cpu_name.Add("Core i7 3.63GHz", 590);
            i7_cpu_name.Add("Core i7 3.64GHz", 591);
            i7_cpu_name.Add("Core i7 3.65GHz", 592);
            i7_cpu_name.Add("Core i7 3.66GHz", 593);
            i7_cpu_name.Add("Core i7 3.67GHz", 594);
            i7_cpu_name.Add("Core i7 3.68GHz", 595);
            i7_cpu_name.Add("Core i7 3.69GHz", 596);
            i7_cpu_name.Add("Core i7 3.70GHz", 597);
            i7_cpu_name.Add("Core i7 3.71GHz", 598);
            i7_cpu_name.Add("Core i7 3.72GHz", 599);
            i7_cpu_name.Add("Core i7 3.73GHz", 600);
            i7_cpu_name.Add("Core i7 3.74GHz", 601);
            i7_cpu_name.Add("Core i7 3.75GHz", 602);
            i7_cpu_name.Add("Core i7 3.76GHz", 603);
            i7_cpu_name.Add("Core i7 3.77GHz", 604);
            i7_cpu_name.Add("Core i7 3.78GHz", 605);
            i7_cpu_name.Add("Core i7 3.79GHz", 606);
            i7_cpu_name.Add("Core i7 3.80GHz", 607);
            i7_cpu_name.Add("Core i7 3.81GHz", 608);
            i7_cpu_name.Add("Core i7 3.82GHz", 609);
            i7_cpu_name.Add("Core i7 3.83GHz", 610);
            i7_cpu_name.Add("Core i7 3.84GHz", 611);
            i7_cpu_name.Add("Core i7 3.85GHz", 612);
            i7_cpu_name.Add("Core i7 3.86GHz", 613);
            i7_cpu_name.Add("Core i7 3.87GHz", 614);
            i7_cpu_name.Add("Core i7 3.88GHz", 615);
            i7_cpu_name.Add("Core i7 3.89GHz", 616);
            i7_cpu_name.Add("Core i7 3.90GHz", 617);
            i7_cpu_name.Add("Core i7 3.91GHz", 618);
            i7_cpu_name.Add("Core i7 3.92GHz", 619);
            i7_cpu_name.Add("Core i7 3.93GHz", 620);
            i7_cpu_name.Add("Core i7 3.94GHz", 621);
            i7_cpu_name.Add("Core i7 3.95GHz", 622);
            i7_cpu_name.Add("Core i7 3.96GHz", 623);
            i7_cpu_name.Add("Core i7 3.97GHz", 624);
            i7_cpu_name.Add("Core i7 3.98GHz", 625);
            i7_cpu_name.Add("Core i7 3.99GHz", 626);
            i7_cpu_name.Add("Core i7 4.00GHz", 627);
            i7_cpu_name.Add("Core i7 4.01GHz", 628);
            i7_cpu_name.Add("Core i7 4.02GHz", 629);
            i7_cpu_name.Add("Core i7 4.03GHz", 630);
            i7_cpu_name.Add("Core i7 4.05GHz", 631);
            i7_cpu_name.Add("Core i7 4.06GHz", 632);
            i7_cpu_name.Add("Core i7 4.07GHz", 633);
            i7_cpu_name.Add("Core i7 4.08GHz", 634);
            i7_cpu_name.Add("Core i7 4.09GHz", 635);
            i7_cpu_name.Add("Core i7 4.10GHz", 636);
            i7_cpu_name.Add("Core i7 4.11GHz", 637);
            i7_cpu_name.Add("Core i7 4.12GHz", 638);
            i7_cpu_name.Add("Core i7 4.13GHz", 639);
            i7_cpu_name.Add("Core i7 4.14GHz", 640);
            i7_cpu_name.Add("Core i7 4.15GHz", 641);
            i7_cpu_name.Add("Core i7 4.16GHz", 642);
            i7_cpu_name.Add("Core i7 4.17GHz", 643);
            i7_cpu_name.Add("Core i7 4.18GHz", 644);
            i7_cpu_name.Add("Core i7 4.19GHz", 645);
            i7_cpu_name.Add("Core i7 4.20GHz", 646);
            i7_cpu_name.Add("Core i7 4.21GHz", 647);
            i7_cpu_name.Add("Core i7 4.22GHz", 648);
            i7_cpu_name.Add("Core i7 4.23GHz", 649);
            i7_cpu_name.Add("Core i7 4.24GHz", 650);
            i7_cpu_name.Add("Core i7 4.25GHz", 651);
            i7_cpu_name.Add("Core i7 4.26GHz", 652);
            i7_cpu_name.Add("Core i7 4.27GHz", 653);
            i7_cpu_name.Add("Core i7 4.28GHz", 654);
            i7_cpu_name.Add("Core i7 4.29GHz", 655);
            i7_cpu_name.Add("Core i7 4.30GHz", 656);
            i7_cpu_name.Add("Core i7 4.31GHz", 657);
            i7_cpu_name.Add("Core i7 4.32GHz", 658);
            i7_cpu_name.Add("Core i7 4.33GHz", 659);
            i7_cpu_name.Add("Core i7 4.34GHz", 660);
            i7_cpu_name.Add("Core i7 4.35GHz", 661);
            i7_cpu_name.Add("Core i7 4.36GHz", 662);
            i7_cpu_name.Add("Core i7 4.37GHz", 663);
            i7_cpu_name.Add("Core i7 4.38GHz", 664);
            i7_cpu_name.Add("Core i7 4.39GHz", 665);
            i7_cpu_name.Add("Core i7 4.40GHz", 666);
            title = compute_difference(s, i7_cpu_name);
            return title;
        }
        public static string comput_title(string s)
        {
            string title = "";
            //       Dictionary<string, int> cpu_name =
            //new Dictionary<string, int>();
            //       cpu_name.Add("Core 2 Duo", 1);
            //       cpu_name.Add("Core i3", 2);
            //       cpu_name.Add("Core i5", 3);
            //       cpu_name.Add("Core i7", 4);
            //       string index = compute_difference(s, cpu_name);

            //switch (index)
            //{
            //    case "Core 2 Duo":
            //        {

            //            break;
            //        }
            //    case "Core i3":
            //        {
            //            title = i3_cpu(s);
            //            break;
            //        }
            //    case "Core i5":
            //        {
            //            title = i5_cpu(s);
            //            break;
            //        }
            //    case "Core i7":
            //        {
            //            title = i7_cpu(s);
            //            break;
            //        }
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Contains("2 Duo"))
                {
                    title = c2d_cpu(s);
                }
                else if (s.Contains("i3"))
                {
                    title = i3_cpu(s);
                }
                else if (s.Contains("i5"))
                {
                    title = i5_cpu(s);
                }
                else if (s.Contains("i7"))
                {
                    title = i7_cpu(s);
                }
            }
                    
            
            return title;
        }
        
        public static string hdd_format(string temp_hdd, bool IsMagento)
        {
            if (!string.IsNullOrEmpty(temp_hdd))
            {
                bool result = temp_hdd.Any(x => !char.IsLetter(x));
                if (result == true)
                {
                    temp_hdd = Regex.Replace(temp_hdd, "[^0-9]", "");
                }

                int formatted_hdd = int.Parse(temp_hdd);
                if (IsMagento == false)
                {
                    formatted_hdd = Convert.ToInt32(formatted_hdd * 1.024 * 1.024);
                }
                formatted_hdd = Levenshtein.Round(formatted_hdd, 10);
                switch (formatted_hdd)
                {
                    case 480:
                        formatted_hdd = 500;
                        temp_hdd = formatted_hdd + "GB HDD";
                        break;
                    case 980:
                        formatted_hdd = 1000;
                        temp_hdd = formatted_hdd / 1000 + "TB HDD";
                        break;
                    default:
                        temp_hdd = formatted_hdd + "GB HDD";
                        break;
                }
            }
         

            return temp_hdd;
        }

        public static string ram_format (string ram, bool IsMagento)
        {
            if (!string.IsNullOrEmpty(ram))
            {
                bool result = ram.Any(x => !char.IsLetter(x));
                if (result == true)
                {
                    ram = Regex.Replace(ram, "[^0-9]", "");
                }
                int formatted_ram = int.Parse(ram);

                if (IsMagento == false)
                {
                    formatted_ram = Levenshtein.Round(formatted_ram, 100);
                    formatted_ram = Convert.ToInt32(formatted_ram /1000);
                    
                }
                ram = formatted_ram + "GB RAM";
            }
            return ram;
        }
     

       

        public static int Compute(string s, string t)
        {
            //core Levenshtein algorithm to compute the difference
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }

    }

}