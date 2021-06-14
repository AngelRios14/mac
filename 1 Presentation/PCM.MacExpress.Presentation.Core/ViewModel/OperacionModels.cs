using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class OperacionModels
    {
        [Display(Name = "Operacion")]
        public int ID_OPERACION { get; set; }

        public int ID_SERVICIO { get; set; }

        [Display(Name = "Servicio")]
        public string SERVICIO { get; set; }

        [Display(Name = "Descripcion")]
        public string DESCRIPCION { get; set; }

        [Display(Name = "Accion")]
        public string ACCION { get; set; }

        public int intNumeroPaginaOperacion { get; set; }
    }
}
