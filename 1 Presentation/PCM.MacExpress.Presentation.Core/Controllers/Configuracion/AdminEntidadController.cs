using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCM.MacExpress.Presentation.Core.ViewModel;
using PCM.MacExpress.Presentation.Core.Infraestructura;
using Newtonsoft.Json;
using Service.Trasnversal;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace PCM.MacExpress.Presentation.Core.Controllers.Configuracion
{
    public class AdminEntidadController : WebBaseController
    {
        public static List<BEEntidad> data = new List<BEEntidad>();

        [HttpGet]
        public ActionResult Index(string wpv)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            EntidadModels models = new EntidadModels();

            models.intNumeroPaginaEntidad = 1;
            models.ID_ENTIDAD = 0;

            ViewBag.POST = 0;
            ViewBag.wpv = Dame_Entero(wpv);
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;

            return View(models);
        }

        [HttpPost]
        public ActionResult Index(int ID_ENTIDAD, string NOMBRE, string RUC, string DEPARTAMENTO, string PROVINCIA, string DISTRITO, string COD_UBIGEO, int ORDEN, bool ES_MUNICIPALIDAD)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            EntidadModels models = new EntidadModels();

            models.intNumeroPaginaEntidad = 1;
            models.ID_ENTIDAD = ID_ENTIDAD;
            models.NOMBRE = NOMBRE;
            models.RUC = RUC;
            models.DEPARTAMENTO = DEPARTAMENTO;
            models.PROVINCIA = PROVINCIA;
            models.DISTRITO = DISTRITO;
            models.ORDEN = ORDEN;
            models.ES_MUNICIPALIDAD = ES_MUNICIPALIDAD;

            byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEEntidad() {
                NOMBRE = NOMBRE,
                RUC = RUC,
                COD_UBIGEO = COD_UBIGEO,
                DEPARTAMENTO = DEPARTAMENTO,
                PROVINCIA = PROVINCIA,
                DISTRITO = DISTRITO,
                ORDEN = ORDEN,
                ES_MUNICIPALIDAD = ES_MUNICIPALIDAD
            }));

            var respuesta = PostJSON("entidad", postBytes);

            ViewBag.POST = 0;
            ViewBag.wpv = 1;
            ViewBag.PaginaActual = 1;
            ViewBag.TotalRegistros = 0;

            if (ID_ENTIDAD == 0) {
                int Contador = data.Count() + 1;
                data.Add(new BEEntidad { FILA = Contador, TOTAL = Contador, PAGINADO = 10, ID_ENTIDAD = Contador, NOMBRE = NOMBRE, RUC = RUC, DEPARTAMENTO = DEPARTAMENTO, PROVINCIA = PROVINCIA, DISTRITO = DISTRITO, ORDEN = ORDEN, ES_MUNICIPALIDAD = ES_MUNICIPALIDAD });
            }

            return View(models);
        }

        [HttpPost]
        public ActionResult nuevaEntidad(EntidadModels model)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            //DataTable DT = new DataTable();
            //var ImgArchivo = (byte[])(DT.Rows[0]["BLARCHIVORNC"]);
            //if (ImgArchivo != null) 
            //{
            //    Response.Buffer = true;
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + DT.Rows[0]["TXARCHIVO"].ToString());
            //    Response.ContentType = "PDF";
            //    Response.BinaryWrite(ImgArchivo);
            //    Response.Flush();
            //    Response.Close();
            //}
            //return View()

            MemoryStream targetAdjunto = new MemoryStream();
            Double kb;
            string RUTA_LOGO = (string.IsNullOrEmpty(model.FullAdjuntoNombre) ? string.Empty : model.FullAdjuntoNombre);

            if (model.FullAdjunto != null)
            {
                model.FullAdjunto.InputStream.CopyTo(targetAdjunto);
                var ext = Path.GetExtension(model.FullAdjunto.FileName).ToLower();
                if (!(ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".tif")))
                {
                    throw new Exception("El archivo[2] de imagen debe de ser PNG, JPG, TIF,O GIF");
                }
                kb = model.FullAdjunto.ContentLength / 1024;
                if (kb > 2048)
                {
                    throw new Exception("El archivo del acta supera los 2mb [2048 kb] permitidos, Vuelva a adjuntar todos los archivos");
                }

                string path = Server.MapPath("~/Uploads/Logo");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                model.FullAdjunto.SaveAs(path + "/" + Path.GetFileName(model.FullAdjunto.FileName));
            }

            byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEEntidad()
            {
                ID_ENTIDAD = model.ID_ENTIDAD,
                NOMBRE = model.NOMBRE,
                RUC = model.RUC,
                COD_UBIGEO = string.Concat(model.DEPARTAMENTO, model.PROVINCIA, model.DISTRITO),
                ORDEN = model.ORDEN,
                RUTA_LOGO = RUTA_LOGO,
                ES_MUNICIPALIDAD = model.ES_MUNICIPALIDAD,
                USUARIO_REGISTRA = oBEUsuario.USUARIO
            }));

            var strRespuesta = PostJSON("entidad", postBytes);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<BEEntidad>>(strRespuesta);

            return RedirectToAction("Index", "AdminEntidad", new { area = "Configuracion", wpv = "1" });
        }

        [HttpGet]
        public ActionResult nuevaEntidad(int ID_ENTIDAD)
        {
            ViewBag.LST_DEPARTAMENTO_ENTIDAD = FnListarDepartamento();

            if (ID_ENTIDAD == 0)
            {
                ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BEEntidad());
                ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BEEntidad());

                return PartialView("Nuevo", new EntidadModels() { ID_ENTIDAD = 0, NOMBRE = string.Empty, RUC = string.Empty, DEPARTAMENTO = string.Empty, PROVINCIA = string.Empty, DISTRITO = string.Empty, ORDEN = 0, ES_MUNICIPALIDAD = false, FullAdjuntoNombre = String.Empty });
            }
            else
            {
                string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", Dame_Entero(ID_ENTIDAD));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                    string COD_UBIGEO_DEPARTAMENTO = string.Empty, COD_UBIGEO_PROVINCIA = string.Empty, COD_UBIGEO_DISTRITO = string.Empty;
                    if (!(string.IsNullOrEmpty(data_.COD_UBIGEO)))
                    {
                        COD_UBIGEO_DEPARTAMENTO = data_.COD_UBIGEO.Substring(0, 2);
                        COD_UBIGEO_PROVINCIA = data_.COD_UBIGEO.Substring(2, 2);
                        COD_UBIGEO_DISTRITO = data_.COD_UBIGEO.Substring(4);

                        ViewBag.LST_PROVINCIA_ENTIDAD = new SelectList(fnListarUbigeo(new BEEntidad() { CONDICION = 2, COD_UBIGEO_DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO, COD_UBIGEO_PROVINCIA = COD_UBIGEO_PROVINCIA }), "COD_UBIGEO", "PROVINCIA");
                        ViewBag.LST_DISTRITO_ENTIDAD = new SelectList(fnListarUbigeo(new BEEntidad() { CONDICION = 3, COD_UBIGEO_DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO, COD_UBIGEO_PROVINCIA = COD_UBIGEO_PROVINCIA }), "COD_UBIGEO", "DISTRITO");
                    }
                    else {
                        ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BEEntidad());
                        ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BEEntidad());
                    }

                    return PartialView("Nuevo", new EntidadModels() { ID_ENTIDAD = data_.ID_ENTIDAD, NOMBRE = data_.NOMBRE, DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO, PROVINCIA = COD_UBIGEO_PROVINCIA, DISTRITO = COD_UBIGEO_DISTRITO, RUC = data_.RUC, ORDEN = data_.ORDEN, ES_MUNICIPALIDAD = data_.ES_MUNICIPALIDAD, FullAdjuntoNombre = data_.RUTA_LOGO });
                }
                else {
                    ViewBag.LST_PROVINCIA_ENTIDAD = FnListarProvincia(new BEEntidad());
                    ViewBag.LST_DISTRITO_ENTIDAD = FnListarDistrito(new BEEntidad());

                    return PartialView("Nuevo", new EntidadModels() { ID_ENTIDAD = 0, NOMBRE = string.Empty, RUC = string.Empty, DEPARTAMENTO = string.Empty, PROVINCIA = string.Empty, DISTRITO = string.Empty, ORDEN = 0, ES_MUNICIPALIDAD = false, FullAdjuntoNombre = string.Empty });
                }
            }
        }

        [HttpPost]
        public ActionResult obtenerAdjunto(int ID_ENTIDAD)
        {
            string lsNombre = string.Empty;
            Byte[] lbArchivo = new Byte[] { 0 };

            try
            {
                string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", Dame_Entero(ID_ENTIDAD));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

                var data_ = ObjetoJSON.Data.Result.FirstOrDefault();
                if (!(string.IsNullOrEmpty(data_.RUTA_LOGO)))
                {
                    lsNombre = Dame_Texto(data_.RUTA_LOGO);
                    string path = Server.MapPath("~/Uploads/Logo") + "/" + lsNombre;
                    lbArchivo = GetFile(path);
                }
            }
            catch (Exception ex) { }
            

            return File(lbArchivo, System.Net.Mime.MediaTypeNames.Application.Octet, lsNombre);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        public JsonResult ListarEntidad(EntidadModels models)
        {
            string COD_UBIGEO = string.Concat(Dame_Texto(models.DEPARTAMENTO), Dame_Texto(models.PROVINCIA), Dame_Texto(models.DISTRITO));
            
            string strCadenaPedicion = string.Concat("entidad?ID_ENTIDAD=", Dame_Entero(models.ID_ENTIDAD),
                                                     "&&NOMBRE=", Dame_Texto(models.NOMBRE), 
                                                     "&&RUC=", Dame_Texto(models.RUC), 
                                                     "&&COD_UBIGEO=", COD_UBIGEO, 
                                                     "&&ORDEN=", Dame_Entero(models.ORDEN),
                                                     "&&ES_MUNICIPALIDAD=", "false",
                                                     "&&PAGINADO=", Dame_Entero(models.intNumeroPaginaEntidad),
                                                     "&&TIPO=1");
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return Json(ObjetoJSON.Data.Result, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEntidadExterna(string valor)
        {
            string controlador = "RazonSocial?RSocial=" + valor;
            List <BEEntidad> LstBEEntidad = RazonSocialSunat(controlador);
            return Json(LstBEEntidad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarUbigeoExterno(string ruc)
        {
            string controlador = "DatosPrincipales?numruc=" + ruc;
            List<BEEntidad> LstBEEntidad = RazonSocialSunat(controlador);
            return Json(LstBEEntidad, JsonRequestBehavior.AllowGet);
        }

        public SelectList FnListarDepartamento()
        {
            List<BEEntidad> data = new List<BEEntidad>();
            string strCadenaPedicion = "entidad?CONDICION=1&&COD_UBIGEO_DEPARTAMENTO=01&&COD_UBIGEO_PROVINCIA=01";
            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                return new SelectList(ObjetoJSON.Data.Result, "COD_UBIGEO", "DEPARTAMENTO");
            else {
                data.Add(new BEEntidad { COD_UBIGEO = "0", DEPARTAMENTO = "Seleccione" });
                return new SelectList(data, "COD_UBIGEO", "DEPARTAMENTO");
            }            
        }

        public SelectList FnListarProvincia(BEEntidad oBEEntidad)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "SELECCIONE", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public SelectList FnListarDistrito(BEEntidad oBEEntidad)
        {
            List<SelectListItem> items_blanco = new List<SelectListItem>();
            items_blanco.Add(new SelectListItem() { Text = "SELECCIONE", Value = "", Selected = true });
            return new SelectList(items_blanco, "Value", "Text");
        }

        public JsonResult ListarUbigeo(BEEntidad models)
        {
            var data = fnListarUbigeo(models);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public List<BEEntidad> fnListarUbigeo(BEEntidad oBEEntidad) {

            List<BEEntidad> data = new List<BEEntidad>();
            string strCadenaPedicion = string.Concat("entidad?CONDICION=", oBEEntidad.CONDICION.ToString(),
                                                    "&&COD_UBIGEO_DEPARTAMENTO=", oBEEntidad.COD_UBIGEO_DEPARTAMENTO,
                                                    "&&COD_UBIGEO_PROVINCIA=", oBEEntidad.COD_UBIGEO_PROVINCIA);

            string strRespuesta = GetJSON(strCadenaPedicion);
            var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEEntidad>>>(strRespuesta);

            if (ObjetoJSON.Data.IsSuccess)
                data = ObjetoJSON.Data.Result;
            else
            {
                data.Add(new BEEntidad { COD_UBIGEO = "0", DEPARTAMENTO = "Seleccione" });
            }
            return data;
        }
    }
}
