using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class BEAtencion
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
        public string FOTO { get; set; }

        public string ENTIDAD_NOMENCLATURA { get; set;}
        public string ENTIDAD_DESCRIPCION { get; set; }
        public string ENTIDAD_IMAGEN { get; set; }
        public int ENTIDAD_ORDEN { get; set; }
        public int SERVICIO_TIPO { get; set;}
        public string SERVICIO_DESCRIPCION { get;set;}
        public int SERVICIO_ORDEN { get; set;}
        public string SERVICIO_LINK { get; set; }

        
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }

        #region REPORTES
        public int ID_SEDE { get; set; }
        public int ID_USUARIO { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
        public string FECHA { get; set; }

        public string MUNICIPALIDAD { get; set; }
        public string SERVICIO { get; set; }
        public string SEDE { get; set; }
        public string HORA_INICIO { get; set; }
        public string HORA_FIN { get; set; }

        public string DEPARTAMENTO { get; set;}
        public string PROVINCIA { get; set;}
        public string DISTRITO { get; set; }
        #endregion
    }
}
