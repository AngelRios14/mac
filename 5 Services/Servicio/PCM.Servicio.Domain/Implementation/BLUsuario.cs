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
    public class BLUsuario: IBLUsuario
    {
        private IDAUsuario oIDAUsuario;

        public BLUsuario(IDAUsuario oIDAUsuarioIn)
        {
            oIDAUsuario = oIDAUsuarioIn;
        }

        public ProcessResult<List<BEUsuario>> Listar(BEUsuario oBEUsuario)
        {
            var resultadoProceso = new ProcessResult<List<BEUsuario>>();
            try
            {
                resultadoProceso.Result = oIDAUsuario.Listar(oBEUsuario);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEUsuario>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BERecurso>> ListarRecurso(BEUsuario oBEUsuario)
        {
            var resultadoProceso = new ProcessResult<List<BERecurso>>();
            try
            {
                resultadoProceso.Result = oIDAUsuario.ListarRecurso(oBEUsuario);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BERecurso>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<List<BEUsuario>> ListarPorId(BEUsuario oBEUsuario)
        {
            var resultadoProceso = new ProcessResult<List<BEUsuario>>();
            try
            {
                resultadoProceso.Result = oIDAUsuario.ListarPorId(oBEUsuario);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEUsuario>(e.Message);
            }
            return resultadoProceso;
        }

        public ProcessResult<BEUsuario> Registrar(BEUsuario oBEUsuario)
        {
            var resultadoProceso = new ProcessResult<BEUsuario>();
            try
            {
                resultadoProceso.Result = oIDAUsuario.Registrar(oBEUsuario);

            }
            catch (System.Exception e)
            {
                resultadoProceso.IsSuccess = false;
                resultadoProceso.Exception = new ApplicationLayerException<BEUsuario>(e.Message);
            }
            return resultadoProceso;
        }
    }
}
