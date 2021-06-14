using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace PCM.Servicio.API.Controllers
{
    public class SedeController : ApiController
    {
        public IBLSede oIBLSede { get; set; }
        public SedeController(IBLSede oIBLSedeIn)
        {
            oIBLSede = oIBLSedeIn;
        }

        // GET: api/Sede
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sede/5
        public JsonResult Get(int ID_SEDE, int ID_ENTIDAD, string NOMBRE, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();

            BESede oBESede = new BESede();
            oBESede.ID_SEDE = ID_SEDE;
            oBESede.ID_ENTIDAD = ID_ENTIDAD;
            oBESede.NOMBRE = NOMBRE;
            oBESede.PAGINADO = PAGINADO;
            oBESede.TIPO = TIPO;
            var response = oIBLSede.Listar(oBESede);
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int ID_SEDE)
        {
            var jResult = new JsonResult();

            BESede oBESede = new BESede();
            oBESede.ID_SEDE = ID_SEDE;
            var response = oIBLSede.ListarPorId(oBESede);
            jResult.Data = response;
            return jResult;
        }

        // POST: api/Sede
        public JsonResult Post([FromBody]BESede value)
        {
            var jResult = new JsonResult();
            var response = oIBLSede.Registrar(value);
            jResult.Data = response;
            return jResult;
        }


        // PUT: api/Sede/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sede/5
        public void Delete(int id)
        {
        }
    }
}
