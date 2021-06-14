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
    public class BLSede: IBLSede
    {
        private IDASede oIDASede;

        public BLSede(IDASede oIDASedeIn)
        {
            oIDASede = oIDASedeIn;
        }

        public ProcessResult<List<BESede>> Listar(BESede oBESede)
        {
            var resultadoProceso = new ProcessResult<List<BESede>>();
            try
            {
                resultadoProceso.Result = oIDASede.Listar(oBESede);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BESede>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BESede>> ListarPorId(BESede oBESede)
        {
            var resultadoProceso = new ProcessResult<List<BESede>>();
            try
            {
                resultadoProceso.Result = oIDASede.ListarPorId(oBESede);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BESede>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BESede> Registrar(BESede oBESede)
        {
            var resultadoProceso = new ProcessResult<BESede>();
            try
            {
                resultadoProceso.Result = oIDASede.Registrar(oBESede);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BESede>(e.Message);
            }
            return resultadoProceso;
        }
    }
}
