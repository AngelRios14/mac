using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDAOperacion
    {
        List<BEOperacion> Listar(BEOperacion oBEOperacion);

        List<BEOperacion> ListarPorId(BEOperacion oBEOperacion);

        BEOperacion Registrar(BEOperacion oBESede);
    }
}
