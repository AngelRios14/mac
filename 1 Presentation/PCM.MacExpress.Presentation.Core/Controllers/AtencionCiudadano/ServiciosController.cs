using PCM.MacExpress.Presentation.Core.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCM.MacExpress.Presentation.Core.ViewModel;
using System.Configuration;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using RestSharp;
using System.IO;

namespace PCM.MacExpress.Presentation.Areas.AtencionCiudadano
{
    public class ServiciosController : WebBaseController
    {
        public static List<BEServicioAtencion> LstAtencion = new List<BEServicioAtencion>();

        // GET: AtencionCiudadano/Servicios
        public ActionResult Index(string wpv, string DNI, string NOMBRE, string APELLIDO_PATERNO, string APELLIDO_MATERNO, string FOTO,string CORREO, string TELEFONO, string EVENTO)
        {
            //var rutaBase = ConfigurationManager.AppSettings["rutaBase"];
            //BEServicio oBEServicio = new BEServicio() { ID_ENTIDAD = 1, ID_TIPOSERVICIO = 1, NOMBRE = "asdasdasdasd", LINK = "asdasdasd" };

            //----------------------------------GET----------------------------------
            //using (var client = new HttpClient()) {
            //    var resp = client.GetStringAsync(new Uri(rutaBase + "/5")).Result;
            //    var d = resp;
            //}
            //----------------------------------POST----------------------------------

            //using (var client = new HttpClient())
            //{
            //    var Content = new StringContent(
            //        JsonConvert.SerializeObject(oBEServicio),
            //        Encoding.UTF8,
            //        "application/json"
            //    );
            //
            //    var resp = client.PostAsync(new Uri(rutaBase), Content).Result;
            //    var d = resp;
            //}
            //--------------------------------------------------------------------

            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
            if (oBEUsuario == null)
                return RedirectToAction("Index", "Login");

            AtencionModels model = new AtencionModels();


            List<BEServicioAtencion> LstServicioAtencion = new List<BEServicioAtencion>();
            string strServicio = string.Empty;

            if (wpv == null)
                model = new AtencionModels();

            if (!(string.IsNullOrEmpty(DNI)) &&
                !(string.IsNullOrEmpty(NOMBRE)) &&
                !(string.IsNullOrEmpty(APELLIDO_PATERNO)) &&
                !(string.IsNullOrEmpty(APELLIDO_MATERNO)) &&
                !(string.IsNullOrEmpty(FOTO))
                )
            {
                model.DNI = DNI;
                model.NOMBRE = NOMBRE;
                model.APELLIDO_PATERNO = APELLIDO_PATERNO;
                model.APELLIDO_MATERNO = APELLIDO_MATERNO;
                model.FOTO = FOTO;
            }

            #region "SERVICIOS"
            if (wpv == "1" || wpv == "2")
            {
                string strCadenaPedicion = string.Concat("servicio?NOMBRE=", string.Empty,
                                                         "&&PAGINADO=1",
                                                         "&&TIPO=" + wpv,
                                                         "&&ID_ENTIDAD=" + Dame_Entero(oBEUsuario.ID_ENTIDAD), "&&TIPO_DATA=");

                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicioAtencion>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                    LstServicioAtencion = ObjetoJSON.Data.Result;
            }

            if (LstServicioAtencion.Count > 0)
            {
                model.TOTAL = LstServicioAtencion.FirstOrDefault().TOTAL;
                model.PAGINADO = 10;// LstServicioAtencion.FirstOrDefault().PAGINADO;
               // model.PAGINADO = 10;
                strServicio = string.Empty;
                LstServicioAtencion.ForEach(servicio => {
                    strServicio = string.Concat(strServicio,
                                                (strServicio.Length == 0 ? string.Empty : model.SEPARADOR[0]),
                                                servicio.ID_ENTIDAD.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_NOMENCLATURA, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_IMAGEN, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ANCHO_ALTO_LOGO, model.SEPARADOR[model.SEPARADOR.Length - 2],
                                                servicio.ID_SERVICIO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_TIPO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_LINK);
                });
            }

            model.SERVICIO = strServicio;
            #endregion


            #region CIUDADANO
            if (EVENTO == "IniciarAtencionTramite") {
                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEAtencion()
                {
                    ID_CIUDADANO = 0,
                    DNI = DNI,
                    NOMBRE = NOMBRE,
                    APELLIDO_PATERNO = APELLIDO_PATERNO,
                    APELLIDO_MATERNO = APELLIDO_MATERNO,
                    USUARIO_REGISTRA = oBEUsuario.USUARIO,
                    FOTO = FOTO,
                    TIPO = 1
                }));

                var strRespuesta_ = PostJSON("atencion", postBytes);
                var ObjetoJSONPOST = JsonConvert.DeserializeObject<ProcesoGenerico<BEAtencion>>(strRespuesta_);
                if (ObjetoJSONPOST.Data.IsSuccess)
                    oBEUsuario.ID_CIUDADANO = ObjetoJSONPOST.Data.Result.ID_CIUDADANO;

                oBEUsuario.CORREO = CORREO;
                oBEUsuario.TELEFONO = TELEFONO;
                Session["UserSession"] = oBEUsuario;
            }
            #endregion

            if (EVENTO == "CerrarTramite")
            {
                if (oBEUsuario.ID_ATENCION > 0)
                {
                    byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEAtencion()
                    {
                        ID_ATENCION = oBEUsuario.ID_ATENCION,
                        ID_SERVICIO = 0,
                        ID_CIUDADANO = 0,
                        ID_ENTIDAD = 0,
                        ID_USUARIO_REGISTRA = 0,
                        CORREO = string.Empty,
                        TELEFONO = string.Empty,
                        USUARIO_REGISTRA = oBEUsuario.USUARIO,
                        ES_FECHA_FIN = 1,
                        TIPO = 2
                    })); ;

                    var strRespuesta = PostJSON("atencion", postBytes);
                    var ObjetoJSONPOST = JsonConvert.DeserializeObject<ProcesoGenerico<BEAtencion>>(strRespuesta);
                }
            }

            ViewBag.wpv = Dame_Entero(wpv);

            return View(model);
        }

        public JsonResult ListarServicios(AtencionModels models)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

            AtencionModels model = new AtencionModels();
            List<BEServicioAtencion> LstServicioAtencion = new List<BEServicioAtencion>();
            string strServicio = string.Empty;

            string wpv = models.ACCION;
            if (wpv == "1" || wpv == "2")
            {
                string strCadenaPedicion = string.Concat("servicio?NOMBRE=", models.SERVICIO,
                                                         "&&PAGINADO=" + models.intNumeroPaginaAtencion,
                                                         "&&TIPO=" + wpv,
                                                         "&&ID_ENTIDAD=" + Dame_Entero(oBEUsuario.ID_ENTIDAD), "&&TIPO_DATA=");

                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicioAtencion>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                    LstServicioAtencion = ObjetoJSON.Data.Result;
            }

            if (LstServicioAtencion.Count > 0)
            {
                model.TOTAL = LstServicioAtencion.FirstOrDefault().TOTAL;
                model.PAGINADO = LstServicioAtencion.FirstOrDefault().PAGINADO;

                strServicio = string.Empty;
                LstServicioAtencion.ForEach(servicio => {
                    strServicio = string.Concat(strServicio,
                                                (strServicio.Length == 0 ? string.Empty : model.SEPARADOR[0]),
                                                servicio.ID_ENTIDAD.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_NOMENCLATURA, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_IMAGEN, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ANCHO_ALTO_LOGO, model.SEPARADOR[model.SEPARADOR.Length - 2],                                                
                                                servicio.ID_SERVICIO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_TIPO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_LINK);
                });
            }

            model.DNI = models.DNI;
            model.NOMBRE = models.NOMBRE;
            model.APELLIDO_PATERNO = models.APELLIDO_PATERNO;
            model.APELLIDO_MATERNO = models.APELLIDO_MATERNO;
            model.SERVICIO = strServicio;
            //if (wpv == "1" || wpv == "2")
            //{
            //    string strCadenaPedicion = string.Concat("servicio?NOMBRE=", models.SERVICIO,
            //                                             "&&PAGINADO=" + models.intNumeroPaginaAtencion,
            //                                             "&&TIPO=" + wpv,
            //                                             "&&ID_ENTIDAD=" + Dame_Entero(oBEUsuario.ID_ENTIDAD), "&&TIPO_DATA=COMBO");

            //    string strRespuesta = GetJSON(strCadenaPedicion);
            //   var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicioAtencion>>>(strRespuesta);
            
            //if (ObjetoJSON.Data.IsSuccess)
            //    LstServicioAtencion = ObjetoJSON.Data.Result;
            //}

            //if (LstServicioAtencion.Count > 0)
            //{
            //    model.TOTAL = LstServicioAtencion.FirstOrDefault().TOTAL;
            //    model.PAGINADO = LstServicioAtencion.FirstOrDefault().PAGINADO;

            //    strServicio = string.Empty;
            //    LstServicioAtencion.ForEach(servicio => {
            //        strServicio = string.Concat(strServicio,
            //                                    (strServicio.Length == 0 ? string.Empty : model.SEPARADOR[0]),
            //                                    servicio.ID_ENTIDAD.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.ENTIDAD_NOMENCLATURA, model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.ENTIDAD_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.ENTIDAD_IMAGEN, model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.ENTIDAD_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.ANCHO_ALTO_LOGO, model.SEPARADOR[model.SEPARADOR.Length - 2],
            //                                    servicio.ID_SERVICIO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.SERVICIO_TIPO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.SERVICIO_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.SERVICIO_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.SERVICIO_LINK, model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.ID_MODOSERVICIO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
            //                                    servicio.DNI_URL, model.SEPARADOR[model.SEPARADOR.Length - 1]);
            //    });
            //}
            //model.SERVICIO_COMBO = strServicio;

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AutocompletarServicios(AtencionModels models)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

            AtencionModels model = new AtencionModels();
            List<BEServicioAtencion> LstServicioAtencion = new List<BEServicioAtencion>();
            string strServicio = string.Empty;

            string wpv = models.ACCION;
            if (wpv == "1" || wpv == "2")
            {
                string strCadenaPedicion = string.Concat("servicio?NOMBRE=", models.SERVICIO,
                                                         "&&PAGINADO=" + models.intNumeroPaginaAtencion,
                                                         "&&TIPO=" + wpv,
                                                         "&&ID_ENTIDAD=" + Dame_Entero(oBEUsuario.ID_ENTIDAD), "&&TIPO_DATA=COMBO");

                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicioAtencion>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                    LstServicioAtencion = ObjetoJSON.Data.Result;
            }


            return Json(LstServicioAtencion, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult nuevaAtencion(string Id, string DNI, string NOMBRE, string APELLIDO_PATERNO, string APELLIDO_MATERNO, string WPV)
        {


            ViewBag.wpv = Dame_Entero(WPV);

            NOMBRE = DesencadenarPalabra(Dame_Texto(NOMBRE));
            APELLIDO_PATERNO = DesencadenarPalabra(Dame_Texto(APELLIDO_PATERNO));
            APELLIDO_MATERNO = DesencadenarPalabra(Dame_Texto(APELLIDO_MATERNO));

            if (Id == "0")
                return PartialView("nuevaAtencion", new AtencionModels() { DNI = string.Empty, NOMBRE = string.Empty, APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty, CORREO = string.Empty, TELEFONO = string.Empty, FOTO = string.Empty, SERVICIO = string.Empty, LINK = string.Empty });
            else
            {
                string strCadenaPedicion = string.Concat("servicio?ID_SERVICIO=", Dame_Entero(Id));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicio>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();

                    #region ATENCION

                    BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
                    if (oBEUsuario == null)
                        return RedirectToAction("Index", "Login");

                    byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEAtencion()
                    {
                        ID_ATENCION = 0,
                        ID_SERVICIO = data_.ID_SERVICIO,
                        ID_CIUDADANO = oBEUsuario.ID_CIUDADANO,
                        ID_ENTIDAD = oBEUsuario.ID_ENTIDAD.Value,
                        ID_USUARIO_REGISTRA = oBEUsuario.ID_USUARIO,
                        CORREO = oBEUsuario.CORREO,
                        TELEFONO = oBEUsuario.TELEFONO,
                        USUARIO_REGISTRA = oBEUsuario.USUARIO,
                        ES_FECHA_FIN = 0,
                        TIPO = 2
                    })); 

                    strRespuesta = PostJSON("atencion", postBytes);
                    var ObjetoJSONPOST = JsonConvert.DeserializeObject<ProcesoGenerico<BEAtencion>>(strRespuesta);
                    if (ObjetoJSONPOST.Data.IsSuccess)
                        oBEUsuario.ID_ATENCION = ObjetoJSONPOST.Data.Result.ID_ATENCION;

                    #endregion

                    
                    return PartialView("nuevaAtencion", new AtencionModels() { DNI_CO = DNI, DNI = DNI, NOMBRE_CO = NOMBRE + ' ' + APELLIDO_PATERNO + ' ' + APELLIDO_MATERNO, NOMBRE = NOMBRE, APELLIDO_PATERNO = APELLIDO_PATERNO, APELLIDO_MATERNO = APELLIDO_MATERNO, CORREO = string.Empty, TELEFONO = string.Empty, FOTO = string.Empty, SERVICIO = data_.NOMBRE, LINK = data_.LINK });
                }
                else
                {
                    return PartialView("nuevaAtencion", new AtencionModels() { DNI = string.Empty, NOMBRE = string.Empty, APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty, CORREO = string.Empty, TELEFONO = string.Empty, FOTO = string.Empty, SERVICIO = string.Empty, LINK = string.Empty });
                }
            }
        }

        //yual
        //solo para los enlaces externos
        [HttpGet]
        public JsonResult inicioAtencion(string Id, string DNI, string NOMBRE, string APELLIDO_PATERNO, string APELLIDO_MATERNO, string WPV)
        {
            ViewBag.wpv = Dame_Entero(WPV);

            NOMBRE = DesencadenarPalabra(Dame_Texto(NOMBRE));
            APELLIDO_PATERNO = DesencadenarPalabra(Dame_Texto(APELLIDO_PATERNO));
            APELLIDO_MATERNO = DesencadenarPalabra(Dame_Texto(APELLIDO_MATERNO));

            if (Id == "0")
                return Json("0", JsonRequestBehavior.AllowGet);
            // return PartialView("nuevaAtencion", new AtencionModels() { DNI = string.Empty, NOMBRE = string.Empty, APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty, CORREO = string.Empty, TELEFONO = string.Empty, FOTO = string.Empty, SERVICIO = string.Empty, LINK = string.Empty });
            else
            {
                string strCadenaPedicion = string.Concat("servicio?ID_SERVICIO=", Dame_Entero(Id));
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicio>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    var data_ = ObjetoJSON.Data.Result.FirstOrDefault();

                    #region ATENCION

                    BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

                    byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEAtencion()
                    {
                        ID_ATENCION = 0,
                        ID_SERVICIO = data_.ID_SERVICIO,
                        ID_CIUDADANO = oBEUsuario.ID_CIUDADANO,
                        ID_ENTIDAD = oBEUsuario.ID_ENTIDAD.Value,
                        ID_USUARIO_REGISTRA = oBEUsuario.ID_USUARIO,
                        CORREO = oBEUsuario.CORREO,
                        TELEFONO = oBEUsuario.TELEFONO,
                        USUARIO_REGISTRA = oBEUsuario.USUARIO,
                        ES_FECHA_FIN = 0,
                        TIPO = 2
                    }));

                    strRespuesta = PostJSON("atencion", postBytes);
                    var ObjetoJSONPOST = JsonConvert.DeserializeObject<ProcesoGenerico<BEAtencion>>(strRespuesta);
                    if (ObjetoJSONPOST.Data.IsSuccess)
                        oBEUsuario.ID_ATENCION = ObjetoJSONPOST.Data.Result.ID_ATENCION;

                    #endregion

                    return Json(oBEUsuario.ID_ATENCION, JsonRequestBehavior.AllowGet);
                    //  return PartialView("nuevaAtencion", new AtencionModels() { DNI_CO = DNI, DNI = DNI, NOMBRE_CO = NOMBRE + ' ' + APELLIDO_PATERNO + ' ' + APELLIDO_MATERNO, NOMBRE = NOMBRE, APELLIDO_PATERNO = APELLIDO_PATERNO, APELLIDO_MATERNO = APELLIDO_MATERNO, CORREO = string.Empty, TELEFONO = string.Empty, FOTO = string.Empty, SERVICIO = data_.NOMBRE, LINK = data_.LINK });
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                    //  return PartialView("nuevaAtencion", new AtencionModels() { DNI = string.Empty, NOMBRE = string.Empty, APELLIDO_PATERNO = string.Empty, APELLIDO_MATERNO = string.Empty, CORREO = string.Empty, TELEFONO = string.Empty, FOTO = string.Empty, SERVICIO = string.Empty, LINK = string.Empty });
                }
            }
        }
        
        //yual
        //solo para los enlaces externos
        [HttpGet]
        public JsonResult TerminarAtencion(int IdAtencion)
        {
            BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

           
                byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new BEAtencion()
                {
                    ID_ATENCION = IdAtencion,
                    ID_SERVICIO = 0,
                    ID_CIUDADANO = 0,
                    ID_ENTIDAD = 0,
                    ID_USUARIO_REGISTRA = 0,
                    CORREO = string.Empty,
                    TELEFONO = string.Empty,
                    USUARIO_REGISTRA = oBEUsuario.USUARIO,
                    ES_FECHA_FIN = 1,
                    TIPO = 2
                })); ;

                var strRespuesta = PostJSON("atencion", postBytes);
                var ObjetoJSONPOST = JsonConvert.DeserializeObject<ProcesoGenerico<BEAtencion>>(strRespuesta);
                return Json("1", JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListaServiciosEntidad(AtencionModels models,string strNombreServicio, int intPagina, string strTipo, int intEntidad)
        {
            //BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;

            AtencionModels model = new AtencionModels();
            List<BEServicioAtencion> LstServicioAtencion = new List<BEServicioAtencion>();
            string strServicio = string.Empty;

            //string wpv = models.ACCION;
            //if (strTipo == "1" || strTipo == "2")
            //{
                string strCadenaPedicion = string.Concat("servicio?NOMBRE=", strNombreServicio,
                                                         "&&PAGINADO=" + intPagina,
                                                         "&&TIPO=" + strTipo,
                                                         "&&ID_ENTIDAD=" + intEntidad, "&&TIPO_DATA=");

                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEServicioAtencion>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                    LstServicioAtencion = ObjetoJSON.Data.Result;
            //}

            if (LstServicioAtencion.Count > 0)
            {
                model.TOTAL = LstServicioAtencion.FirstOrDefault().TOTAL;
                model.PAGINADO = LstServicioAtencion.FirstOrDefault().PAGINADO;

                strServicio = string.Empty;
                LstServicioAtencion.ForEach(servicio => {
                    strServicio = string.Concat(strServicio,
                                                (strServicio.Length == 0 ? string.Empty : model.SEPARADOR[0]),
                                                servicio.ID_ENTIDAD.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_NOMENCLATURA, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_IMAGEN, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ENTIDAD_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ANCHO_ALTO_LOGO, model.SEPARADOR[model.SEPARADOR.Length - 2],
                                                servicio.ID_SERVICIO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_TIPO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],                                                
                                                servicio.SERVICIO_DESCRIPCION, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_ORDEN.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.SERVICIO_LINK, model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.ID_MODOSERVICIO.ToString(), model.SEPARADOR[model.SEPARADOR.Length - 1],
                                                servicio.DNI_URL, model.SEPARADOR[model.SEPARADOR.Length - 1]);
                });
            }

            model.DNI = models.DNI;
            model.NOMBRE = models.NOMBRE;
            model.APELLIDO_PATERNO = models.APELLIDO_PATERNO;
            model.APELLIDO_MATERNO = models.APELLIDO_MATERNO;
            model.SERVICIO = strServicio;

            

            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}
