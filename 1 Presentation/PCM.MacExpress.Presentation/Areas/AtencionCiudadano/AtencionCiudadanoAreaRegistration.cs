using System.Web.Mvc;

namespace PCM.Reclamo.Presentation.Areas.AtencionCiudadano
{
    public class AtencionCiudadanoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AtencionCiudadano";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AtencionCiudadano_default",
                "AtencionCiudadano/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}