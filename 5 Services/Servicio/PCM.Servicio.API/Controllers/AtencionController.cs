using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;


namespace PCM.Servicio.API.Controllers
{
    public class AtencionController : ApiController
    {
        public IBLAtencion oIBLAtencion { get; set; }
        public AtencionController(IBLAtencion oIBLAtencionIn)
        {
            oIBLAtencion = oIBLAtencionIn;

        }
        // GET: api/Atencion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Atencion/5
        public JsonResult Get(int ID_ATENCION, int ID_SERVICIO, int ID_CIUDADANO, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();
            var response = oIBLAtencion.ListarAtencion(new BEAtencion() { ID_ATENCION = ID_ATENCION, ID_SERVICIO = ID_SERVICIO, ID_CIUDADANO = ID_CIUDADANO, PAGINADO = PAGINADO, TIPO = TIPO });
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int ID_CIUDADANO, string DNI, string NOMBRE, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();
            var response = oIBLAtencion.ListarCiudadano(new BEAtencion() { ID_CIUDADANO = ID_CIUDADANO, DNI = DNI, NOMBRE = NOMBRE, PAGINADO = PAGINADO, TIPO = TIPO });
            jResult.Data = response;
            return jResult;
        }
        public JsonResult Get(int ID_ENTIDAD, int ID_SEDE, int ID_USUARIO, string DNI, int ID_SERVICIO, string FECHA_INICIO, string FECHA_FIN, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();
            var response = oIBLAtencion.ListarReporteAtencionSede(new BEAtencion()
            {
                ID_ENTIDAD = ID_ENTIDAD,
                ID_SEDE = ID_SEDE,
                ID_USUARIO = ID_USUARIO,
                DNI = DNI,
                ID_SERVICIO = ID_SERVICIO,
                FECHA_INICIO = Convert.ToDateTime(FECHA_INICIO),
                FECHA_FIN = Convert.ToDateTime(FECHA_FIN),
                PAGINADO = PAGINADO,
                TIPO = TIPO
            });
            jResult.Data = response;
            return jResult;
        }
        public JsonResult Get(string DEPARTAMENTO, string PROVINCIA,string DISTRITO, int ID_ENTIDAD, int ID_SEDE, int ID_USUARIO, string DNI, int ID_SERVICIO, string FECHA_INICIO, string FECHA_FIN, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();
            var response = oIBLAtencion.ListarReporteAtencionDepartamento(new BEAtencion()
            {
                DEPARTAMENTO = DEPARTAMENTO,
                PROVINCIA = PROVINCIA,
                DISTRITO = DISTRITO,
                ID_ENTIDAD = ID_ENTIDAD,
                ID_SEDE = ID_SEDE,
                ID_USUARIO = ID_USUARIO,
                DNI = DNI,
                ID_SERVICIO = ID_SERVICIO,
                FECHA_INICIO = Convert.ToDateTime(FECHA_INICIO),
                FECHA_FIN = Convert.ToDateTime(FECHA_FIN),
                PAGINADO = PAGINADO,
                TIPO = TIPO
            });
            jResult.Data = response;
            return jResult;
        }


        // POST: api/Atencion
        public JsonResult Post([FromBody]BEAtencion value)
        {
            var jResult = new JsonResult();
            if (value.TIPO == 1)
                jResult.Data = oIBLAtencion.RegistrarCiudadano(value); 
            else
                jResult.Data = oIBLAtencion.RegistrarAtencion(value);
            
            return jResult;
        }

        // PUT: api/Atencion/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Atencion/5
        public void Delete(int id)
        {
        }
    }
}
