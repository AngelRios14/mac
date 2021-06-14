using PCM.Servicio.Entity;
using PCM.Servicio.Infraestructure.Base;
using PCM.Servicio.Infraestructure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCM.Servicio.Infraestructure.Implementation
{
    public class DAAtencion : IDAAtencion
    {
        public List<BEAtencion> ListarAtencion(BEAtencion oBEAtencion)
        {
            var Result = new List<BEAtencion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_ATENCION", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_ATENCION", SqlDbType.Int, oBEAtencion.ID_ATENCION),
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEAtencion.ID_SERVICIO),
                baseSql.CreateParameter("ID_CIUDADANO", SqlDbType.Int, oBEAtencion.ID_CIUDADANO),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEAtencion.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEAtencion.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEAtencion>().readDataReaderList(oReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                baseSql.CloseConection();
            }

            return Result;
        }

        public List<BEAtencion> ListarCiudadano(BEAtencion oBEAtencion)
        {
            var Result = new List<BEAtencion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_CIUDADANO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_CIUDADANO", SqlDbType.Int, oBEAtencion.ID_CIUDADANO),
                baseSql.CreateParameter("DNI", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.DNI) ? string.Empty : oBEAtencion.DNI)),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.NOMBRE) ? string.Empty : oBEAtencion.NOMBRE)),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEAtencion.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEAtencion.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEAtencion>().readDataReaderList(oReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                baseSql.CloseConection();
            }

            return Result;
        }

        public BEAtencion RegistrarAtencion(BEAtencion oBEAtencion)
        {
            var Result = new BEAtencion();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_ATENCION", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_ATENCION", SqlDbType.Int, oBEAtencion.ID_ATENCION),
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEAtencion.ID_SERVICIO),
                baseSql.CreateParameter("ID_CIUDADANO", SqlDbType.Int, oBEAtencion.ID_CIUDADANO),
                baseSql.CreateParameter("ID_ENTIDAD_MUNICIPAL", SqlDbType.Int, oBEAtencion.ID_ENTIDAD),
                baseSql.CreateParameter("ID_USUARIO_REGISTRA", SqlDbType.Int, oBEAtencion.ID_USUARIO_REGISTRA),
                baseSql.CreateParameter("CORREO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.CORREO) ? string.Empty : oBEAtencion.CORREO)),
                baseSql.CreateParameter("TELEFONO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.TELEFONO) ? string.Empty : oBEAtencion.TELEFONO)),
                baseSql.CreateParameter("ES_FECHA_FIN", SqlDbType.Int, oBEAtencion.ES_FECHA_FIN),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.USUARIO_REGISTRA) ? string.Empty : oBEAtencion.USUARIO_REGISTRA)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEAtencion>().readDataReader(oReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                baseSql.CloseConection();
            }

            return Result;
        }

        public BEAtencion RegistrarCiudadano(BEAtencion oBEAtencion)
        {
            var Result = new BEAtencion();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_CIUDADANO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_CIUDADANO", SqlDbType.Int, oBEAtencion.ID_CIUDADANO),
                baseSql.CreateParameter("DNI", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.DNI) ? string.Empty : oBEAtencion.DNI)),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.NOMBRE) ? string.Empty : oBEAtencion.NOMBRE)),
                baseSql.CreateParameter("APELLIDO_PATERNO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.APELLIDO_PATERNO) ? string.Empty : oBEAtencion.APELLIDO_PATERNO)),
                baseSql.CreateParameter("APELLIDO_MATERNO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.APELLIDO_MATERNO) ? string.Empty : oBEAtencion.APELLIDO_MATERNO)),
                baseSql.CreateParameter("FOTO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.FOTO) ? string.Empty : oBEAtencion.FOTO)),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.USUARIO_REGISTRA) ? string.Empty : oBEAtencion.USUARIO_REGISTRA)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEAtencion>().readDataReader(oReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                baseSql.CloseConection();
            }

            return Result;
        }

        public List<BEAtencion> ListarReporteAtencionSede(BEAtencion oBEAtencion)
        {
            var Result = new List<BEAtencion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_RPT_ATENCION_POR_SEDE", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_ENTIDAD_MUNICIPAL", SqlDbType.Int, oBEAtencion.ID_ENTIDAD),
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, oBEAtencion.ID_SEDE),
                baseSql.CreateParameter("ID_USUARIO_REGISTRA", SqlDbType.Int, oBEAtencion.ID_USUARIO),
                baseSql.CreateParameter("DNI", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.DNI) ? string.Empty : oBEAtencion.DNI)),
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEAtencion.ID_SERVICIO),
                baseSql.CreateParameter("FECHA_INICIO", SqlDbType.DateTime, oBEAtencion.FECHA_INICIO),
                baseSql.CreateParameter("FECHA_FIN", SqlDbType.DateTime, oBEAtencion.FECHA_FIN),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEAtencion.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEAtencion.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEAtencion>().readDataReaderList(oReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                baseSql.CloseConection();
            }

            return Result;
        }

        public List<BEAtencion> ListarReporteAtencionDepartamento(BEAtencion oBEAtencion)
        {
            var Result = new List<BEAtencion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_RPT_ATENCION_POR_DEPARTAMENTO", CommandType.StoredProcedure,
                baseSql.CreateParameter("DEPARTAMENTO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.DEPARTAMENTO) ? string.Empty : oBEAtencion.DEPARTAMENTO)),
                baseSql.CreateParameter("PROVINCIA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.PROVINCIA) ? string.Empty : oBEAtencion.PROVINCIA)),
                baseSql.CreateParameter("DISTRITO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.DISTRITO) ? string.Empty : oBEAtencion.DISTRITO)),
                baseSql.CreateParameter("ID_ENTIDAD_MUNICIPAL", SqlDbType.Int, oBEAtencion.ID_ENTIDAD),
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, oBEAtencion.ID_SEDE),
                baseSql.CreateParameter("ID_USUARIO_REGISTRA", SqlDbType.Int, oBEAtencion.ID_USUARIO),
                baseSql.CreateParameter("DNI", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEAtencion.DNI) ? string.Empty : oBEAtencion.DNI)),
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEAtencion.ID_SERVICIO),
                baseSql.CreateParameter("FECHA_INICIO", SqlDbType.DateTime, oBEAtencion.FECHA_INICIO),
                baseSql.CreateParameter("FECHA_FIN", SqlDbType.DateTime, oBEAtencion.FECHA_FIN),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEAtencion.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEAtencion.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEAtencion>().readDataReaderList(oReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                baseSql.CloseConection();
            }

            return Result;
        }
    }
}
