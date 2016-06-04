using HtmlAgilityPack;
using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{

    public partial class _Default : Page
    {
        ArrayList bio = new ArrayList();
        ArrayList websites = new ArrayList();
        ArrayList follower = new ArrayList();
        ArrayList ids = new ArrayList();
        ArrayList name = new ArrayList();
        ArrayList username = new ArrayList();
        ArrayList profile_picture = new ArrayList();
        ArrayList search_list = new ArrayList();
        string nextpageurl = "";
        string userid = "";
        string username_only = "";

      
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                create_list();
                fetchweb web = new fetchweb();
                foreach (string id in search_list)
                {
                    web.fetch_url("https://www.instagram.com/"+ id, id);
                }
                
                //    updateListid();
               // web.average("https://www.instagram.com/fashionista804");


                string dbHost = "mysql.us.cloudlogin.co";
                string dbUser = "one007hk_ig";
                string dbPass = "chisan0501";
                string dbName = "one007hk_ig";

                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "select * from search_list where status != 'USED'";
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    string temp_name = reader["user_id"].ToString();
                    string temp_value = reader["follower"].ToString();
                    progress_list.Items.Add(new ListItem(temp_name, temp_value));
                }
                conn.Close();
                conn.Open();
                command.CommandText = "update one007hk_ig.ig i, one007hk_ig.search_list s set i.exisit = 'YES' where i.username = s.user_id";
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
        public void create_list ()
        {
            string dbHost = "mysql.us.cloudlogin.co";
            string dbUser = "one007hk_ig";
            string dbPass = "chisan0501";
            string dbName = "one007hk_ig";

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "select user_id from search_list where search is NULL";
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                search_list.Add(reader["user_id"].ToString());
                
            }
            conn.Close();
        }
        public void get_media_total (string name)
        {
            try
            {
                string url = "https://api.instagram.com/v1/users/" + name + "?access_token=14004119.1fb234f.06c4339b9c774158a8d7d55209b04e3c";
                var w = new WebClient();
                var json_data = w.DownloadString(url);
                if (!string.IsNullOrEmpty(json_data))
                {
                    dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(json_data);
                    var some = jsonData.data;
                    var count = some.counts;
                    var media_count = some.counts.media.ToString();
                    int real_count = int.Parse(media_count);
                    string dbHost = "mysql.us.cloudlogin.co";
                    string dbUser = "one007hk_ig";
                    string dbPass = "chisan0501";
                    string dbName = "one007hk_ig";

                    string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
                    MySqlConnection conn = new MySqlConnection(connStr);
                    MySqlCommand command = conn.CreateCommand();
                    conn.Open();
                    command.CommandText = "update search_list set media_total = '" + real_count + "' where'" + name + "' = id";
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
           catch(Exception e)
            {

            }
            
             
        }

        public void updateListid ()
        {
            Dictionary<string, string> usr_id_list =
        new Dictionary<string, string>();
            //ArrayList usr_id_list = new ArrayList();
            string dbHost = "mysql.us.cloudlogin.co";
            string dbUser = "one007hk_ig";
            string dbPass = "chisan0501";
            string dbName = "one007hk_ig";

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
          // command.CommandText = "select user_id from search_list where id is NULL";
            command.CommandText = "select distinct id,media_total from search_list where lpp is NULL";
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
              //  usr_id_list.Add(reader["user_id"].ToString());
                usr_id_list.Add(reader["id"].ToString(), reader["media_total"].ToString());
            }
            conn.Close();
            //foreach(string id in usr_id_list)
            //{
            //    //   get_userid(id);
            //    //  get_media_total(id);
            //}

            foreach (KeyValuePair<string,string> pair in usr_id_list)
            {
               
                average(pair.Key, pair.Value);
                page_id = "";
                count = 0;
                realcount_comment = 0;
                realcount_like = 0;
            }
        }
        public void get_userid(string name)
        {
            
               // string url = "https://api.instagram.com/v1/users/search?access_token=14004119.1fb234f.06c4339b9c774158a8d7d55209b04e3c&q=" + name;
            string url = "https://api.instagram.com/v1/users/search?access_token="+access_token_text.Text+"&q=" + name;
            var w = new WebClient();
            var json_data = w.DownloadString(url);
            if (!string.IsNullOrEmpty(json_data))
            {
                dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(json_data);
                int count = jsonData.data.Count;
                for (int i = 0; i < count; i++)
                {
                    name = name.ToLower();
                    string temp_username = jsonData.data[i].username.ToString();
                    
                    if (temp_username == name)
                    {
                        userid = jsonData.data[i].id.ToString();
                        break;
                    }
                    userid = "0";
                }
                
                string dbHost = "mysql.us.cloudlogin.co";
                string dbUser = "one007hk_ig";
                string dbPass = "chisan0501";
                string dbName = "one007hk_ig";

                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "update search_list set id = '" + userid + "' where user_id = '" + name + "'";
                command.ExecuteNonQuery();
                conn.Close();
            }

        }
        public void getfollowerlist(string url)
        {

            var w = new WebClient();
            var json_data = w.DownloadString(url);
            if (!string.IsNullOrEmpty(json_data))
            {
                // var root = JsonConvert.DeserializeObject<List< insta.RootObject >>(json_data);
                dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(json_data);
                int total = jsonData.data.Count;
                try
                {
                    var nextpage = jsonData.pagination;

                    nextpageurl = nextpage.next_url.ToString();
                }
                catch (Exception e)
                {
                    string error = e.ToString();
                    return;
                }
                page_id_insert(nextpageurl);
                for (int i = 0; i < total; i++)
                {
                    name.Add(jsonData.data[i].username.ToString());
                    ids.Add(jsonData.data[i].id.ToString());

                    profile_picture.Add(jsonData.data[i].profile_picture.ToString());
                }
            }
            getfollower();
        }
        public void getfollower()
        {

            foreach (string id in ids)
            {
                System.Threading.Thread.Sleep(3000);
                //query each user's follower number
                string url = "https://api.instagram.com/v1/users/" + id + "?access_token=" + access_token_text.Text;
                // url = "https://api.instagram.com/v1/users/2914084521?access_token=14004119.1fb234f.06c4339b9c774158a8d7d55209b04e3c";
                var json_data = "";
                try
                {
                    var w = new WebClient();
                    json_data = w.DownloadString(url);
                }
                catch (WebException ex)
                {
                    string exception = ex.ToString();
                    continue;
                }

                if (!string.IsNullOrEmpty(json_data))
                {

                    dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(json_data);
                    var some = jsonData.data;
                    var bios = some.bio.ToString();
                    var result_username = some.username.ToString();
                    var count = some.counts;
                    var follow = count.followed_by.ToString();
                    double condition = double.Parse(follow);
                    double follower_double = Double.Parse(follower_text.Text);
                    if (condition > follower_double)
                    {
                        username_only = result_username;
                        result_username = "https://www.instagram.com/" + result_username;
                        string temp_bio = bios;
                        temp_bio = temp_bio.Replace("'", "\\'");
                        database_insert(result_username, follow, temp_bio, userid, username_only);
                     
                    }

                    // var firstName = obj.Properties().Select(p => p.Root);
                    //   websites.Add(jsonData.data[0].website.ToString());
                    // var reuslt = jsonData.data[1].bio.ToString();
                    //  follower.Add(jsonData.data[1].counts.followed_by.ToString());

                }
            }
            getfollowerlist(nextpageurl);
        }
        public void page_id_insert(string page_id)
        {
            string dbHost = "mysql.us.cloudlogin.co";
            string dbUser = "one007hk_ig";
            string dbPass = "chisan0501";
            string dbName = "one007hk_ig";

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "Insert into page_id(page_id) values('" + page_id + "')";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public void database_insert(string user_id, string follower, string bio, string master_id, string username_only)
        {
            // database info
            string dbHost = "mysql.us.cloudlogin.co";
            string dbUser = "one007hk_ig";
            string dbPass = "chisan0501";
            string dbName = "one007hk_ig";

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "Insert into ig(user_id,follower,bio,master_id,username) values('" + user_id + "','" + follower + "','" + bio + "','" + master_id + "','" + username_only + "') ON DUPLICATE KEY UPDATE follower= '" + follower + "'";
            command.ExecuteNonQuery();
            conn.Close();
           
        }

        int count = 0;
        double realcount_comment = 0;
        double realcount_like = 0;
      
        string page_id = "";
        public void average (string userid,string current_count)
        {
            string url = "";
           if (page_id == "")
            {
                url = "https://api.instagram.com/v1/users/" + userid + "/media/recent?access_token=14004119.50b0fed.b377e2365b6e4c30a7d4edafded6e758";
            }
            else
            {
                url = page_id;
            }
          
                var w = new WebClient();
                var json_data = w.DownloadString(url);
                if (!string.IsNullOrEmpty(json_data))
                {
                    dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(json_data);
                    var image_page = jsonData.pagination;
                    var image_data = jsonData.data;
                    int number = image_data.Count;

                    for (int i = 0; i < number; i++)
                    {
                        var temp_comments = image_data[i].comments;
                        string temp_count = temp_comments.count.ToString();
                        int count = int.Parse(temp_count);
                        realcount_comment += count;
                        var temp_like = image_data[i].likes;
                        temp_count = temp_like.count.ToString();
                        count = int.Parse(temp_count);
                        realcount_like += count;
                    }
                    count += number;
                    page_id = image_page.next_url;

            
            }
                if (page_id != null)
            {
                average(userid,current_count);
            }

            // average_next(page_id, current_count, userid);
            var average_cpp = realcount_comment / count;
            var average_lpp = realcount_like / count;
            string dbHost = "mysql.us.cloudlogin.co";
            string dbUser = "one007hk_ig";
            string dbPass = "chisan0501";
            string dbName = "one007hk_ig";

            string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand command = conn.CreateCommand();
            conn.Open();
            command.CommandText = "update search_list set cutoff = '" + current_count + "' , cpp = '" + average_cpp + "' , lpp = '" + average_lpp + "', count = '" + count + "' where id = '" + userid + "'";
            command.ExecuteNonQuery();
            conn.Close();
        }
        protected void submit(object sender, EventArgs e)
        {
            if (is_save.Checked == true)
            {
                string dbHost = "mysql.us.cloudlogin.co";
                string dbUser = "one007hk_ig";
                string dbPass = "chisan0501";
                string dbName = "one007hk_ig";

                string connStr = "server=" + dbHost + ";uid=" + dbUser + ";pwd=" + dbPass + ";database=" + dbName + ";port=3306";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand command = conn.CreateCommand();
                conn.Open();
                command.CommandText = "Update search_list set status ='USED' where user_id = '" + user_txt.Text + "'";
                command.ExecuteNonQuery();
                conn.Close();
            }
            userid = usernmae_txt.Text;
            string first_url = "https://api.instagram.com/v1/users/" + userid + "/follows?access_token=" + access_token_text.Text + "";
            getfollowerlist(first_url);
        }

        protected void nametoid_Click(object sender, EventArgs e)
        {
            get_userid(user_name_text.Text);
            user_name_text.Text = userid;
            usernmae_txt.Text = userid;
        }
    }
}