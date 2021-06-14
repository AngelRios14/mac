using PCM.Servicio.Entity;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Interface
{
    public interface IBLEntidad
    {
        ProcessResult<List<BEEntidad>> Listar(BEEntidad oBEEntidad);

        ProcessResult<List<BEEntidad>> ListarPorId(BEEntidad oBEEntidad);

        ProcessResult<List<BEEntidad>> BuscarUbigeo(BEEntidad oBEEntidad);

        ProcessResult<BEEntidad> Registrar(BEEntidad oBEEntidad);
    }
}
