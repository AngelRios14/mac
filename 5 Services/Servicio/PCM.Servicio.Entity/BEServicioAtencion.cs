using PCM.Servicio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity
{
    public class BEServicioAtencion : FilterPaginate
    {
        public int ID_ENTIDAD { get; set; }
        public string ENTIDAD_NOMENCLATURA { get; set; }
        public string ENTIDAD_DESCRIPCION { get; set; }
        public string ENTIDAD_IMAGEN { get; set; }
        public int ID_MODOSERVICIO { get; set; }
        public string ANCHO_ALTO_LOGO { get; set; }
        public int ENTIDAD_ORDEN { get; set; }
        public int ID_SERVICIO { get; set; }
        public int SERVICIO_TIPO { get; set; }
        public string SERVICIO_DESCRIPCION { get; set; }
        public int SERVICIO_ORDEN { get; set; }
        public string SERVICIO_LINK { get; set; }
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }
        public string DNI_URL { get; set; }
    }
}
