using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDAAtencion
    {
        List<BEAtencion> ListarAtencion(BEAtencion oBEAtencion);

        List<BEAtencion> ListarCiudadano(BEAtencion oBEAtencion);

        BEAtencion RegistrarAtencion(BEAtencion oBEAtencion);

        BEAtencion RegistrarCiudadano(BEAtencion oBEAtencion);

        List<BEAtencion> ListarReporteAtencionSede(BEAtencion oBEAtencion);

        List<BEAtencion> ListarReporteAtencionDepartamento(BEAtencion oBEAtencion);
    }
}
