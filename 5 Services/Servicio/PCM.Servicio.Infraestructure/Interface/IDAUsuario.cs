using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDAUsuario
    {
        List<BEUsuario> Listar(BEUsuario oBEUsuario);

        List<BERecurso> ListarRecurso(BEUsuario oBEUsuario);

        List<BEUsuario> ListarPorId(BEUsuario oBEUsuario);

        BEUsuario Registrar(BEUsuario oBESede);
    }
}
