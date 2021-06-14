using PCM.Servicio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity
{
    public class BEServicio : FilterPaginate
    {

        public int ID_SERVICIO { get; set; }
        public int ID_TIPOSERVICIO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_MODOSERVICIO { get; set; }
        public string NOMBRE { get; set; }
        public int ID_TIPOACCESO { get; set; }
        public string LINK { get; set; }
        public int ORDEN { get; set; }
        public int ESTADO_VIGENCIA { get; set; }
        public string ENTIDAD { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string MODO_SERVICIO { get; set; }
        public string TIPO_ACCESO { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }
        public string DNI_URL { get; set; }
    }
}
