using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Http;

namespace PCM.Servicio.API.Controllers
{
    public class ParametroController : ApiController
    {
        public IBLParametro oIBLParametro { get; set; }
        public ParametroController(IBLParametro oIBLParametroIn)
        {
            oIBLParametro = oIBLParametroIn;
        }

        // GET: api/Parametro
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Parametro/5
        public JsonResult Get(int ID_PARAMETRO, int ID_OPERACION, string PARAMETRO, int ID_TIPO_PARAMETRO, string COMENTARIO, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();

            BEParametro oBEParametro = new BEParametro();
            oBEParametro.ID_PARAMETRO = ID_PARAMETRO;
            oBEParametro.ID_OPERACION = ID_OPERACION;
            oBEParametro.PARAMETRO = PARAMETRO;
            oBEParametro.ID_OBLIGATORIO = 0;
            oBEParametro.ID_TIPO_PARAMETRO = ID_TIPO_PARAMETRO;
            oBEParametro.COMENTARIO = COMENTARIO;
            oBEParametro.PAGINADO = PAGINADO;
            oBEParametro.TIPO = TIPO;
            var response = oIBLParametro.Listar(oBEParametro);
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int ID_PARAMETRO)
        {
            var jResult = new JsonResult();

            BEParametro oBEParametro = new BEParametro();
            oBEParametro.ID_PARAMETRO = ID_PARAMETRO;
            var response = oIBLParametro.ListarPorId(oBEParametro);
            jResult.Data = response;
            return jResult;
        }
        // POST: api/Parametro
        public JsonResult Post([FromBody]BEParametro value)
        {
            var jResult = new JsonResult();
            var response = oIBLParametro.Registrar(value);
            jResult.Data = response;
            return jResult;
        }


        // PUT: api/Parametro/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Parametro/5
        public void Delete(int id)
        {
        }
    }
}
