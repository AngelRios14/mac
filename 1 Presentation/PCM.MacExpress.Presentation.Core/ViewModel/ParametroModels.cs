using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class ParametroModels
    {
        [Display(Name = "Parametro")]
        public int ID_PARAMETRO { get; set; }

        public int ID_OPERACION { get; set; }

        [Display(Name = "Operacion")]
        public string OPERACION { get; set; }

        [Display(Name = "Parametro")]
        public string PARAMETRO { get; set; }

        [Display(Name = "Es Obligatorio")]
        public int ID_OBLIGATORIO { get; set; }

        [Display(Name = "Tipo Parametro")]
        public int ID_TIPO_PARAMETRO { get; set; }

        [Display(Name = "Comentario")]
        public string COMENTARIO { get; set; }

        public int intNumeroPaginaParametro { get; set; }
    }
}
