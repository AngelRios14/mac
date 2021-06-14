using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class UsuarioModels
    {
        public int ID_USUARIO { get; set; }

        [Display(Name = "Municipalidad")]
        public int? ID_ENTIDAD { get; set; }

        [Display(Name = "Sede")]
        public int? ID_SEDE { get; set; }

        [Display(Name = "Dni")]
        public string DNI { get; set; }

        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Display(Name = "Apellido Paterno")]
        public string APELLIDO_PATERNO { get; set; }

        [Display(Name = "Apellido Materno")]
        public string APELLIDO_MATERNO { get; set; }

        [Display(Name = "Usuario")]
        public string USUARIO { get; set; }

        [Display(Name = "Contraseña")]
        public string PASSWORD { get; set; }

        [Display(Name = "Fecha Caduca")]
        public DateTime FECHA_CADUCA { get; set; } = DateTime.Now;

        [Display(Name = "Estado")]
        public int ESTADO_VIGENCIA { get; set; }

        [Display(Name = "Tipo Usuario")]
        public int TIPO_USUARIO { get; set; }
        public int TIPO { get; set; } = 1;
        public int intNumeroPaginaUsuario { get; set; }
        public string NOMBRE_ENTIDAD { get; set; }
        public string RUC_ENTIDAD { get; set; }

        public int FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
    }
}
