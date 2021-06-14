using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class modelReporte
    {
        [Display(Name = "Municipalidad")]
        public int ID_ENTIDAD { get; set; }

        [Display(Name = "Entidad")]
        public int ID_ENTIDAD_E { get; set; }

        [Display(Name = "Sede")]
        public int ID_SEDE { get; set; }

        [Display(Name = "Asesor")]
        public int ID_USUARIO { get; set; }

        [Display(Name = "DNI del Ciudadano")]
        public string DNI { get; set; }

        [Display(Name = "Servicio")]
        public string ID_SERVICIO { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime FECHA_INICIO { get; set; } = DateTime.Now;

        [Display(Name = "Fecha Fin")]
        public DateTime FECHA_FIN { get; set; } = DateTime.Now;

        [Display(Name = "Departamento")]
        public string DEPARTAMENTO { get; set; }

        [Display(Name = "Provincia")]
        public string PROVINCIA { get; set; }

        [Display(Name = "Distrito")]
        public string DISTRITO { get; set; }

    }
}
