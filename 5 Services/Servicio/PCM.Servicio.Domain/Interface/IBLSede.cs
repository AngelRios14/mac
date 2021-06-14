using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLSede
    {
        ProcessResult<List<BESede>> Listar(BESede oBESede);

        ProcessResult<List<BESede>> ListarPorId(BESede oBESede);

        ProcessResult<BESede> Registrar(BESede oBESede);
    }
}
