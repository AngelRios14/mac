using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class GeneralModels
    {
        [Display(Name = "Todos los derechos reservados (v.1.0)")]
        public string strVersionSistema { get; set; }

        [Display(Name = "© 2019 Mejora Atención al ciudadano express (v.1.0)")]
        public string strPieSistema { get; set; }

        [Display(Name = "Código Captcha")]
        public string strCapcha { get; set; }

        [Display(Name = "Usuario")]
        public string strUsuario { get; set; }

        [Display(Name = "Clave")]
        public string strClave { get; set; }

        public bool Autentificado { get; set; }

        public string nombreUsuario { get; set; }
        public string nombreSistema { get; set; }
        public string nombreRol { get; set; }
        public string nombreEntidad { get; set; }
        public string MensajeResult { get; set; }

        public int iRecordCount { get; set; }
    }
}
