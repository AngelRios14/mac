using PCM.Servicio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity
{
    public class BEAtencion : FilterPaginate
    {
        public int ID_ATENCION { get; set; } = 0;
        public int ID_SERVICIO { get; set; }
        public int ID_CIUDADANO { get; set; }
        public int ID_ENTIDAD { get; set; }
        public int ID_USUARIO_REGISTRA { get; set; }
        public string DNI { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public int ES_FECHA_FIN { get; set; } = 0;
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }

        #region REPORTES
        public int ID_SEDE { get; set; }
        public int ID_USUARIO { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public string FECHA { get; set;}

        public string MUNICIPALIDAD { get; set; }
        public string SERVICIO { get; set; }
        public string SEDE { get; set; }
        public string HORA_INICIO { get; set; }
        public string HORA_FIN { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }

        public string FOTO { get; set; }
        #endregion
    }
}
