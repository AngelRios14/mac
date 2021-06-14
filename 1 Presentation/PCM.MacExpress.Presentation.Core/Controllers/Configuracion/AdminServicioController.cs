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
    public class AdminServicioController : WebBaseController
    {
        private static List<BEServicio> data = new List<BEServicio>();
        private static List<BEOperacion> dataOperacion = new List<BEOperacion>();
        private static List<BEParametro> dataParametro = new List<BEParametro>();
        

        [HttpGet]
        public ActionResult Index(string wpv)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            ServicioModels models = new ServicioModels();

            models.intNumeroPaginaServicio = 1;
            models.ID_SERVICIO = 0;

            ViewBag.POST = 0;
            ViewBag.wpv = Dame_Entero(wpv);
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;
            ViewBag.LST_ENTIDAD_SERVICIO = FnListarEntidadServicio();
            ViewBag.LST_TIPOSERVICIO_SERVICIO = FnListarTiposervicioServicio();
            ViewBag.LST_MODOSERVICIO = FnListarModoservicioServicio();
            ViewBag.LST_TIPOACCESO = FnListarTipoAcceso();
            ViewBag.LST_ESTADO_SERVICIO = FnListarEstadoServicio();

            return View(models);
        }

        [HttpPost]
        public ActionResult Index(int ID_SERVICIO, int ID_TIPOSERVICIO, int ID_ENTIDAD, int ID_MODOSERVICIO, string NOMBRE, int ID_TIPOACCESO
            , string LINK, int ORDEN, int ESTADO_VIGENCIA, int ID_OPERACION, string DESCRIPCION, string ACCION, int ID_PARAMETRO, string PARAMETRO
            , int ID_OBLIGATORIO, int ID_TIPO_PARAMETRO, string COMENTARIO, string DNI_URL)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            ServicioModels models = new ServicioModels();

            ViewBag.POST = 0;
            ViewBag.wpv = 1;
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;
            ViewBag.LST_ENTIDAD_SERVICIO = FnListarEntidadServicio();
            ViewBag.LST_TIPOSERVICIO_SERVICIO = FnListarTiposervicioServicio();
            ViewBag.LST_MODOSERVICIO = FnListarModoservicioServicio();
            ViewBag.LST_TIPOACCESO = FnListarTipoAcceso();
            ViewBag.LST_ESTADO_SERVICIO = FnListarEstadoServicio();

            if (DESCRIPCION.Length > 0) //operacion
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEOperacion()
                {
                    ID_OPERACION = ID_OPERACION,
                    ID_SERVICIO = ID_SERVICIO,
                    DESCRIPCION = DESCRIPCION,
                    ACCION = ACCION,                    
                    USUARIO_REGISTRA = oBEUsuario.USUARIO
                }));

                var strRespuesta = PostJSON("operacion", postBytes);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<BEOperacion>>(strRespuesta);

                models.intNumeroPaginaServicio = 1;
                return View(models);
            }
            else if (PARAMETRO.Length > 0) { //parametro
                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEParametro()
                {
                    ID_PARAMETRO = ID_PARAMETRO,
                    ID_OPERACION = ID_OPERACION,
                    PARAMETRO = PARAMETRO,
                    ID_OBLIGATORIO = ID_OBLIGATORIO,
                    ID_TIPO_PARAMETRO = ID_TIPO_PARAMETRO,
                    COMENTARIO = COMENTARIO,
                    USUARIO_REGISTRA = oBEUsuario.USUARIO
                }));

                var strRespuesta = PostJSON("parametro", postBytes);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<BEParametro>>(strRespuesta);

                models.intNumeroPaginaServicio = 1;
                return View(models);
            }
            else
            {//para Servicio

                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEServicio()
                {
                    ID_SERVICIO = ID_SERVICIO,
                    ID_TIPOSERVICIO = ID_TIPOSERVICIO,
                    ID_ENTIDAD = ID_ENTIDAD,
                    ID_MODOSERVICIO = ID_MODOSERVICIO,
                    NOMBRE = NOMBRE,
                    ID_TIPOACCESO = ID_TIPOACCESO,
                    LINK = LINK,
                    ORDEN = ORDEN,                    
                    ESTADO_VIGENCIA = ESTADO_VIGENCIA,
                    USUARIO_REGISTRA = oBEUsuario.USUARIO,
                    DNI_URL = DNI_URL
                }));

                var strRespuesta = PostJSON("servicio", postBytes);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<BEEntidad>>(strRespuesta);

                models.intNumeroPaginaServicio = 1;
                return View(models);
            }
        }

        [HttpPost]
        public ActionResult nuevaServicio(ServicioModels model)
        {
            return RedirectToAction("Index", "AdminServicio", new { area = "Configuracion", wpv = "1" });
        }

        [HttpGet]
        public ActionResult nuevaServicio(int ID_SERVICIO)
        {
            ViewBag.LST_ENTIDAD_SERVICIO = FnListarEntidadServicio();
            ViewBag.LST_TIPOSERVICIO_SERVICIO = FnListarTiposervicioServicio();

            
            if (ID_SERVICIO == 0)
                return PartialView("Nuevo", new ServicioModels() { ID_SERVICIO = 0, TIPOSERVICIO = 0, ID_ENTIDAD = 0, MODOSERVICIO = 0, NOMBRE = string.Empty, LINK = string.Empty, ORDEN = 0, ESTADO_VIGENCIA = 0, DNI_URL = "0" });
            else
            {
                string strCadenaPedicion = string.Concat("servicio?ID_SERVICIO=", Dame_Entero(ID_SERVICIO));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicio>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                    return PartialView("Nuevo", new ServicioModels() { ID_SERVICIO = data_.ID_SERVICIO, TIPOSERVICIO = data_.ID_TIPOSERVICIO, ID_ENTIDAD = data_.ID_ENTIDAD, TIPOACCESO = data_.ID_TIPOACCESO, MODOSERVICIO = data_.ID_MODOSERVICIO, NOMBRE = data_.NOMBRE, LINK = data_.LINK, ORDEN = data_.ORDEN, ESTADO_VIGENCIA = data_.ESTADO_VIGENCIA, DNI_URL = data_.DNI_URL });
                }
                else
                {
                    return PartialView("Nuevo", new ServicioModels() { ID_SERVICIO = 0, TIPOSERVICIO = 0, ID_ENTIDAD = 0, MODOSERVICIO = 0, TIPOACCESO = 0, NOMBRE = string.Empty, LINK = string.Empty, ORDEN = 0, ESTADO_VIGENCIA = 0, DNI_URL = "0" });
                }
            }
        }

        [HttpGet]
        public ActionResult nuevaOperacion(int ID_OPERACION, int ID_SERVICIO, string NOMBRE)
        {
            if (!(string.IsNullOrEmpty(NOMBRE))) {
                int cont = NOMBRE.IndexOf('│');
                while (cont > 0) {
                    NOMBRE = NOMBRE.Replace('│', ' ');
                    cont = NOMBRE.IndexOf('│');
                }
            }
                
            if (ID_OPERACION == 0)
                return PartialView("NuevoOperacion", new OperacionModels() { ID_OPERACION = 0, ID_SERVICIO = ID_SERVICIO, SERVICIO = NOMBRE, DESCRIPCION = string.Empty, ACCION = string.Empty });
            else
            {
                string strCadenaPedicion = string.Concat("operacion?ID_OPERACION=", Dame_Entero(ID_OPERACION));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEOperacion>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                    return PartialView("NuevoOperacion", new OperacionModels() { ID_OPERACION = data_.ID_OPERACION, ID_SERVICIO = data_.ID_SERVICIO, SERVICIO = data_.SERVICIO, DESCRIPCION = data_.DESCRIPCION, ACCION = data_.ACCION });
                }
                else
                {
                    return PartialView("NuevoOperacion", new OperacionModels() { ID_OPERACION = 0, ID_SERVICIO = ID_SERVICIO, SERVICIO = NOMBRE, DESCRIPCION = string.Empty, ACCION = string.Empty });
                }
            }
        }

        [HttpGet]
        public ActionResult nuevaParametro(int ID_PARAMETRO, int ID_OPERACION, string NOMBRE)
        {
            if (!(string.IsNullOrEmpty(NOMBRE)))
            {
                int cont = NOMBRE.IndexOf('│');
                while (cont > 0)
                {
                    NOMBRE = NOMBRE.Replace('│', ' ');
                    cont = NOMBRE.IndexOf('│');
                }
            }

            if (ID_PARAMETRO == 0)
                return PartialView("NuevoParametro", new ParametroModels() { ID_PARAMETRO = 0, ID_OPERACION = 0, OPERACION = NOMBRE, PARAMETRO = string.Empty, ID_OBLIGATORIO = 0, ID_TIPO_PARAMETRO = 0, COMENTARIO = string.Empty });
            else
            {
                string strCadenaPedicion = string.Concat("parametro?ID_PARAMETRO=", Dame_Entero(ID_PARAMETRO));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEParametro>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                    return PartialView("NuevoParametro", new ParametroModels() { ID_PARAMETRO = data_.ID_PARAMETRO, OPERACION = data_.OPERACION, ID_OPERACION = data_.ID_OPERACION, PARAMETRO = data_.PARAMETRO, ID_OBLIGATORIO = data_.ID_OBLIGATORIO, ID_TIPO_PARAMETRO = data_.ID_TIPO_PARAMETRO, COMENTARIO = data_.COMENTARIO });
                }
                else
                {
                    return PartialView("NuevoParametro", new ParametroModels() { ID_PARAMETRO = 0, ID_OPERACION = 0, OPERACION = NOMBRE, PARAMETRO = string.Empty, ID_OBLIGATORIO = 0, ID_TIPO_PARAMETRO = 0, COMENTARIO = string.Empty });
                }
            }
        }

        public JsonResult ListarServicio(ServicioModels models)
        {
            string strCadenaPedicion = string.Concat("servicio?ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                     "&&ID_TIPOSERVICIO=", Dame_Entero(models.TIPOSERVICIO),
                                                     "&&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                     "&&ID_MODOSERVICIO=", Dame_Entero(models.MODOSERVICIO),
                                                     "&&NOMBRE=", Dame_Texto(models.NOMBRE),
                                                     "&&ID_TIPOACCESO=", Dame_Entero(models.TIPOACCESO),
                                                     "&&LINK=", Dame_Texto(models.LINK),
                                                     "&&ORDEN=", Dame_Entero(models.ORDEN),
                                                     "&&ESTADO_VIGENCIA=", Dame_Entero(models.ESTADO_VIGENCIA),
                                                     "&&PAGINADO=", Dame_Entero(models.intNumeroPaginaServicio),
                                                     "&&TIPO=1");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicio>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return Json(ObjetoJSON.Data.Result, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarOperacion(OperacionModels models)
        {
            string strCadenaPedicion = string.Concat("operacion?ID_OPERACION=", Dame_Entero(models.ID_OPERACION),
                                                     "&&ID_SERVICIO=", Dame_Entero(models.ID_SERVICIO),
                                                     "&&DESCRIPCION=", Dame_Texto(models.DESCRIPCION),
                                                     "&&ACCION=", Dame_Texto(models.ACCION),                                                     
                                                     "&&PAGINADO=", Dame_Entero(models.intNumeroPaginaOperacion),
                                                     "&&TIPO=1");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEOperacion>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return Json(ObjetoJSON.Data.Result, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarParametro(ParametroModels models)
        {
            string strCadenaPedicion = string.Concat("parametro?ID_PARAMETRO=", Dame_Entero(models.ID_PARAMETRO),
                                                    "&&ID_OPERACION=", Dame_Entero(models.ID_OPERACION),
                                                    "&&PARAMETRO=", string.Empty,
                                                    "&&ID_TIPO_PARAMETRO=0",
                                                    "&&COMENTARIO=", string.Empty,
                                                    "&&PAGINADO=", Dame_Entero(models.intNumeroPaginaParametro),
                                                    "&&TIPO=1");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEParametro>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return Json(ObjetoJSON.Data.Result, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.AllowGet);
        }

        public SelectList FnListarEntidadServicio()
        {
            string COD_UBIGEO = string.Empty;

            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", 0,
                                                     "&&NOMBRE=", string.Empty,
                                                     "&&RUC=", string.Empty,
                                                     "&&COD_UBIGEO=", string.Empty,
                                                     "&&ORDEN=", 0,
                                                     "&&ES_MUNICIPALIDAD=", "false",
                                                     "&&PAGINADO=1",
                                                     "&&TIPO=3");
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
        public SelectList FnListarTiposervicioServicio() {
            List<BEEnumerado> data = new List<BEEnumerado>();
            data.Add(new BEEnumerado { ID_ENUMERADO = 1, DESCRIPCION = "Parcial" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 2, DESCRIPCION = "Completo" });
            return new SelectList(data, "ID_ENUMERADO", "DESCRIPCION");
        }

        public SelectList FnListarModoservicioServicio()
        {
            List<BEEnumerado> data = new List<BEEnumerado>();
            data.Add(new BEEnumerado { ID_ENUMERADO = 1, DESCRIPCION = "Pagina Web" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 2, DESCRIPCION = "Servicio Web" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 3, DESCRIPCION = "Enlace Web Externo" });
            return new SelectList(data, "ID_ENUMERADO", "DESCRIPCION");
        }

        public SelectList FnListarTipoAcceso()
        {
            List<BEEnumerado> data = new List<BEEnumerado>();
            data.Add(new BEEnumerado { ID_ENUMERADO = 1, DESCRIPCION = "Todos" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 2, DESCRIPCION = "Propio" });
            return new SelectList(data, "ID_ENUMERADO", "DESCRIPCION");
        }

        public SelectList FnListarEstadoServicio()
        {
            List<BEEnumerado> data = new List<BEEnumerado>();
            data.Add(new BEEnumerado { ID_ENUMERADO = 1, DESCRIPCION = "Activo" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 2, DESCRIPCION = "Desactivo" });
            return new SelectList(data, "ID_ENUMERADO", "DESCRIPCION");
        }
    }
}
