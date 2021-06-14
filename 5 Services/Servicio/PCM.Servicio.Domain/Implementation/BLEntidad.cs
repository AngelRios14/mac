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
    public class BLEntidad: IBLEntidad
    {
        private IDAEntidad oIDAEntidad;

        public BLEntidad(IDAEntidad oIDAEntidadIn)
        {
            oIDAEntidad = oIDAEntidadIn;
        }

        public ProcessResult<List<BEEntidad>> Listar(BEEntidad oBEEntidad)
        {
            var resultadoProceso = new ProcessResult<List<BEEntidad>>();
            try
            {
                resultadoProceso.Result = oIDAEntidad.Listar(oBEEntidad);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEEntidad>> ListarPorId(BEEntidad oBEEntidad)
        {
            var resultadoProceso = new ProcessResult<List<BEEntidad>>();
            try
            {
                resultadoProceso.Result = oIDAEntidad.ListarPorId(oBEEntidad);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEEntidad>> BuscarUbigeo(BEEntidad oBEEntidad)
        {
            var resultadoProceso = new ProcessResult<List<BEEntidad>>();
            try
            {
                resultadoProceso.Result = oIDAEntidad.BuscarUbigeo(oBEEntidad);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEServicio>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEEntidad> Registrar(BEEntidad oBEEntidad) {
            var resultadoProceso = new ProcessResult<BEEntidad>();
            try
            {
                resultadoProceso.Result = oIDAEntidad.Registrar(oBEEntidad);

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
