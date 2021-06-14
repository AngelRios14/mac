using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace PCM.Servicio.API.Controllers
{
    public class EntidadController : ApiController
    {
        public IBLEntidad oIBLEntidad { get; set; }
        public EntidadController(IBLEntidad oIBLEntidadIn)
        {
            oIBLEntidad = oIBLEntidadIn;

        }
        // GET: api/Entidad
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Entidad/5
        public JsonResult Get(int CONDICION, string COD_UBIGEO_DEPARTAMENTO, string COD_UBIGEO_PROVINCIA)
        {
            var jResult = new JsonResult();

            BEEntidad oBEEntidad = new BEEntidad();
            oBEEntidad.CONDICION = CONDICION;
            oBEEntidad.COD_UBIGEO_DEPARTAMENTO = COD_UBIGEO_DEPARTAMENTO;
            oBEEntidad.COD_UBIGEO_PROVINCIA = COD_UBIGEO_PROVINCIA;
            var response = oIBLEntidad.BuscarUbigeo(oBEEntidad);
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int ID_ENTIDAD, string NOMBRE, string RUC, string COD_UBIGEO, int ORDEN, bool ES_MUNICIPALIDAD, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();

            BEEntidad oBEEntidad = new BEEntidad();
            oBEEntidad.ID_ENTIDAD = ID_ENTIDAD;
            oBEEntidad.NOMBRE = NOMBRE;
            oBEEntidad.RUC = RUC;
            oBEEntidad.COD_UBIGEO = COD_UBIGEO;
            oBEEntidad.ORDEN = ORDEN;
            oBEEntidad.ES_MUNICIPALIDAD = ES_MUNICIPALIDAD;
            oBEEntidad.PAGINADO = PAGINADO;
            oBEEntidad.TIPO = TIPO;
            var response = oIBLEntidad.Listar(oBEEntidad);
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int ID_ENTIDAD)
        {
            var jResult = new JsonResult();

            BEEntidad oBEEntidad = new BEEntidad();
            oBEEntidad.ID_ENTIDAD = ID_ENTIDAD;
            var response = oIBLEntidad.ListarPorId(oBEEntidad);
            jResult.Data = response;
            return jResult;
        }

        // POST: api/Entidad
        public JsonResult Post([FromBody]BEEntidad value)
        {
            var jResult = new JsonResult();
            var response = oIBLEntidad.Registrar(value);
            jResult.Data = response;
            return jResult;
        }

        // PUT: api/Entidad/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Entidad/5
        public void Delete(int id)
        {
        }
    }
}
