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
    public class BLServicio : IBLServicio
    {
        private IDAServicio oIDAServicio;

        public BLServicio(IDAServicio oIDAServicioIn)
        {
            oIDAServicio = oIDAServicioIn;
        }

        public ProcessResult<List<BEServicio>> buscar(BEServicio oBEServicio)
        {

            var resultadoProceso = new ProcessResult<List<BEServicio>>();
            try
            {
                //   oServiceRepository = new ServiceRepository();
                resultadoProceso.Result = oIDAServicio.buscar(oBEServicio);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;

        }

        public ProcessResult<List<BEServicioAtencion>> ListarAtencion(BEServicio oBEServicio)
        {
            var resultadoProceso = new ProcessResult<List<BEServicioAtencion>>();
            try
            {
                resultadoProceso.Result = oIDAServicio.ListarAtencion(oBEServicio);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicioAtencion>(e.Message);
            }
            return resultadoProceso;
        }
        public ProcessResult<List<BEServicioAtencion>> ListarAtencionOrig(BEServicio oBEServicio)
        {
            var resultadoProceso = new ProcessResult<List<BEServicioAtencion>>();
            try
            {
                resultadoProceso.Result = oIDAServicio.ListarAtencionOrig(oBEServicio);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicioAtencion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEServicio>> Listar(BEServicio oBEServicio)
        {
            var resultadoProceso = new ProcessResult<List<BEServicio>>();
            try
            {
                resultadoProceso.Result = oIDAServicio.Listar(oBEServicio);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEServicio>> ListarPorId(BEServicio oBEServicio)
        {
            var resultadoProceso = new ProcessResult<List<BEServicio>>();
            try
            {
                resultadoProceso.Result = oIDAServicio.ListarPorId(oBEServicio);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEServicio> Registrar(BEServicio oBEServicio)
        {
            var resultadoProceso = new ProcessResult<BEServicio>();
            try
            {
                resultadoProceso.Result = oIDAServicio.Registrar(oBEServicio);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;
        }

    }
}
