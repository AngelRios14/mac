using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDAServicio
    {
        List<BEServicio> buscar(BEServicio oBEServicio);
        List<BEServicioAtencion> ListarAtencion(BEServicio oBEServicio);
        List<BEServicioAtencion> ListarAtencionOrig(BEServicio oBEServicio);
        List<BEServicio> Listar(BEServicio oBEServicio);
        List<BEServicio> ListarPorId(BEServicio oBEServicio);
        BEServicio Registrar(BEServicio oBEServicio);
    }
}
