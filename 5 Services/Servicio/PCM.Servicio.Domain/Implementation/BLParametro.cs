using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Entity;
using PCM.Servicio.Infraestructure.Interface;
using Service.Trasnversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Domain.Implementation
{
    public class BLParametro : IBLParametro
    {
        private IDAParametro oIDAParametro;

        public BLParametro(IDAParametro oIDAParametroIn)
        {
            oIDAParametro = oIDAParametroIn;
        }

        public ProcessResult<List<BEParametro>> Listar(BEParametro oBEParametro)
        {
            var resultadoProceso = new ProcessResult<List<BEParametro>>();
            try
            {
                resultadoProceso.Result = oIDAParametro.Listar(oBEParametro);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEParametro>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEParametro>> ListarPorId(BEParametro oBEParametro)
        {
            var resultadoProceso = new ProcessResult<List<BEParametro>>();
            try
            {
                resultadoProceso.Result = oIDAParametro.ListarPorId(oBEParametro);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEParametro>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEParametro> Registrar(BEParametro oBEParametro)
        {
            var resultadoProceso = new ProcessResult<BEParametro>();
            try
            {
                resultadoProceso.Result = oIDAParametro.Registrar(oBEParametro);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEParametro>(e.Message);
            }
            return resultadoProceso;
        }
    }
}
