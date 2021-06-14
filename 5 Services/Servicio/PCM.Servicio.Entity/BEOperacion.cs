using PCM.Servicio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity
{
    public class BEOperacion : FilterPaginate
    {
        public int ID_OPERACION { get; set; }
        public int ID_SERVICIO { get; set; }        
        public string SERVICIO { get; set; }
        public string DESCRIPCION { get; set; }
        public string ACCION { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }
    }
}
