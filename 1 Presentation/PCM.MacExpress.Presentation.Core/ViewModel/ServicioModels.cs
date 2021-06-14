using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class ServicioModels
    {
        public int ID_SERVICIO { get; set; }

        [Display(Name = "Tipo Servicio")]
        public int TIPOSERVICIO { get; set; }

        [Display(Name = "Entidad")]
        public int ID_ENTIDAD { get; set; }

        [Display(Name = "Modo Servicio")]
        public int MODOSERVICIO { get; set; }

        [Display(Name = "Estado")]
        public int ESTADO_VIGENCIA { get; set; }

        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Display(Name = "Tipo Acceso")]
        public int TIPOACCESO { get; set; }

        [Display(Name = "Link")]
        public string LINK { get; set; }

        [Display(Name = "Orden")]
        public int ORDEN { get; set; }
        public int intNumeroPaginaServicio { get; set; }
        public int[] SelectedValues { get; set; }
        public string DNI_URL { get; set; }
    }
}
