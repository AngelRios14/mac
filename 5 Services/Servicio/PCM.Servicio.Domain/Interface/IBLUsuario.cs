using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLUsuario
    {
        ProcessResult<List<BEUsuario>> Listar(BEUsuario oBEUsuario);

        ProcessResult<List<BERecurso>> ListarRecurso(BEUsuario oBEUsuario);

        ProcessResult<List<BEUsuario>> ListarPorId(BEUsuario oBEUsuario);

        ProcessResult<BEUsuario> Registrar(BEUsuario oBEUsuario);
    }
}
