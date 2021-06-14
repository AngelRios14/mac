using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class BESede
    {
        public int ID_SEDE { get; set; }
        public int ID_ENTIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }

        public string COD_UBIGEO_DEPARTAMENTO { get; set; }
        public string COD_UBIGEO_PROVINCIA { get; set; }
        public int CONDICION { get; set; }
        public string COD_UBIGEO { get; set; }

        public string NOMBRE_ENTIDAD { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public int FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
    }
}
