using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.Controllers.Base
{
    [Serializable]
    public class CuentaUsuario
    {

        public string CodigoUsuario { get; set; }
        public string CodigoEmpleado { get; set; }
        public string CodigoPerfil { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CorreoElectronico { get; set; }
        public string Cuenta { get; set; }
        public string Contrasena { get; set; }
        public string CodigoEstado { get; set; }
        public string Dependencia { get; set; }
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1} {2}", this.Nombre, this.ApellidoPaterno, this.ApellidoMaterno);
            }
        }
        public byte[] Imagen { get; set; }

    }
}
