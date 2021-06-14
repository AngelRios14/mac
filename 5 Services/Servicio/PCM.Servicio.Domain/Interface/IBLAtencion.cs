using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLAtencion
    {
        ProcessResult<List<BEAtencion>> ListarAtencion(BEAtencion oBEAtencion);

        ProcessResult<List<BEAtencion>> ListarCiudadano(BEAtencion oBEAtencion);

        ProcessResult<BEAtencion> RegistrarAtencion(BEAtencion oBEAtencion);

        ProcessResult<BEAtencion> RegistrarCiudadano(BEAtencion oBEAtencion);

        ProcessResult<List<BEAtencion>> ListarReporteAtencionSede(BEAtencion oBEAtencion);
        ProcessResult<List<BEAtencion>> ListarReporteAtencionDepartamento(BEAtencion oBEAtencion);
    }
}
