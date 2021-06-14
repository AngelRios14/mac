using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class AtencionModels
    {
        [Display(Name = "DNI")]
        public string DNI { get; set; } = string.Empty;

        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; } = string.Empty;

        [Display(Name = "Apellido Paterno")]
        public string APELLIDO_PATERNO { get; set; } = string.Empty;

        [Display(Name = "Apellido Materno")]
        public string APELLIDO_MATERNO { get; set; } = string.Empty;

        [Display(Name = "Correo")]
        public string CORREO { get; set; } = string.Empty;

        [Display(Name = "Telefóno")]
        public string TELEFONO { get; set; } = string.Empty;

        [Display(Name = "Foto")]
        public string FOTO { get; set; } = string.Empty;

        public string SERVICIO { get; set; } = string.Empty;
        public string SERVICIO_COMBO { get; set; } = string.Empty;
        public int TOTAL { get; set; } = 0;
        public int PAGINADO { get; set; } = 0;
        public string ACCION { get; set; }
        public int intNumeroPaginaAtencion { get; set; }
        public string[] SEPARADOR { get; set; } = new string[3] {"│", "┼", "┬" };
        public string LINK { get; set; }

        public string DNI_CO { get; set; } = string.Empty;
        public string NOMBRE_CO { get; set; } = string.Empty;
    }
}
