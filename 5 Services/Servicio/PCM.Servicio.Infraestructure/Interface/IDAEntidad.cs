using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDAEntidad
    {
        List<BEEntidad> Listar(BEEntidad oBEEntidad);

        List<BEEntidad> ListarPorId(BEEntidad oBEEntidad);

        List<BEEntidad> BuscarUbigeo(BEEntidad oBEEntidad);

        BEEntidad Registrar(BEEntidad oBEEntidad);
    }
}
