using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Interface
{
    public interface IDAParametro
    {
        List<BEParametro> Listar(BEParametro oBEParametro);

        List<BEParametro> ListarPorId(BEParametro oBEParametro);

        BEParametro Registrar(BEParametro oBEParametro);
    }
}
