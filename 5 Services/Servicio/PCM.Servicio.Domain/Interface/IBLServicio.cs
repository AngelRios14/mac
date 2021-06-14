using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLServicio
    {
        ProcessResult<List<BEServicio>> buscar(BEServicio oBEServicio);
        ProcessResult<List<BEServicioAtencion>> ListarAtencion(BEServicio oBEServicio);
        ProcessResult<List<BEServicioAtencion>> ListarAtencionOrig(BEServicio oBEServicio);
        ProcessResult<List<BEServicio>> Listar(BEServicio oBEServicio);
        ProcessResult<List<BEServicio>> ListarPorId(BEServicio oBEServicio);
        ProcessResult<BEServicio> Registrar(BEServicio oBEServicio); 
    }
}
