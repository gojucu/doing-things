using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace staj_day3_meh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

         //routes.MapRoute(
         //              name: "Default",
         //              url: "{controller}/{action}/{id}",
         //              defaults: new { controller = "Home", action = "Index", id = "2" }
         //          );

            routes.MapRoute(
                name: "Sidebar",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "SidebarGetir", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            
        }
    }
}
