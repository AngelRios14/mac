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
using System.Web.Http.Cors;

namespace PCM.Servicio.API.Controllers
{
    [EnableCors(origins: "http://192.168.146.205", headers: "*", methods: "*")]
    public class ServicioController : ApiController
    {
        public IBLServicio oIBLServicio { get; set; }
        public ServicioController(IBLServicio oIBLServicioIn)
        {
            oIBLServicio = oIBLServicioIn;

        }
        // GET: api/Sede
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Servicio/5
        public JsonResult Get(int ID_SERVICIO, int ID_TIPOSERVICIO, int ID_ENTIDAD, int ID_MODOSERVICIO, string NOMBRE, int ID_TIPOACCESO, string LINK, int ORDEN, int ESTADO_VIGENCIA, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();

            BEServicio oBEServicio = new BEServicio();
            oBEServicio.ID_SERVICIO = ID_SERVICIO;
            oBEServicio.ID_TIPOSERVICIO = ID_TIPOSERVICIO;
            oBEServicio.ID_ENTIDAD = ID_ENTIDAD;
            oBEServicio.ID_MODOSERVICIO = ID_MODOSERVICIO;
            oBEServicio.NOMBRE = NOMBRE;
            oBEServicio.ID_TIPOACCESO = ID_TIPOACCESO;
            oBEServicio.LINK = LINK;
            oBEServicio.ORDEN = ORDEN;
            oBEServicio.ESTADO_VIGENCIA = ESTADO_VIGENCIA;
            oBEServicio.PAGINADO = PAGINADO;
            oBEServicio.TIPO = TIPO;
            var response = oIBLServicio.Listar(oBEServicio);
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int ID_SERVICIO)
        {
            var jResult = new JsonResult();

            BEServicio oBEServicio = new BEServicio();
            oBEServicio.ID_SERVICIO = ID_SERVICIO;
            var response = oIBLServicio.ListarPorId(oBEServicio);
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(string NOMBRE, int PAGINADO, int TIPO, int ID_ENTIDAD, string TIPO_DATA)
        {
            var jResult = new JsonResult();
             
            if (TIPO_DATA == "COMBO")
            {
                jResult.Data = oIBLServicio.ListarAtencionOrig(new BEServicio() { NOMBRE = NOMBRE, PAGINADO = PAGINADO, TIPO = TIPO, ID_ENTIDAD = ID_ENTIDAD });
            }
            else
            {
                jResult.Data = oIBLServicio.ListarAtencion(new BEServicio() { NOMBRE = NOMBRE, PAGINADO = PAGINADO, TIPO = TIPO, ID_ENTIDAD = ID_ENTIDAD });
            }
            return jResult;
        }

        // POST: api/Servicio
        public JsonResult Post([FromBody]BEServicio value)
        {
            var jResult = new JsonResult();
            var response = oIBLServicio.Registrar(value);
            jResult.Data = response;
            return jResult;
        }

        // PUT: api/Servicio/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Servicio/5
        public void Delete(int id)
        {
        }
    }
}
