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
    public class DASede: IDASede
    {
        public List<BESede> Listar(BESede oBESede)
        {
            var Result = new List<BESede>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_SEDE", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, oBESede.ID_SEDE),
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBESede.ID_ENTIDAD),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBESede.NOMBRE) ? string.Empty : oBESede.NOMBRE)),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBESede.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBESede.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BESede>().readDataReaderList(oReader);
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

        public List<BESede> ListarPorId(BESede oBESede)
        {
            var Result = new List<BESede>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_SEDE_ID", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, oBESede.ID_SEDE));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BESede>().readDataReaderList(oReader);
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

        public BESede Registrar(BESede oBESede)
        {
            var Result = new BESede();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_SEDE", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_SEDE", SqlDbType.Int, oBESede.ID_SEDE),
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBESede.ID_ENTIDAD),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBESede.NOMBRE) ? string.Empty : oBESede.NOMBRE)),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBESede.USUARIO_REGISTRA) ? string.Empty : oBESede.USUARIO_REGISTRA)),

                baseSql.CreateParameter("COD_UBIGEO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBESede.COD_UBIGEO) ? string.Empty : oBESede.COD_UBIGEO)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BESede>().readDataReader(oReader);
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
