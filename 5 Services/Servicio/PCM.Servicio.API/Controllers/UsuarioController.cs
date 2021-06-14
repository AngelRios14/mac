using PCM.Servicio.Domain.Interface;
using PCM.Servicio.Entity;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace PCM.Servicio.API.Controllers
{
    public class UsuarioController : ApiController
    {
        public IBLUsuario oIBLUsuario { get; set; }

        public UsuarioController(IBLUsuario oIBLUsuarioIn)
        {
            oIBLUsuario = oIBLUsuarioIn;
        }

        // GET: api/Usuario
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public JsonResult Get(int ID_USUARIO, int ID_ENTIDAD, int ID_SEDE, string DNI, string NOMBRE, string APELLIDO_PATERNO, string APELLIDO_MATERNO, string USUARIO, int ESTADO_VIGENCIA, int TIPO_USUARIO, int PAGINADO, int TIPO)
        {
            var jResult = new JsonResult();

            BEUsuario oBEUsuario = new BEUsuario();
            oBEUsuario.ID_USUARIO = ID_USUARIO;
            oBEUsuario.ID_ENTIDAD = ID_ENTIDAD;
            oBEUsuario.ID_SEDE = ID_SEDE;
            oBEUsuario.DNI = DNI;
            oBEUsuario.NOMBRE = NOMBRE;
            oBEUsuario.APELLIDO_PATERNO = APELLIDO_PATERNO;
            oBEUsuario.APELLIDO_MATERNO = APELLIDO_MATERNO;
            oBEUsuario.USUARIO = USUARIO;            
            oBEUsuario.ESTADO_VIGENCIA = ESTADO_VIGENCIA;
            oBEUsuario.TIPO_USUARIO = TIPO_USUARIO;
            oBEUsuario.PAGINADO = PAGINADO;
            oBEUsuario.TIPO = TIPO;
            var response = oIBLUsuario.Listar(oBEUsuario);
            jResult.Data = response;
            return jResult;
        }

        // GET: api/Usuario/5
        public JsonResult Get(int ID_USUARIO)
        {
            var jResult = new JsonResult();
            var response = oIBLUsuario.ListarPorId(new BEUsuario() { ID_USUARIO = ID_USUARIO });
            jResult.Data = response;
            return jResult;
        }

        public JsonResult Get(int TIPO_USUARIO, int ESTADO)
        {
            var jResult = new JsonResult();
            var response = oIBLUsuario.ListarRecurso(new BEUsuario() { TIPO_USUARIO = TIPO_USUARIO, ESTADO_VIGENCIA = ESTADO });
            jResult.Data = response;
            return jResult;
        }

        // POST: api/Usuario
        public JsonResult Post([FromBody]BEUsuario value)
        {
            var jResult = new JsonResult();
            var response = oIBLUsuario.Registrar(value);
            jResult.Data = response;
            return jResult;
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
