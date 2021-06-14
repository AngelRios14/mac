using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.ViewModel
{
    public class BECiudadano
    {
        public int ID_CIUDADANO { get; set; }
        public string DNI { get; set; } = string.Empty;
        public string NOMBRE { get; set; } = string.Empty;
        public string APELLIDO_PATERNO { get; set; } = string.Empty;
        public string APELLIDO_MATERNO { get; set; } = string.Empty;
        public string FOTO { get; set; } = string.Empty;
    }
}
