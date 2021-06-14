using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class BEParametro
    {
        public int ID_PARAMETRO { get; set; }
        public int ID_OPERACION { get; set; }
        public string PARAMETRO { get; set; }
        public int ID_OBLIGATORIO { get; set; }
        public int ID_TIPO_PARAMETRO { get; set; }
        public string COMENTARIO { get; set; }
        public string OPERACION { get; set; }
        public string OBLIGATORIO { get; set; }
        public string TIPO_PARAMETRO { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public int FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
    }
}
