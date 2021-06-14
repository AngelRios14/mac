using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PCM.MacExpress.Presentation.Core.Infraestructura;
using PCM.MacExpress.Presentation.Core.ViewModel;

namespace PCM.SGR.Presentacion.Areas.AtencionCiudadano.Controllers
{
    public class AtenderCiudadanoController : WebBaseController
    {
        public static List<AtencionModels> data = new List<AtencionModels>();
        
        public ActionResult Index()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            AtencionModels model = new AtencionModels();
            ViewBag.wpv = 1;
            return View(model);
        }

        public ActionResult NuevoEntidad()
        {
            return RedirectToAction("NuevoEntidad");
        }
        public JsonResult ListarCiudadano(AtencionModels models)
        {

            if (!(string.IsNullOrEmpty(models.DNI)))
            {

                try
                {

                    BECiudadano oBECiudadano = new BECiudadano();


            List<BEAtencion> data = new List<BEAtencion>();
            string strCadenaPedicion = string.Concat("atencion?ID_CIUDADANO=", "0",
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&NOMBRE=", "",
                                                                "&&PAGINADO=", Dame_Texto(models.DNI),
                                                                "&&TIPO=", "0");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEAtencion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;

                    if (data.Count>0)
                    {
                        models.NOMBRE = data[0].NOMBRE;
                        models.APELLIDO_PATERNO = data[0].APELLIDO_PATERNO;
                        models.APELLIDO_MATERNO = data[0].APELLIDO_MATERNO;
                        models.FOTO = data[0].FOTO;
                    }
                    else
                    {

                        oBECiudadano = DniReniec(models.DNI);

                        models.NOMBRE = oBECiudadano.NOMBRE;
                        models.APELLIDO_PATERNO = oBECiudadano.APELLIDO_PATERNO;
                        models.APELLIDO_MATERNO = oBECiudadano.APELLIDO_MATERNO;
                        models.FOTO = "data:image/png;base64,"+oBECiudadano.FOTO;

                    }


                }
                catch (Exception ex) { }                
            }
            
            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}
