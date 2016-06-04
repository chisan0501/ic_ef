using Microsoft.Owin;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections;
using System.Net;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup {


       
        //get userid by username
    
        //starts here
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);

            
          
        }
       
    }
}
