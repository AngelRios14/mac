using PCM.Servicio.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Entity
{
    public class BEUsuario : FilterPaginate
    {
        public int ID_USUARIO { get; set; }
        public int? ID_ENTIDAD { get; set; }
        public int? ID_SEDE { get; set; }
        public string DNI { get; set; }
        public string NOMBRE { get; set; }        
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRE_ENTIDAD { get; set; }
        public string NOMBRE_SEDE { get; set; }
        public string USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public string CONTRASENA { get; set; }
        public DateTime FECHA_CADUCA { get; set; }
        public int ESTADO_VIGENCIA { get; set; }
        public int TIPO_USUARIO { get; set; }
        public string TIPOUSUARIO { get; set; }
        public string USUARIO_REGISTRA { get; set; }
        public Int64 FILA { get; set; }
        public int TOTAL { get; set; }
        public int PAGINADO { get; set; }
        public int TIPO { get; set; }
    }
}
