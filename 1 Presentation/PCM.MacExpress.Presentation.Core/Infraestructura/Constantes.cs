using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.MacExpress.Presentation.Core.Infraestructura
{
    enum ESTADO_VIGENCIA
    {
        ACTIVO = 1,
        DESACTIVO = 2
    }

    enum PAGINA_DEFECTO
    {
        ESTA_POR_DEFECTO = 1,
        NO_ESTA_POR_DEFECTO = 0
    }

    enum TIPO_USUARIO {
        ASESOR = 1,
        SUPERVISOR_MUNICIPAL = 2,
        ADMINISTRADOR_SAC = 3
    }
}
