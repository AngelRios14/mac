using PCM.MacExpress.Presentation.Core.Infraestructura;
using PCM.MacExpress.Presentation.Core.ViewModel;
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using Newtonsoft.Json;

namespace PCM.MacExpress.Presentation.Core.Controllers.Security
{
    public class LoginController : WebBaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["UserSession"] = null;
            GeneralModels models= new GeneralModels();
            models.MensajeResult = "";
            models.iRecordCount = 0;
            return View(models);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(GeneralModels models)
        {
            if (models != null)
            {
                if (string.IsNullOrEmpty(models.strCapcha) && models.iRecordCount>2)
                    models.MensajeResult = "Por favor ingrese un código Captcha";

                else if (Session["genCCod"] == null && models.iRecordCount> 2)
                    models.MensajeResult ="Ocurrió un error al obtener el código Captcha. Por favor recargue la página.";

                else
                {

                    bool parsedFlag = false;
                    Boolean.TryParse(ConfigurationManager.AppSettings["ByPassCaptchaCode"], out parsedFlag);
                    //if (!parsedFlag && models.iRecordCount>2)
                    //{
                        if (String.Compare(models.strCapcha, (string)Session["genCCod"], StringComparison.OrdinalIgnoreCase) != 0 && models.iRecordCount > 2)
                        {
                            models.MensajeResult = "No coincide con el texto de la imagen.";
                           
                        }
                        else
                        {
                            string mensaje = string.Empty;
                            bool autenticado = SiteSpecificAuthenticationMethod(models.strUsuario, models.strClave, ref mensaje);
                            //bool autenticado = true;

                            if (!(string.IsNullOrEmpty(mensaje)))
                                models.MensajeResult = mensaje;

                            models.MensajeResult = "El usuario y/o clave son inválidos";
                            models.Autentificado = autenticado;
                        }
                        //models.iRecordCount = models.iRecordCount + 1;
                    }

                //}
            }

            if (models.Autentificado)
            {
                BEUsuario oBEUsuario = Session["UserSession"] as BEUsuario;
                if (oBEUsuario != null)
                {
                    Session["nombreUsuario"] = string.Concat(oBEUsuario.NOMBRE, " ", oBEUsuario.APELLIDO_PATERNO, " ", oBEUsuario.APELLIDO_MATERNO);
                    Session["nombreRol"] = oBEUsuario.Lista_Permiso[oBEUsuario.Lista_Permiso.Count - 1].ROL;
                    Session["nombreEntidad"] = oBEUsuario.NOMBRE_ENTIDAD;
                }


                BERecurso oBERecurso = new BERecurso();

                if (oBEUsuario != null)
                    oBERecurso = oBEUsuario.Lista_Recurso.Where(recurso => recurso.RECURSO_DEFECTO == (int)PAGINA_DEFECTO.ESTA_POR_DEFECTO).FirstOrDefault();
                else
                    return View(models);

                if (oBERecurso != null)
                    if (string.IsNullOrEmpty(oBERecurso.AREA))
                        return RedirectToAction(oBERecurso.URLMENU, oBERecurso.PAGINA);
                    else
                        return RedirectToAction(oBERecurso.URLMENU, oBERecurso.PAGINA, new { area = oBERecurso.AREA });
                else
                    return View(models);
            }
            else
            {
                models.iRecordCount = models.iRecordCount + 1;
               // models.MensajeResult = "El usuario y/o clave son inválidos";
                return View(models);
            }
        }


        [HttpPost]
        public JsonResult GenerarCaptcha()
        {
            string codigo, imagenBase64;

            #region GenerarCapcha

            StringBuilder sb = new StringBuilder();
            Random oAzar = new Random();

            for (int i = 0; i < 4; i++)
            {
                sb.Append((char)(oAzar.Next(0, 26) + 65));
            }
            Bitmap bmp = new Bitmap(200, 80, PixelFormat.Format32bppArgb);
            Graphics grafico = Graphics.FromImage(bmp);
            grafico.SmoothingMode = SmoothingMode.Default;
            Rectangle rect = new Rectangle(0, 0, 200, 80);
            HatchBrush oHatchBrush = new HatchBrush(HatchStyle.Divot, Color.DimGray, Color.White);
            grafico.FillRectangle(oHatchBrush, rect);
            SizeF size;
            float fontSize = rect.Height + 5;
            Font font;

            do
            {
                fontSize--;
                font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                size = grafico.MeasureString(sb.ToString(), font);
            } while (size.Width > rect.Width);

            grafico.DrawString(sb.ToString(), font, Brushes.DarkGreen, 0, 5);

            Point punto1;
            Point punto2;
            //for (int i = 0; i < 5; i++)
            //{
            //    punto1 = new Point(oAzar.Next(200), oAzar.Next(80));
            //    punto2 = new Point(oAzar.Next(200), oAzar.Next(80));
            //    grafico.DrawLine(new Pen(Brushes.White, 5), punto1, punto2);
            //}
            codigo = sb.ToString();
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                imagenBase64 = Convert.ToBase64String(ms.ToArray());
            }
            font.Dispose();
            oHatchBrush.Dispose();
            grafico.Dispose();

            #endregion

            Session["genCCod"] = codigo;

            return Json(imagenBase64, JsonRequestBehavior.AllowGet);
        }

        public bool SiteSpecificAuthenticationMethod(string userName, string password, ref string sMensaje)
        {
            BEUsuario oBEUsuario = new BEUsuario();
            oBEUsuario.USUARIO = userName;
            oBEUsuario.CONTRASENA = password;
            oBEUsuario.id_aplicacion = 1;
            oBEUsuario.Autenticado = false;

            try
            {
                List<BEUsuario> LstUsuario = new List<BEUsuario>();
                List<BEPermiso> lstBEPermiso = new List<BEPermiso>();

                string strCadenaPedicion = string.Concat("usuario?ID_USUARIO=0",
                                                    "&&ID_ENTIDAD=0",
                                                    "&&ID_SEDE=0",
                                                    "&&DNI=", string.Empty,
                                                    "&&NOMBRE=", string.Empty,
                                                    "&&APELLIDO_PATERNO=", string.Empty,
                                                    "&&APELLIDO_MATERNO=", string.Empty,
                                                    "&&USUARIO=", Dame_Texto(oBEUsuario.USUARIO),
                                                    "&&ESTADO_VIGENCIA=0",
                                                    "&&TIPO_USUARIO=0",
                                                    "&&PAGINADO=1",
                                                    "&&TIPO=5");
                string strRespuesta = GetJSON(strCadenaPedicion);
                var ObjetoJSON = JsonConvert.DeserializeObject<ProcesoGenerico<List<BEUsuario>>>(strRespuesta);

                if (ObjetoJSON.Data.IsSuccess)
                {
                    LstUsuario = ObjetoJSON.Data.Result;
                    if (LstUsuario.Count > 0)
                    {
                        BEUsuario oBEUsuario_ = LstUsuario.FirstOrDefault();
                        oBEUsuario_.Autenticado = false;

                        bool bit_ESTADO_VIGENCIA = false, bit_FECHA_CADUCA = false;
                        lstBEPermiso.Add(new BEPermiso() { PERMISO_ID = 1, PERMISO = "Modificar", ROL = oBEUsuario_.TIPOUSUARIO });
                        lstBEPermiso.Add(new BEPermiso() { PERMISO_ID = 2, PERMISO = "Registrar", ROL = oBEUsuario_.TIPOUSUARIO });
                        lstBEPermiso.Add(new BEPermiso() { PERMISO_ID = 3, PERMISO = "Consultar", ROL = oBEUsuario_.TIPOUSUARIO });

                        if (oBEUsuario_.CONTRASENA == oBEUsuario.CONTRASENA)
                        {


                            if (oBEUsuario_.ESTADO_VIGENCIA == (int)ESTADO_VIGENCIA.ACTIVO)
                                bit_ESTADO_VIGENCIA = true;

                            if (((oBEUsuario_.FECHA_CADUCA - DateTime.Now).Days) > -1)
                                bit_FECHA_CADUCA = true;
                        }

                        if (bit_ESTADO_VIGENCIA && bit_FECHA_CADUCA)
                        {

                            oBEUsuario_.Autenticado = true;

                            strCadenaPedicion = string.Concat("usuario?TIPO_USUARIO=", oBEUsuario_.TIPO_USUARIO,
                                                               "&&ESTADO=1");
                            strRespuesta = GetJSON(strCadenaPedicion);

                            var ObjetoJSON_Recurso = JsonConvert.DeserializeObject<ProcesoGenerico<List<BERecurso>>>(strRespuesta);
                            if (ObjetoJSON_Recurso.Data.IsSuccess)
                                oBEUsuario_.Lista_Recurso = ObjetoJSON_Recurso.Data.Result;

                            oBEUsuario_.Lista_Permiso = lstBEPermiso;
                        }

                        oBEUsuario = oBEUsuario_;
                    }
                }


                //oBEUsuario = new BLUsuario().AutentificarUsuario(oBEUsuario);
                //#region "AutentificarUsuario"
                //List<BEPermiso> lstBEPermiso = new List<BEPermiso>();
                //lstBEPermiso.Add(new BEPermiso() { PERMISO_ID = 1, PERMISO = "Modifica", ROL = "Administrador" });
                //lstBEPermiso.Add(new BEPermiso() { PERMISO_ID = 2, PERMISO = "Registra", ROL = "Administrador" });
                //lstBEPermiso.Add(new BEPermiso() { PERMISO_ID = 3, PERMISO = "Consulta", ROL = "Administrador" });

                //List<BERecurso> lstBERecurso = new List<BERecurso>();
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 1, ID_RECURSO_PARENT = 0, ORDEN = 1, TITULO = "Configuración" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 2, ID_RECURSO_PARENT = 0, ORDEN = 2, TITULO = "Atención Ciudadano" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 3, ID_RECURSO_PARENT = 1, ORDEN = 1, TITULO = "Administrar Usuario", PAGINA = "AdminUsuario", URLMENU = "Index", AREA = "Configuracion" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 4, ID_RECURSO_PARENT = 1, ORDEN = 2, TITULO = "Administrar Sede", PAGINA = "AdminSede", URLMENU = "Index", AREA = "Configuracion" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 5, ID_RECURSO_PARENT = 1, ORDEN = 3, TITULO = "Administrar Entidad", PAGINA = "AdminEntidad", URLMENU = "Index", AREA = "Configuracion" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 6, ID_RECURSO_PARENT = 2, ORDEN = 1, TITULO = "Identificación Ciudadano", PAGINA = "AtenderCiudadano", URLMENU = "Index", AREA = "AtencionCiudadano" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 7, ID_RECURSO_PARENT = 1, ORDEN = 4, TITULO = "Administrar Servicio", PAGINA = "AdminServicio", URLMENU = "Index", AREA = "Configuracion" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 8, ID_RECURSO_PARENT = 0, ORDEN = 1, TITULO = "Generar Reportes" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 9, ID_RECURSO_PARENT = 8, ORDEN = 1, TITULO = "Reportes Atención de Mensual y Anual", PAGINA = "ReportesAtencion", URLMENU = "Index", AREA = "Reportes" });
                //lstBERecurso.Add(new BERecurso() { ID_RECURSO = 9, ID_RECURSO_PARENT = 8, ORDEN = 2, TITULO = "Reportes Atención por Entidad", PAGINA = "ReportesAtencion", URLMENU = "Entidad", AREA = "Reportes" });
                ////lstBERecurso.Add(new BERecurso() { RECURSO_ID = 10, RECURSO_PARENT_ID = 1, ORDEN = 5, TITULO = "Prueba combo multiple", PAGINA = "Prueba", UrlMenu = "Index", AREA = "Configuracion" });


                //#endregion

                //oBEUsuario = new BEUsuario()
                //{
                //    ID_USUARIO = 1,
                //    id_aplicacion = 1,
                //    USUARIO = "fgodoy",
                //    NOMBRE = "Franklin Godoy Amasifuen",
                //    CONTRASENA = "arica1212",
                //    Autenticado = true,

                //    Lista_Permiso = lstBEPermiso,
                //    Lista_Recurso = lstBERecurso
                //};

                Session["UserSession"] = oBEUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oBEUsuario.Autenticado;
        }

    }
}
