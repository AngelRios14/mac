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
    public class BLAtencion : IBLAtencion
    {
        private IDAAtencion oIDAAtencion;

        public BLAtencion(IDAAtencion oIDAAtencionIn)
        {
            oIDAAtencion = oIDAAtencionIn;
        }
        public ProcessResult<List<BEAtencion>> ListarAtencion(BEAtencion oBEAtencion)
        {
            var resultadoProceso = new ProcessResult<List<BEAtencion>>();
            try
            {
                resultadoProceso.Result = oIDAAtencion.ListarAtencion(oBEAtencion);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEAtencion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEAtencion>> ListarCiudadano(BEAtencion oBEAtencion)
        {
            var resultadoProceso = new ProcessResult<List<BEAtencion>>();
            try
            {
                resultadoProceso.Result = oIDAAtencion.ListarCiudadano(oBEAtencion);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEAtencion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEAtencion> RegistrarAtencion(BEAtencion oBEAtencion)
        {
            var resultadoProceso = new ProcessResult<BEAtencion>();
            try
            {
                resultadoProceso.Result = oIDAAtencion.RegistrarAtencion(oBEAtencion);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEAtencion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEAtencion> RegistrarCiudadano(BEAtencion oBEAtencion)
        {
            var resultadoProceso = new ProcessResult<BEAtencion>();
            try
            {
                resultadoProceso.Result = oIDAAtencion.RegistrarCiudadano(oBEAtencion);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEAtencion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEAtencion>> ListarReporteAtencionSede(BEAtencion oBEAtencion)
        {
            var resultadoProceso = new ProcessResult<List<BEAtencion>>();
            try
            {
                resultadoProceso.Result = oIDAAtencion.ListarReporteAtencionSede(oBEAtencion);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEAtencion>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEAtencion>> ListarReporteAtencionDepartamento(BEAtencion oBEAtencion)
        {
            var resultadoProceso = new ProcessResult<List<BEAtencion>>();
            try
            {
                resultadoProceso.Result = oIDAAtencion.ListarReporteAtencionDepartamento(oBEAtencion);
            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEAtencion>(e.Message);
            }
            return resultadoProceso;
        }

    }
}
