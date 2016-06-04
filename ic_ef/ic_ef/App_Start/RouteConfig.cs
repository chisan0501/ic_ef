using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ic_ef
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*BarCodeHandler}", new { BarCodeHandler = @"(.*/)?Barcode.ashx(/.*)?" });
            routes.MapRoute(
                name: "Default",    
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Welcome", id = UrlParameter.Optional }
            );
        }
    }
}
