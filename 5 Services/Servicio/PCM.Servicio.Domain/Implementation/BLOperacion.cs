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
    public class BLOperacion : IBLOperacion
    {
        private IDAOperacion oIDAOperacion;

        public BLOperacion(IDAOperacion oIDAOperacionIn)
        {
            oIDAOperacion = oIDAOperacionIn;
        }

        public ProcessResult<List<BEOperacion>> Listar(BEOperacion oBEOperacion)
        {
            var resultadoProceso = new ProcessResult<List<BEOperacion>>();
            try
            {
                resultadoProceso.Result = oIDAOperacion.Listar(oBEOperacion);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEOperacion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEOperacion>> ListarPorId(BEOperacion oBEOperacion)
        {
            var resultadoProceso = new ProcessResult<List<BEOperacion>>();
            try
            {
                resultadoProceso.Result = oIDAOperacion.ListarPorId(oBEOperacion);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEOperacion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEOperacion> Registrar(BEOperacion oBEOperacion)
        {
            var resultadoProceso = new ProcessResult<BEOperacion>();
            try
            {
                resultadoProceso.Result = oIDAOperacion.Registrar(oBEOperacion);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEOperacion>(e.Message);
            }
            return resultadoProceso;
        }    
    }
}
