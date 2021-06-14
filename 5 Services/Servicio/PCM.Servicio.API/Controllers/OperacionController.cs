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
    public class OperacionController : ApiController
    {
        public IBLOperacion oIBLOperacion { get; set; }
        public OperacionController(IBLOperacion oIBLOperacionIn)
        {
            oIBLOperacion = oIBLOperacionIn;
        }
        // GET: api/Operacion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public JsonResult Get(int ID_OPERACION, int ID_SERVICIO, string DESCRIPCION, string ACCION, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();

            BEOperacion oBEOperacion = new BEOperacion();
            oBEOperacion.ID_OPERACION = ID_OPERACION;
            oBEOperacion.ID_SERVICIO = ID_SERVICIO;
            oBEOperacion.DESCRIPCION = DESCRIPCION;
            oBEOperacion.ACCION = ACCION;
            oBEOperacion.PAGINADO = PAGINADO;
            oBEOperacion.TIPO = TIPO;
            var response = oIBLOperacion.Listar(oBEOperacion);
            jResult.Data = response;
            return jResult;
        }

        // GET: api/Operacion/5
        public JsonResult Get(int ID_OPERACION)
        {
            var jResult = new JsonResult();

            BEOperacion oBEOperacion = new BEOperacion();
            oBEOperacion.ID_OPERACION = ID_OPERACION;
            var response = oIBLOperacion.ListarPorId(oBEOperacion);
            jResult.Data = response;
            return jResult;
        }

        // POST: api/Operacion
        public JsonResult Post([FromBody]BEOperacion value)
        {
            var jResult = new JsonResult();
            var response = oIBLOperacion.Registrar(value);
            jResult.Data = response;
            return jResult;
        }

        // PUT: api/Operacion/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Operacion/5
        public void Delete(int id)
        {
        }
    }
}
