using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApplication1
{
    public class fetchweb
    {
        public void login ()
        {
            IWebDriver driver = new ChromeDriver("D:\\");

            //Navigate to google page
            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
            IWebElement element = driver.FindElement(By.CssSelector("#react-root > div > article > div > div:nth-child(1) > div > form > div:nth-child(1) > input"));
            element.SendKeys("shorewood12");
            element = driver.FindElement(By.Name("password"));
            element.SendKeys("chisan0501");
            element = driver.FindElement(By.CssSelector("#react-root > div > article > div > div:nth-child(1) > div > form > span > button"));
            element.Click();
        }
        public void average (string url)
        {
            //Create the reference for our browser
            IWebDriver driver = new FirefoxDriver();

            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
            Thread.Sleep(1000);
            IWebElement element = driver.FindElement(By.CssSelector("#react-root > div > article > div > div:nth-child(1) > div > form > div:nth-child(1) > input"));
            element.SendKeys("shorewood12");
            element = driver.FindElement(By.Name("password"));
            element.SendKeys("chisan0501");
            element = driver.FindElement(By.CssSelector("#react-root > div > article > div > div:nth-child(1) > div > form > span > button"));
            element.Click();
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
            element = driver.FindElement(By.CssSelector("#react-root > section > main > article > div > div._pupj3 > a"));
            element.Click();
            
            IWebElement test_all = driver.FindElement(By.CssSelector("#react-root > section > main > article > div > div._8fxp6"));
            Thread.Sleep(2000);
            IList<IWebElement> test_list = test_all.FindElements(By.TagName("span"));
            foreach (IWebElement Element in test_list)
            {
                if (!String.IsNullOrEmpty(Element.Text))
                {
                  
                }
            }


        }
        public void fetch_url(string url, string master_id)
        {
            string dbHost = "mysql.us.cloudlogin.co";
            string dbUser = "one007hk_ig";
            string dbPass = "chisan0501";
            string dbName = "one007hk_ig";

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
          
            conn.Open();
            command.CommandText = "update search_list set search = 'YES' where user_id = '"+master_id+"'";
            command.ExecuteNonQuery();
            conn.Close();
            //Create the reference for our browser
            IWebDriver driver = new FirefoxDriver();
            
            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/");
            Thread.Sleep(1000);
            IWebElement element = driver.FindElement(By.CssSelector("#react-root > div > article > div > div:nth-child(1) > div > form > div:nth-child(1) > input"));
            element.SendKeys("shorewood12");
            element = driver.FindElement(By.Name("password"));
            element.SendKeys("chisan0501");
            element = driver.FindElement(By.CssSelector("#react-root > div > article > div > div:nth-child(1) > div > form > span > button"));
            element.Click();
            Thread.Sleep(1000);
            //Navigate to page
            driver.Navigate().GoToUrl(url);

            Thread.Sleep(2000);
            //Find the 3 stat data
            
           string media_total = driver.FindElement(By.ClassName("_e8fkl")).Text;
            if (String.IsNullOrEmpty(media_total))
            {
                media_total = "0";
            }
            string followers = driver.FindElement(By.ClassName("_pr3wx")).GetAttribute("title");
            if (String.IsNullOrEmpty(followers))
            {
                followers = "0";
            }
            string followings = driver.FindElement(By.ClassName("_bgvpv")).Text;
            if (String.IsNullOrEmpty(followings))
            {
                followings = "0";
            }
            IList<IWebElement> cartItems = driver.FindElements(By.ClassName("_ocznt"));
            //Perform 
            Thread.Sleep(2000);
            element = driver.FindElement(By.ClassName("_c26bu"));
            element.Click();
            Thread.Sleep(2000);
            // IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            // js.ExecuteScript("document.getElementById('text-8').scrollIntoView(true);");
           
            followings = followings.Replace(",", "");
            int true_following = int.Parse(followings);


            //start fetching list
            element = driver.FindElement(By.ClassName("_4gt3b"));
            

          
            // driver.FindElement(By.ClassName("_oidfu")).Click();

            // element.SendKeys("executeautomation");
            for (int second = 0; ; second++)
            {
                IWebElement test_all = driver.FindElement(By.CssSelector("body > div:nth-child(9) > div > div._g1ax7 > div > div._4gt3b > ul"));
                IList<IWebElement> test_list = test_all.FindElements(By.TagName("li"));
                String[] test_allText = new String[test_list.Count];
                int count = test_allText.Count();


                if (true_following - count <= 5)
                {
                    break;
                }
                element.SendKeys(Keys.PageDown);
               
                
            }
            Thread.Sleep(2000);
            ArrayList temp = new ArrayList();
            IWebElement all = driver.FindElement(By.CssSelector("body > div:nth-child(9) > div > div._g1ax7 > div > div._4gt3b > ul"));
            IList<IWebElement> list = all.FindElements(By.TagName("a"));
            foreach (IWebElement Element in list)
            {
                if (!String.IsNullOrEmpty(Element.Text))
                {
                    temp.Add(Element.Text);
                }
            }
           
                    ArrayList valid_usr = new ArrayList();
            foreach (string result in temp)
            {
               

                    driver.Navigate().GoToUrl("https://www.instagram.com/" + result);
                    Thread.Sleep(1000);
              
                    string filter_followers = driver.FindElement(By.ClassName("_pr3wx")).GetAttribute("title");
                if (String.IsNullOrEmpty(filter_followers))
                {
                    filter_followers = "0";
                }
                string filter_media_total = driver.FindElement(By.ClassName("_e8fkl")).Text;
                if (String.IsNullOrEmpty(filter_media_total))
                {
                    filter_media_total = "0";
                }
                string bio = driver.FindElement(By.CssSelector("#react-root > section > main > article > header > div._de9bg > div._bugdy")).Text;
                bio = bio.Replace("'", "");
                string filter_followings = driver.FindElement(By.ClassName("_bgvpv")).Text;
                if (String.IsNullOrEmpty(filter_followings))
                {
                    filter_followings = "0";
                }
                filter_followers = filter_followers.Replace(",", "");
                    int true_filter_followers = int.Parse(filter_followers);
                    if (true_filter_followers > 120000)
                    {

                        dbHost = "mysql.us.cloudlogin.co";
                         dbUser = "one007hk_ig";
                        dbPass = "chisan0501";
                        dbName = "one007hk_ig";

                        connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
                        conn = new MySqlConnection(connStr);
                        command = conn.CreateCommand();
                        conn.Open();
                        command.CommandText = "Insert into ig(user_id,master_id,follower,bio,username,media_total,following) values('" + "https://www.instagram.com/" + result + "','"+ master_id+"','" + filter_followers + "','" + bio + "','" + result + "','" + filter_media_total + "','" + filter_followings + "') ON DUPLICATE KEY UPDATE follower= '" + filter_followers + "'";
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }

            
        

            //Close the browser
            driver.Close();
        }
    }
}