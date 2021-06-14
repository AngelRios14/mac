using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

namespace PCM.Servicio.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            //config.Routes.Where(r => r is Route).ToList().ForEach(r =>
            //{
            //    Route router = (Route)r;
            //    if (router.DataTokens != null && router.DataTokens.ContainsKey("area"))
            //    {
            //        router.DataTokens["Namespaces"] = "PCM.Servicio.API.Controllers." + router.DataTokens["area"];
            //    }
            //});
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
