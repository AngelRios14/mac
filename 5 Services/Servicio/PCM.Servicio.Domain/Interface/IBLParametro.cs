using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLParametro
    {
        ProcessResult<List<BEParametro>> Listar(BEParametro oBEParametro);

        ProcessResult<List<BEParametro>> ListarPorId(BEParametro oBEParametro);

        ProcessResult<BEParametro> Registrar(BEParametro oBEParametro);
    }
}
