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
    public class DAOperacion : IDAOperacion
    {
        public List<BEOperacion> Listar(BEOperacion oBEOperacion)
        {
            var Result = new List<BEOperacion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_OPERACION", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_OPERACION", SqlDbType.Int, oBEOperacion.ID_OPERACION),
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEOperacion.ID_SERVICIO),
                baseSql.CreateParameter("DESCRIPCION", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEOperacion.DESCRIPCION) ? string.Empty : oBEOperacion.DESCRIPCION)),
                baseSql.CreateParameter("ACCION", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEOperacion.ACCION) ? string.Empty : oBEOperacion.ACCION)),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEOperacion.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEOperacion.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEOperacion>().readDataReaderList(oReader);
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

        public List<BEOperacion> ListarPorId(BEOperacion oBEOperacion)
        {
            var Result = new List<BEOperacion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_OPERACION_ID", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_OPERACION", SqlDbType.Int, oBEOperacion.ID_OPERACION));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEOperacion>().readDataReaderList(oReader);
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

        public BEOperacion Registrar(BEOperacion oBEOperacion)
        {
            var Result = new BEOperacion();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_OPERACION", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_OPERACION", SqlDbType.Int, oBEOperacion.ID_OPERACION),
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEOperacion.ID_SERVICIO),
                baseSql.CreateParameter("DESCRIPCION", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEOperacion.DESCRIPCION) ? string.Empty : oBEOperacion.DESCRIPCION)),
                baseSql.CreateParameter("ACCION", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEOperacion.ACCION) ? string.Empty : oBEOperacion.ACCION)),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEOperacion.USUARIO_REGISTRA) ? string.Empty : oBEOperacion.USUARIO_REGISTRA)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEOperacion>().readDataReader(oReader);
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
