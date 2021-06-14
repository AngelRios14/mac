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
    public class DAParametro : IDAParametro
    {
        public List<BEParametro> Listar(BEParametro oBEParametro)
        {
            var Result = new List<BEParametro>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_PARAMETRO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_PARAMETRO", SqlDbType.Int, oBEParametro.ID_PARAMETRO),
                baseSql.CreateParameter("ID_OPERACION", SqlDbType.Int, oBEParametro.ID_OPERACION),
                baseSql.CreateParameter("PARAMETRO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEParametro.PARAMETRO) ? string.Empty : oBEParametro.PARAMETRO)),
                baseSql.CreateParameter("ID_OBLIGATORIO", SqlDbType.Bit, oBEParametro.ID_OBLIGATORIO),
                baseSql.CreateParameter("ID_TIPO_PARAMETRO", SqlDbType.Int, oBEParametro.ID_TIPO_PARAMETRO),
                baseSql.CreateParameter("COMENTARIO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEParametro.COMENTARIO) ? string.Empty : oBEParametro.COMENTARIO)),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEParametro.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEParametro.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEParametro>().readDataReaderList(oReader);
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

        public List<BEParametro> ListarPorId(BEParametro oBEParametro)
        {
            var Result = new List<BEParametro>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_PARAMETRO_ID", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_PARAMETRO", SqlDbType.Int, oBEParametro.ID_PARAMETRO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEParametro>().readDataReaderList(oReader);
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

        public BEParametro Registrar(BEParametro oBEParametro)
        {
            var Result = new BEParametro();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_PARAMETRO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_PARAMETRO", SqlDbType.Int, oBEParametro.ID_PARAMETRO),
                baseSql.CreateParameter("ID_OPERACION", SqlDbType.Int, oBEParametro.ID_OPERACION),
                baseSql.CreateParameter("PARAMETRO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEParametro.PARAMETRO) ? string.Empty : oBEParametro.PARAMETRO)),
                baseSql.CreateParameter("ID_OBLIGATORIO", SqlDbType.Int, oBEParametro.ID_OBLIGATORIO),
                baseSql.CreateParameter("ID_TIPO_PARAMETRO", SqlDbType.Int, oBEParametro.ID_TIPO_PARAMETRO),
                baseSql.CreateParameter("COMENTARIO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEParametro.COMENTARIO) ? string.Empty : oBEParametro.COMENTARIO)),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEParametro.USUARIO_REGISTRA) ? string.Empty : oBEParametro.USUARIO_REGISTRA)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEParametro>().readDataReader(oReader);
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
