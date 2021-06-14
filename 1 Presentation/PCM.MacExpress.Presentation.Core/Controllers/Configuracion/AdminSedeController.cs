using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCM.MacExpress.Presentation.Core.ViewModel;
using PCM.MacExpress.Presentation.Core.Infraestructura;
using Newtonsoft.Json;
using System.Text;

namespace PCM.MacExpress.Presentation.Core.Controllers.Configuracion
{
    public class AdminSedeController : WebBaseController
    {
        private static List<BESede> data = new List<BESede>();

        [HttpGet]
        public ActionResult Index(string wpv)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            SedeModels models = new SedeModels();

            models.intNumeroPaginaSede = 1;
            models.ID_SEDE = 0;

            ViewBag.POST = 0;
            ViewBag.wpv = Dame_Entero(wpv);
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;
            ViewBag.LST_ENTIDAD_SEDE = FnListarEntidadSede();

            return View(models);
        }

        [HttpPost]
        public ActionResult Index(int ID_SEDE, int ID_ENTIDAD, string NOMBRE,string DEPARTAMENTO,string PROVINCIA,string DISTRITO)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            SedeModels models = new SedeModels();
            models.ID_SEDE = 0;
            models.intNumeroPaginaSede = 1;

            byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BESede()
            {
                ID_SEDE = ID_SEDE,
                ID_ENTIDAD = ID_ENTIDAD,
                NOMBRE = NOMBRE,
                USUARIO_REGISTRA = oBEUsuario.USUARIO,
                COD_UBIGEO = DEPARTAMENTO + PROVINCIA + DISTRITO
            })); ;

            var strRespuesta = PostJSON("sede", postBytes);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<BEEntidad>>(strRespuesta);

            ViewBag.POST = 0;
            ViewBag.wpv = 1;
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;
            ViewBag.LST_ENTIDAD_SEDE = FnListarEntidadSede();

            return View();
        }

        [HttpGet]
        public ActionResult nuevaSede(int ID_SEDE)
        {
            ViewBag.LST_ENTIDAD_SEDE = FnListarEntidadSede();
            ViewBag.LST_DEPARTAMENTO_ENTIDAD = FnListarDepartamento();


            if (ID_SEDE == 0)
            {
                ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BESede());
                ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BESede());

                return PartialView("Nuevo", new SedeModels() { ID_SEDE = 0, ID_ENTIDAD = 0, NOMBRE = string.Empty, DEPARTAMENTO = string.Empty, PROVINCIA = string.Empty, DISTRITO = string.Empty });

            }
            else
            {
                string strCadenaPedicion = string.Concat("sede?ID_SEDE=", Dame_Entero(ID_SEDE));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BESede>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                    string COD_UBIGEO_DEPARTAMENTO = string.Empty, COD_UBIGEO_PROVINCIA = string.Empty, COD_UBIGEO_DISTRITO = string.Empty;
                    if (!(string.IsNullOrEmpty(data_.COD_UBIGEO)))
                    {
                        COD_UBIGEO_DEPARTAMENTO = data_.COD_UBIGEO.Substring(0, 2);
                        COD_UBIGEO_PROVINCIA = data_.COD_UBIGEO.Substring(2, 2);
                        COD_UBIGEO_DISTRITO = data_.COD_UBIGEO.Substring(4);

                        ViewBag.LST_PROVINCIA_ENTIDAD = new SelectList(fnListarUbigeo(new BESede() { CONDICION = 2, COD_UBIGEO_DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO, COD_UBIGEO_PROVINCIA = COD_UBIGEO_PROVINCIA }), "COD_UBIGEO", "PROVINCIA");
                        ViewBag.LST_DISTRITO_ENTIDAD = new SelectList(fnListarUbigeo(new BESede() { CONDICION = 3, COD_UBIGEO_DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO, COD_UBIGEO_PROVINCIA = COD_UBIGEO_PROVINCIA }), "COD_UBIGEO", "DISTRITO");

                    }
                    else
                    {
                        ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BESede());
                        ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BESede());

                    }
                        return PartialView("Nuevo", new SedeModels() { ID_SEDE = data_.ID_SEDE, ID_ENTIDAD = data_.ID_ENTIDAD, NOMBRE = data_.NOMBRE, DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO, PROVINCIA = COD_UBIGEO_PROVINCIA, DISTRITO = COD_UBIGEO_DISTRITO });
                }
                else
                {
                    return PartialView("Nuevo", new SedeModels() { ID_SEDE = 0, ID_ENTIDAD = 0, NOMBRE = string.Empty, DEPARTAMENTO = string.Empty, PROVINCIA = string.Empty, DISTRITO = string.Empty });
                }
            }
        }

        public JsonResult ListarSede(SedeModels models)
        {
            string strCadenaPedicion = string.Concat("sede?ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                     "&&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                     "&&NOMBRE=", Dame_Texto(models.NOMBRE),
                                                     "&&PAGINADO=", Dame_Entero(models.intNumeroPaginaSede),
                                                     "&&TIPO=1");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BESede>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return Json(ObjetoJSON.Data.Result, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.AllowGet);
        }

        public SelectList FnListarDepartamento()
        {
            List<BEEntidad> data = new List<BEEntidad>();
            string strCadenaPedicion = "entidad?CONDICION=1&&COD_UBIGEO_DEPARTAMENTO=01&&COD_UBIGEO_PROVINCIA=01";
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return new SelectList(ObjetoJSON.Data.Result, "COD_UBIGEO", "DEPARTAMENTO");
            else
            {
                data.Add(new BEEntidad { COD_UBIGEO = "0", DEPARTAMENTO = "Seleccione" });
                return new SelectList(data, "COD_UBIGEO", "DEPARTAMENTO");
            }
        }

        public SelectList FnListarProvincia(BESede oBEEntidad)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "SELECCIONE", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarDistrito(BESede oBEEntidad)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "SELECCIONE", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarEntidadSede()
        {
            string COD_UBIGEO = string.Empty;

            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", 0,
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&RUC=", string.Empty,
                                                     "&&COD_UBIGEO=", string.Empty,
                                                     "&&ORDEN=", 0,
                                                     "&&ES_MUNICIPALIDAD=", "true",
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return new SelectList(ObjetoJSON.Data.Result, "ID_ENTIDAD", "NOMBRE");
            else
            {
                List<BEEntidad> data_ = new List<BEEntidad>();
                data_.Add(new BEEntidad { ID_ENTIDAD = 0, NOMBRE = "SELECCIONE" });
                return new SelectList(data_, "ID_ENTIDAD", "NOMBRE");
            }
        }
        public List<BESede> fnListarUbigeo(BESede oBEEntidad)
        {

            List<BESede> data = new List<BESede>();
            string strCadenaPedicion = string.Concat("entidad?CONDICION=", oBEEntidad.CONDICION.ToString(),
                                                    "&&COD_UBIGEO_DEPARTAMENTO=", oBEEntidad.COD_UBIGEO_DEPARTAMENTO,
                                                    "&&COD_UBIGEO_PROVINCIA=", oBEEntidad.COD_UBIGEO_PROVINCIA);

            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BESede>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;
            else
            {
                data.Add(new BESede { COD_UBIGEO = "0", DEPARTAMENTO = "Seleccione" });
            }
            return data;
        }
    }

}
