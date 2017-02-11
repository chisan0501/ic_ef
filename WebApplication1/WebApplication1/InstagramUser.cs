using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   
namespace WebApplication1
{
    class InstagramUser
    {

        public bool isVerified;
        public int followers;
        public string username;
        public string description;

        public void parsePage(string ua, string user)
        {
            using (Request r = new Request(ua))
            {
                string response = r.requestString("https://instagram.com/" + user);
                parsePage(response);
            }
        }

        public void parsePage(string response)
        {
            //Verified
            string verified = "\"is_verified\":";
            if (response.Contains(verified))
            {
                int startIndex = response.IndexOf(verified);
                int endIndex = response.IndexOf(",", startIndex);
                string verifiedVal = response.Substring(startIndex + verified.Length, (endIndex - (startIndex + verified.Length)));
                isVerified = (verifiedVal == "true" ? true : false);
            }

            //Followers
            string follower = "\"followed_by\":{\"count\":";
            if (response.Contains(follower))
            {
                int startIndex = response.IndexOf(follower);
                int endIndex = response.IndexOf("}", startIndex);
                string follow = response.Substring(startIndex + follower.Length, (endIndex - (startIndex + follower.Length)));
                followers = Int32.Parse(follow);
            }

            //Username
            string name = "<meta property=\"og:url\" content=\"https://instagram.com/";
            if (response.Contains(name))
            {
                int startIndex = response.IndexOf(name);
                int endIndex = response.IndexOf("/", startIndex + name.Length);
                string n = response.Substring(startIndex + name.Length, (endIndex - (startIndex + name.Length)));
                username = n;
            }

            //Description
            string bio = "\"biography\":\"";
            if (response.Contains(bio))
            {
                int startIndex = response.IndexOf(bio);
                int endIndex = response.IndexOf("\",", startIndex + bio.Length);
                string desc = response.Substring(startIndex + bio.Length, (endIndex - (startIndex + bio.Length)));
                description = desc;
            }
        }
    }
}