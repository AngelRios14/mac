using PCM.Servicio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity
{
    public class BEEntidad : FilterPaginate
    {
        public int ID_ENTIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string RUC { get; set; }
        public string COD_UBIGEO { get; set; }
        public string COD_UBIGEO_DEPARTAMENTO { get; set; }
        public string COD_UBIGEO_PROVINCIA { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public int CONDICION { get; set; }
        public int ORDEN { get; set; }
        public string RUTA_LOGO { get; set; }
        public bool ES_MUNICIPALIDAD { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }

    }
}
