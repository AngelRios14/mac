using Newtonsoft.Json;
using PCM.MacExpress.Presentation.Core.Infraestructura;
using PCM.MacExpress.Presentation.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM.MacExpress.Presentation.Core.Controllers.Reportes
{
    public class ReportesAtencionController : WebBaseController
    {
        // GET: Reportes/ReportesAtencion
        public ActionResult Index()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            modelReporte models = new modelReporte();

            ViewBag.LST_ENTIDAD_REPORTE = FnListarEntidadSede("true", oBEUsuario.ID_ENTIDAD);
            ViewBag.LST_SEDE_REPORTE = FnListarSede(new BESede());
            ViewBag.LST_USUARIO_REPORTE = FnListarUsuario(new BEUsuario());
            ViewBag.LST_SERVICIO_REPORTE = FnListarServicio();

            models.FECHA_INICIO = DateTime.Now;
            models.FECHA_FIN = DateTime.Now;

            return View(models);
        }

        [HttpPost]
        public ActionResult Index(modelReporte models)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            List<BEAtencion> data = new List<BEAtencion>();
            string strCadenaPedicion = string.Concat("atencion?ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                                "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                                "&&ID_USUARIO=", Dame_Entero(models.ID_USUARIO),
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                                "&&FECHA_INICIO=", models.FECHA_INICIO.ToString("dd/MM/yyyy"),
                                                                "&&FECHA_FIN=", models.FECHA_FIN.ToString("dd/MM/yyyy"),
                                                                "&&PAGINADO=1",
                                                                "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEAtencion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;

            if (data.Count > 0) {
                string reporte = GenerateStringHTML("REPORTE DE ATENCIONES POR SEDE", data, 1); //ObtenerReporte(data);

                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteAtencionSede_" + (new Random().Next(10000)).ToString() + ".xls");
                Response.Charset = String.Empty;
                Response.Write(reporte);
                Response.End();
            }

            ViewBag.LST_ENTIDAD_REPORTE = FnListarEntidadSede("true", oBEUsuario.ID_ENTIDAD);
            ViewBag.LST_SEDE_REPORTE = FnListarSede(new BESede());
            ViewBag.LST_USUARIO_REPORTE = FnListarUsuario(new BEUsuario());
            ViewBag.LST_SERVICIO_REPORTE = FnListarServicio();

            return View(models);
        }

        public ActionResult Entidad()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            modelReporte models = new modelReporte();

            ViewBag.LST_DEPARTAMENTO_ENTIDAD = FnListarDepartamento();
            ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BEEntidad());
            ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BEEntidad());

            ViewBag.LST_ENTIDAD_REPORTE = FnListarEntidadSede();
            ViewBag.LST_SEDE_REPORTE = FnListarSede(new BESede());
            ViewBag.LST_USUARIO_REPORTE = FnListarUsuario(new BEUsuario());
            ViewBag.LST_SERVICIO_REPORTE = FnListarServicio();

            models.FECHA_INICIO = DateTime.Now;
            models.FECHA_FIN = DateTime.Now;

            return View(models);
        }

        [HttpPost]
        public ActionResult Entidad(modelReporte models) 
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            List<BEAtencion> data = new List<BEAtencion>();
            string strCadenaPedicion = string.Concat("atencion?DEPARTAMENTO=", Dame_Texto(models.DEPARTAMENTO),
                                                                "&PROVINCIA=", Dame_Texto(models.PROVINCIA),
                                                                "&DISTRITO=", Dame_Texto(models.DISTRITO),
                                                                "&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                                "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                                "&&ID_USUARIO=", Dame_Entero(models.ID_USUARIO),
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                                "&&FECHA_INICIO=", models.FECHA_INICIO.ToString("dd/MM/yyyy"),
                                                                "&&FECHA_FIN=", models.FECHA_FIN.ToString("dd/MM/yyyy"),
                                                                "&&PAGINADO=1",
                                                                "&&TIPO=1");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEAtencion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;

            if (data.Count > 0) {
                string reporte = GenerateStringHTML("REPORTE DE ATENCIONES POR DEPARTAMENTO", data, 2); //ObtenerReporte(data);

                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=ReporteAtencionSede_" + (new Random().Next(10000)).ToString() + ".xls");
                Response.Charset = String.Empty;
                Response.Write(reporte);
                Response.End();
            }

            ViewBag.LST_DEPARTAMENTO_ENTIDAD = FnListarDepartamento();
            ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BEEntidad());
            ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BEEntidad());

            ViewBag.LST_ENTIDAD_REPORTE = FnListarEntidadSede();
            ViewBag.LST_SEDE_REPORTE = FnListarSede(new BESede());
            ViewBag.LST_USUARIO_REPORTE = FnListarUsuario(new BEUsuario());
            ViewBag.LST_SERVICIO_REPORTE = FnListarServicio();

            return View(models);
        }

        public SelectList FnListarServicio()
        {
            var LstAtencion = new List<BEAtencion>();

            string strCadenaPedicion = string.Concat("servicio?ID_SERVICIO=0",
                                                     "&&ID_TIPOSERVICIO=0",
                                                     "&&ID_ENTIDAD=0",
                                                     "&&ID_MODOSERVICIO=0",
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&ID_TIPOACCESO=0",
                                                     "&&LINK=", string.Empty,
                                                     "&&ORDEN=0", 
                                                     "&&ESTADO_VIGENCIA=0",
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicio>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return new SelectList(ObjetoJSON.Data.Result, "ID_SERVICIO", "NOMBRE");
            else
            {
                List<BEServicio> data_ = new List<BEServicio>();
                data_.Add(new BEServicio { ID_ENTIDAD = 0, NOMBRE = "TODOS" });
                return new SelectList(data_, "ID_ENTIDAD", "NOMBRE");
            }
        }
        public SelectList FnListarUsuario(BEUsuario BEUsuario)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "TODOS", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarSede(BESede oBESede)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "TODOS", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarEntidadSede() {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "TODOS", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarEntidadSede(string esMunicipalidad, int? ID_ENTIDAD)
        {
            string COD_UBIGEO = string.Empty;

            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", 0,
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&RUC=", string.Empty,
                                                     "&&COD_UBIGEO=", string.Empty,
                                                     "&&ORDEN=", 0,
                                                     "&&ES_MUNICIPALIDAD=", esMunicipalidad,
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess) {
                List<BEEntidad> LstEntidad = ObjetoJSON.Data.Result;

                if(ID_ENTIDAD != null)
                    if (ID_ENTIDAD > 0)
                        LstEntidad = LstEntidad.Where(x => x.ID_ENTIDAD == ID_ENTIDAD).ToList();

                return new SelectList(LstEntidad, "ID_ENTIDAD", "NOMBRE");
            }
            else
            {
                List<BEEntidad> data_ = new List<BEEntidad>();
                data_.Add(new BEEntidad { ID_ENTIDAD = 0, NOMBRE = "TODOS" });
                return new SelectList(data_, "ID_ENTIDAD", "NOMBRE");
            }
        }

        public JsonResult ListarEntidad(BEEntidad models)
        {
            List<BESede> data = new List<BESede>();
            string strCadenaPedicion = string.Concat("sede?ID_SEDE=0",
                                                     "&&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");

            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BESede>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;
            else
            {
                data.Add(new BESede { ID_SEDE = 0, NOMBRE = "TODOS" });
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarUsuario(BEUsuario models)
        {
            List<BEUsuario> data = new List<BEUsuario>();
            string strCadenaPedicion = string.Concat("usuario?ID_USUARIO=0",
                                                                "&&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                                "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&NOMBRE=", Dame_Texto(models.NOMBRE),
                                                                "&&APELLIDO_PATERNO=", Dame_Texto(models.APELLIDO_PATERNO),
                                                                "&&APELLIDO_MATERNO=", Dame_Texto(models.APELLIDO_MATERNO),
                                                                "&&USUARIO=", Dame_Texto(models.USUARIO),
                                                                "&&ESTADO_VIGENCIA=", Dame_Entero(models.ESTADO_VIGENCIA),
                                                                "&&TIPO_USUARIO=", (int)TIPO_USUARIO.ASESOR,
                                                                "&&PAGINADO=1",
                                                                "&&TIPO=5");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEUsuario>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;
            else
            {
                data.Add(new BEUsuario { ID_USUARIO = 0, NOMBRE = "TODOS", APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty });
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarReporte(BEAtencion models)
        {

            List<BEAtencion> data = new List<BEAtencion>();
            string strCadenaPedicion = string.Concat("atencion?ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                                "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                                "&&ID_USUARIO=", Dame_Entero(models.ID_USUARIO),
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                                "&&FECHA_INICIO=", models.FECHA_INICIO.ToString("dd/MM/yyyy"),
                                                                "&&FECHA_FIN=", models.FECHA_FIN.ToString("dd/MM/yyyy"),
                                                                "&&PAGINADO=", Dame_Entero(models.PAGINADO),
                                                                "&&TIPO=", Dame_Entero(models.TIPO));
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEAtencion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarReporteDepartamento(BEAtencion models)
        {
            List<BEAtencion> data = new List<BEAtencion>();
            string strCadenaPedicion = string.Concat("atencion?DEPARTAMENTO=", Dame_Texto(models.DEPARTAMENTO),
                                                                "&PROVINCIA=", Dame_Texto(models.PROVINCIA),
                                                                "&DISTRITO=", Dame_Texto(models.DISTRITO),
                                                                "&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                                "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                                "&&ID_USUARIO=", Dame_Entero(models.ID_USUARIO),
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                                "&&FECHA_INICIO=", models.FECHA_INICIO.ToString("dd/MM/yyyy"),
                                                                "&&FECHA_FIN=", models.FECHA_FIN.ToString("dd/MM/yyyy"),
                                                                "&&PAGINADO=", Dame_Entero(models.PAGINADO),
                                                                "&&TIPO=", Dame_Entero(models.TIPO));
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEAtencion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;

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
                data.Add(new BEEntidad { COD_UBIGEO = "0", DEPARTAMENTO = "TODOS" });
                return new SelectList(data, "COD_UBIGEO", "DEPARTAMENTO");
            }
        }

        public SelectList FnListarProvincia(BEEntidad oBEEntidad)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "TODOS", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarDistrito(BEEntidad oBEEntidad)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "TODOS", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public JsonResult ListarEntidadSede(BEEntidad models)
        {
            List<BEEntidad> data = new List<BEEntidad>();
            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", 0,
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&RUC=", string.Empty,
                                                     "&&COD_UBIGEO=", models.COD_UBIGEO,
                                                     "&&ORDEN=", 0,
                                                     "&&ES_MUNICIPALIDAD=", true,
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=2");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;
            else
                data.Add(new BEEntidad(){ ID_ENTIDAD = 0, NOMBRE = "TODOS" });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Monitoreo()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            string perfil = oBEUsuario.Lista_Permiso[0].ROL;
            modelReporte models = new modelReporte();

            ViewBag.LST_ENTIDAD_REPORTE = FnListarEntidadSede("true", oBEUsuario.ID_ENTIDAD);
            ViewBag.LST_SEDE_REPORTE = FnListarSede(new BESede());
            ViewBag.LST_USUARIO_REPORTE = FnListarUsuario(new BEUsuario());
            ViewBag.LST_SERVICIO_REPORTE = FnListarServicio();

            models.FECHA_INICIO = DateTime.Now;
            models.FECHA_FIN = DateTime.Now;

            return View(models);
        }


        public JsonResult ListarMonAtenciones(BEAtencion models)
        {

            List<BEAtencion> data = new List<BEAtencion>();
            string strCadenaPedicion = string.Concat("atencion?ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                                "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                                "&&ID_USUARIO=", Dame_Entero(models.ID_USUARIO),
                                                                "&&DNI=", Dame_Texto(models.DNI),
                                                                "&&ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                                "&&FECHA_INICIO=", models.FECHA_INICIO.ToString("dd/MM/yyyy"),
                                                                "&&FECHA_FIN=", models.FECHA_FIN.ToString("dd/MM/yyyy"),
                                                                "&&PAGINADO=", Dame_Entero(models.PAGINADO),
                                                                "&&TIPO=", Dame_Entero(models.TIPO));
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEAtencion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
