using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDASede
    {
        List<BESede> Listar(BESede oBESede);

        List<BESede> ListarPorId(BESede oBESede);

        BESede Registrar(BESede oBESede);
    }
}
