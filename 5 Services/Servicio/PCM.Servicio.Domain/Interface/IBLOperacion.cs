using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLOperacion
    {
        ProcessResult<List<BEOperacion>> Listar(BEOperacion oBEOperacion);

        ProcessResult<List<BEOperacion>> ListarPorId(BEOperacion oBEOperacion);

        ProcessResult<BEOperacion> Registrar(BEOperacion oBEOperacion);
    }
}
