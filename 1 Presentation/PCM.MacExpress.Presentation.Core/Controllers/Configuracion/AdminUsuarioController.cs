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
    public class AdminUsuarioController : WebBaseController
    {
        private static List<BEUsuario> data = new List<BEUsuario>();
        private static List<BESede> dataSede = new List<BESede>();

        [HttpGet]
        public ActionResult Index(string wpv)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");
            
            UsuarioModels models = new UsuarioModels();

            models.intNumeroPaginaUsuario = 1;
            models.ID_USUARIO = 0;

            ViewBag.POST = 0;
            ViewBag.wpv = Dame_Entero(wpv);
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;
            ViewBag.LST_ESTADO_USUARIO = FnListarEstadoUsuario();
            ViewBag.LST_ENTIDAD_USUARIO = FnListarEntidadUsuario();
            ViewBag.LST_SEDE_USUARIO = FnListarSedeUsuario();
            ViewBag.LST_TIPO_USUARIO = FnListarTipoUsuario();

            return View(models);
        }

        [HttpPost]
        public ActionResult Index(int ID_USUARIO, int? ID_ENTIDAD, int? ID_SEDE, string DNI, string NOMBRE, string APELLIDO_PATERNO, string APELLIDO_MATERNO, string USUARIO, string PASSWORD, DateTime FECHA_CADUCA, int ESTADO_VIGENCIA, int TIPO_USUARIO)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            UsuarioModels models = new UsuarioModels();
            models.ID_SEDE = 0;
            models.intNumeroPaginaUsuario = 1;

            byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEUsuario()
            {
                ID_USUARIO = ID_USUARIO,
                ID_ENTIDAD = ID_ENTIDAD,
                ID_SEDE = ID_SEDE,
                DNI = DNI,
                NOMBRE = NOMBRE,
                APELLIDO_PATERNO = APELLIDO_PATERNO,
                APELLIDO_MATERNO = APELLIDO_MATERNO,
                USUARIO = USUARIO,
                PASSWORD = PASSWORD,
                FECHA_CADUCA = FECHA_CADUCA,
                ESTADO_VIGENCIA = ESTADO_VIGENCIA,
                TIPO_USUARIO = TIPO_USUARIO,
                USUARIO_REGISTRA = oBEUsuario.USUARIO
            }));

            var strRespuesta = PostJSON("usuario", postBytes);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<BEUsuario>>(strRespuesta);
            
            ViewBag.POST = 0;
            ViewBag.wpv = 1;
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;
            ViewBag.LST_ESTADO_USUARIO = FnListarEstadoUsuario();
            ViewBag.LST_ENTIDAD_USUARIO = FnListarEntidadUsuario();
            ViewBag.LST_SEDE_USUARIO = FnListarSedeUsuario();
            ViewBag.LST_TIPO_USUARIO = FnListarTipoUsuario();

            return View(models);
        }

        [HttpGet]
        public ActionResult nuevaUsuario(int ID_USUARIO)
        {
            ViewBag.LST_TIPO_USUARIO = FnListarTipoUsuario();
            ViewBag.LST_ENTIDAD_USUARIO = FnListarEntidadUsuario();
            ViewBag.LST_SEDE_USUARIO = FnListarSedeUsuario();

            if (ID_USUARIO == 0)
                return PartialView("Nuevo", new UsuarioModels() { ID_USUARIO = 0, ID_ENTIDAD = 0, DNI = string.Empty, NOMBRE = string.Empty, APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty, USUARIO = string.Empty, PASSWORD = string.Empty, FECHA_CADUCA = DateTime.Now.AddYears(1), ESTADO_VIGENCIA = 0, TIPO_USUARIO = 0 });
            else
            {
                string strCadenaPedicion = string.Concat("usuario?ID_USUARIO=", Dame_Entero(ID_USUARIO));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEUsuario>>>(strRespuesta);
                
                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                    if(data_.ID_ENTIDAD != null)
                        ViewBag.LST_SEDE_USUARIO = new SelectList(fnListarEntidad(new BESede() { ID_ENTIDAD = data_.ID_ENTIDAD.Value }), "ID_SEDE", "NOMBRE");

                    return PartialView("Nuevo", new UsuarioModels() { ID_USUARIO = data_.ID_USUARIO, ID_ENTIDAD = data_.ID_ENTIDAD, ID_SEDE = data_.ID_SEDE, DNI = data_.DNI, NOMBRE = data_.NOMBRE, APELLIDO_PATERNO = data_.APELLIDO_PATERNO, APELLIDO_MATERNO = data_.APELLIDO_MATERNO, USUARIO = data_.USUARIO, PASSWORD = data_.PASSWORD, FECHA_CADUCA = data_.FECHA_CADUCA, ESTADO_VIGENCIA = data_.ESTADO_VIGENCIA, TIPO_USUARIO = data_.TIPO_USUARIO });
                }
                else
                    return PartialView("Nuevo", new UsuarioModels() { ID_USUARIO = 0, ID_ENTIDAD = 0, DNI = string.Empty, NOMBRE = string.Empty, APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty, USUARIO = string.Empty, PASSWORD = string.Empty, FECHA_CADUCA = DateTime.Now.AddYears(1), ESTADO_VIGENCIA = 0, TIPO_USUARIO = 0 });
            }
        }

        public JsonResult ListarUsuario(UsuarioModels models)
        {
            string strCadenaPedicion = string.Concat("usuario?ID_USUARIO=", Dame_Entero(models.ID_USUARIO),
                                                    "&&ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                    "&&ID_SEDE=", Dame_Entero(models.ID_SEDE),
                                                    "&&DNI=", Dame_Texto(models.DNI),
                                                    "&&NOMBRE=", Dame_Texto(models.NOMBRE),
                                                    "&&APELLIDO_PATERNO=", Dame_Texto(models.APELLIDO_PATERNO),
                                                    "&&APELLIDO_MATERNO=", Dame_Texto(models.APELLIDO_MATERNO),
                                                    "&&USUARIO=", Dame_Texto(models.USUARIO),
                                                    "&&ESTADO_VIGENCIA=", Dame_Entero(models.ESTADO_VIGENCIA),
                                                    "&&TIPO_USUARIO=", Dame_Entero(models.TIPO_USUARIO),                                                    
                                                    "&&PAGINADO=", Dame_Entero(models.intNumeroPaginaUsuario),
                                                    "&&TIPO=", Dame_Entero(models.TIPO));
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEUsuario>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return Json(ObjetoJSON.Data.Result, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CambiarContrasena(cambiarcontrasenaModel models) 
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

            ProcesoGenericoData<string> Retorna = new ProcesoGenericoData<string>();

            string strCadenaPedicion = string.Concat("usuario?ID_USUARIO=", Dame_Entero(oBEUsuario.ID_USUARIO));
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEUsuario>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
            {
                var data_ = ObjetoJSON.Data.Result.FirstOrDefault();

                if(data_.PASSWORD == models.contrasenaActual) 
                {
                    byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEUsuario()
                    {
                        ID_USUARIO = data_.ID_USUARIO,
                        ID_ENTIDAD = data_.ID_ENTIDAD,
                        ID_SEDE = data_.ID_SEDE,
                        DNI = data_.DNI,
                        NOMBRE = data_.NOMBRE,
                        APELLIDO_PATERNO = data_.APELLIDO_PATERNO,
                        APELLIDO_MATERNO = data_.APELLIDO_MATERNO,
                        USUARIO = data_.USUARIO,
                        PASSWORD = models.contrasenaNueva,
                        FECHA_CADUCA = data_.FECHA_CADUCA,
                        ESTADO_VIGENCIA = data_.ESTADO_VIGENCIA,
                        TIPO_USUARIO = data_.TIPO_USUARIO,
                        USUARIO_REGISTRA = oBEUsuario.USUARIO
                    }));

                    strRespuesta = PostJSON("usuario", postBytes);
                    var ObjetoJSON_ = JsonConvert.DeserializeObject<ProcesoGenerico<BEUsuario>>(strRespuesta);

                    Retorna.IsSuccess = true;
                    Retorna.Message = "Actualizado correctamente";
                }
                else {
                    Retorna.IsSuccess = false;
                    Retorna.Message="La contraseña actual es incorrecta";
                }
            }

            return Json(Retorna, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CambiarClave()
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            return PartialView("CambiarClave");
        }

        public SelectList FnListarEstadoUsuario()
        {
            List<BEEnumerado> data = new List<BEEnumerado>();
            data.Add(new BEEnumerado { ID_ENUMERADO = 1, DESCRIPCION = "Activo" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 2, DESCRIPCION = "Desactivo" });
            return new SelectList(data, "ID_ENUMERADO", "DESCRIPCION");
        }

        public SelectList FnListarEntidadUsuario()
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

        public SelectList FnListarSedeUsuario()
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "SELECCIONE", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarTipoUsuario()
        {
            List<BEEnumerado> data = new List<BEEnumerado>();
            data.Add(new BEEnumerado { ID_ENUMERADO = 1, DESCRIPCION = "Asesor" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 2, DESCRIPCION = "Supervisor Municipal" });
            data.Add(new BEEnumerado { ID_ENUMERADO = 3, DESCRIPCION = "Administrador" });

            return new SelectList(data, "ID_ENUMERADO", "DESCRIPCION");
        }

        public JsonResult ListarEntidad(BESede models)
        {
            var data = fnListarEntidad(models);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        public List<BESede> fnListarEntidad(BESede models)
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
                data.Add(new BESede { ID_SEDE = 0, NOMBRE = "Seleccione" });
            }
            return data;
        }

        public JsonResult ListarPersonaExterna(string dni)
        {
            BECiudadano oBECiudadano = DniReniec(dni);
            return Json(oBECiudadano, JsonRequestBehavior.AllowGet);
        }
    }
}
