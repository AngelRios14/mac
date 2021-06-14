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
    public class DAUsuario: IDAUsuario
    {
        public List<BEUsuario> Listar(BEUsuario oBEUsuario)
        {
            var Result = new List<BEUsuario>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_USUARIO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_USUARIO", SqlDbType.Int, oBEUsuario.ID_USUARIO),
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBEUsuario.ID_ENTIDAD),
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, oBEUsuario.ID_SEDE),
                baseSql.CreateParameter("DNI", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.DNI) ? string.Empty : oBEUsuario.DNI)),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.NOMBRE) ? string.Empty : oBEUsuario.NOMBRE)),
                baseSql.CreateParameter("APELLIDO_PATERNO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.APELLIDO_PATERNO) ? string.Empty : oBEUsuario.APELLIDO_PATERNO)),
                baseSql.CreateParameter("APELLIDO_MATERNO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.APELLIDO_MATERNO) ? string.Empty : oBEUsuario.APELLIDO_MATERNO)),
                baseSql.CreateParameter("USUARIO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.USUARIO) ? string.Empty : oBEUsuario.USUARIO)),
                baseSql.CreateParameter("ESTADO_VIGENCIA", SqlDbType.Int, oBEUsuario.ESTADO_VIGENCIA),
                baseSql.CreateParameter("TIPO_USUARIO", SqlDbType.Int, oBEUsuario.TIPO_USUARIO),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEUsuario.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEUsuario.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEUsuario>().readDataReaderList(oReader);
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

        public List<BERecurso> ListarRecurso(BEUsuario oBEUsuario)
        {
            var Result = new List<BERecurso>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_RECURSO_MENU", CommandType.StoredProcedure,
                baseSql.CreateParameter("TIPO_USUARIO", SqlDbType.Int, oBEUsuario.TIPO_USUARIO),
                baseSql.CreateParameter("ESTADO", SqlDbType.Int, oBEUsuario.ESTADO_VIGENCIA));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BERecurso>().readDataReaderList(oReader);
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

        public List<BEUsuario> ListarPorId(BEUsuario oBEUsuario)
        {
            var Result = new List<BEUsuario>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_USUARIO_ID", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_USUARIO", SqlDbType.Int, oBEUsuario.ID_USUARIO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEUsuario>().readDataReaderList(oReader);
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

        public BEUsuario Registrar(BEUsuario oBEUsuario)
        {
            var Result = new BEUsuario();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_USUARIO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_USUARIO", SqlDbType.Int, oBEUsuario.ID_USUARIO),
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, (oBEUsuario.ID_ENTIDAD == null ? 0 : oBEUsuario.ID_ENTIDAD)),
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, (oBEUsuario.ID_SEDE == null ? 0 : oBEUsuario.ID_SEDE)),
                baseSql.CreateParameter("DNI", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.DNI) ? string.Empty : oBEUsuario.DNI)),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.NOMBRE) ? string.Empty : oBEUsuario.NOMBRE)),
                baseSql.CreateParameter("APELLIDO_PATERNO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.APELLIDO_PATERNO) ? string.Empty : oBEUsuario.APELLIDO_PATERNO)),
                baseSql.CreateParameter("APELLIDO_MATERNO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.APELLIDO_MATERNO) ? string.Empty : oBEUsuario.APELLIDO_MATERNO)),
                baseSql.CreateParameter("USUARIO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.USUARIO) ? string.Empty : oBEUsuario.USUARIO)),
                baseSql.CreateParameter("PASSWORD", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.PASSWORD) ? string.Empty : oBEUsuario.PASSWORD)),
                baseSql.CreateParameter("FECHA_CADUCA", SqlDbType.DateTime, oBEUsuario.FECHA_CADUCA),
                baseSql.CreateParameter("ESTADO_VIGENCIA", SqlDbType.Int, oBEUsuario.ESTADO_VIGENCIA),
                baseSql.CreateParameter("TIPO_USUARIO", SqlDbType.Int, oBEUsuario.TIPO_USUARIO),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEUsuario.USUARIO_REGISTRA) ? string.Empty : oBEUsuario.USUARIO_REGISTRA)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEUsuario>().readDataReader(oReader);
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
