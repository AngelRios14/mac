using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class EntidadModels
    {
        [Display(Name = "Entidad")]
        public int ID_ENTIDAD { get; set; }

        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }

        [Display(Name = "Ruc")]
        public string RUC { get; set; }

        [Display(Name = "Departamento")]
        public string DEPARTAMENTO { get; set; }

        [Display(Name = "Provincia")]
        public string PROVINCIA { get; set; }

        [Display(Name = "Distrito")]
        public string DISTRITO { get; set; }

        [Display(Name = "Orden")]
        public int ORDEN { get; set; }

        [Display(Name = "Es Municipalidad")]
        public bool ES_MUNICIPALIDAD { get; set; }

        [Display(Name = "Adjuntar Logo")]
        public HttpPostedFileBase FullAdjunto { get; set; }

        public string FullAdjuntoNombre { get; set; }

        public int intNumeroPaginaEntidad { get; set; }

        public List<EntidadModels> GetEntidadList { get; set; }

        public int[] SelectedValues { get; set; }

    }
}
