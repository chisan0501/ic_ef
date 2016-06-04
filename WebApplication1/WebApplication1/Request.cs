using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    class Request : IDisposable
    {
        private string useragent;
        private WebClient wc;

        public Request(string ua)
        {
            useragent = ua;
            wc = new WebClient();
            wc.Headers.Add("User-Agent", ua);
            wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            //wc.Headers.Add("Connection", "close");
            wc.Headers.Add("Cache-Control", "max-age=0");
        }

        public string requestString(string uri)
        {
            try
            {
                string resp = wc.DownloadString(@uri);
                return resp;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public void Dispose()
        {
            wc.Dispose();
        }
    }
}