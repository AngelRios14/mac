using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class BEOperacion
    {
        public int ID_OPERACION { get; set; }
        public int ID_SERVICIO { get; set; }
        public string SERVICIO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ACCION { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public int FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
    }
}
