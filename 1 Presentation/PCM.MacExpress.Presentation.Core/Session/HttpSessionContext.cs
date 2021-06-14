using PCM.MacExpress.Presentation.Core.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.Mvc;

namespace PCM.MacExpress.Presentation.Core.Session
{
    public sealed class HttpSessionContext
    {
        public static string AssemblyAplicationName = System.Configuration.ConfigurationManager.AppSettings["AssemblyAplicationName"];
        /// <summary>
        /// Obtiene la sesión actual.
        /// </summary>
        /// <returns></returns>
        public static CuentaUsuario CurrentAccount()
        {
            var result = HttpContext.Current.Session != null ? (CuentaUsuario)HttpContext.Current.Session[AssemblyAplicationName] : null;

            return result;
        }
        /// <summary>
        /// Obtiene la sesión actual.
        /// </summary>
        /// <returns></returns>
        public static void SetAccount(CuentaUsuario account)
        {
            HttpContext.Current.Session.Add(AssemblyAplicationName, account);
        }

        public static void RemoveAccount()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }

        public static RedirectResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            string LogoutUrl = string.Format("{0}", System.Web.Security.FormsAuthentication.LoginUrl);
            return new RedirectResult(LogoutUrl);
        }
        /// <summary>
        /// Guarda los Datos de Sesion de la Aplicación
        /// </summary>
        /// <param name="datosSesion">Datos de Sesion</param>
        public static void GuardarDatosSesion(CuentaUsuario datosSesion)
        {
            HttpContext.Current.Session[AssemblyAplicationName] = datosSesion;
        }
        /// <summary>
        /// Obtiene los datos de la sesion
        /// </summary>
        /// <returns></returns>
        public static CuentaUsuario ObtenerDatosSesion()
        {
            var result = HttpContext.Current.Session != null ? (CuentaUsuario)HttpContext.Current.Session[AssemblyAplicationName] : null;
            return result;
        }
        /// <summary>
        /// Permite validar el acceso a URL
        /// </summary>
        /// <returns>Indicador de acceso</returns>
        public static bool ValidarAccesoUrl()
        {
            var pyfSession = CurrentAccount();
            object area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];
            string areaName = area != null ? area.ToString() : "";
            string controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"].ToString();
            string aciontName = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString();
            string url = areaName + "/" + controllerName;
            //   var modulo = pyfSession.Permisos.Where(x => x.Ruta.ToUpper().Contains(url.ToUpper())).FirstOrDefault(); 
            //   bool resultado = (modulo != null);
            return true;//CAMBIADO
        }
    }
}
