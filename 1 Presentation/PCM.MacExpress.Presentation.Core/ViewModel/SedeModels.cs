using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class SedeModels
    {
        public int ID_SEDE { get; set; }

        [Display(Name = "Municipalidad")]
        public int ID_ENTIDAD { get; set; }

        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }


        public int intNumeroPaginaSede { get; set; }
    }
}
