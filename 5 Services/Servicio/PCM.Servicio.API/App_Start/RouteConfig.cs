using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PCM.Servicio.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.Where(r => r is Route).ToList().ForEach(r =>
            //{
            //    Route router = (Route)r;
            //    if (router.DataTokens != null && router.DataTokens.ContainsKey("area"))
            //    {
            //        router.DataTokens["Namespaces"] = "PCM.Servicio.API.Controllers." + router.DataTokens["area"];
            //    }
            //});
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
