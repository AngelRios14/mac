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
   public class DAServicio: IDAServicio
    {

        public List<BEServicio> buscar(BEServicio oBEServicio)
        {
            var Result = new List<BEServicio>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("PK_MAS_SERVICE_BUSCAR", CommandType.StoredProcedure,
                baseSql.CreateParameter("P_NOMBRE", SqlDbType.NVarChar, " "),
                baseSql.CreateParameter("P_PAGDESDE", SqlDbType.Int, oBEServicio.getPageNumber()),
                baseSql.CreateParameter("P_PAGHASTA", SqlDbType.Int, oBEServicio.getPageRows()));
                //baseSql.CreateParameter("P_PAGHASTA", SqlDbType.Int, 16));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEServicio>().readDataReaderList(oReader);
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

        public List<BEServicioAtencion> ListarAtencion(BEServicio oBEServicio)
        {
            var Result = new List<BEServicioAtencion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_SERVICIO_ATENCION_V2", CommandType.StoredProcedure,
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.NOMBRE) ? string.Empty : oBEServicio.NOMBRE)),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEServicio.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEServicio.TIPO),
                baseSql.CreateParameter("ID_ENTIDAD_USUARIO", SqlDbType.Int, oBEServicio.ID_ENTIDAD));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEServicioAtencion>().readDataReaderList(oReader);
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
        public List<BEServicioAtencion> ListarAtencionOrig(BEServicio oBEServicio)
        {
            var Result = new List<BEServicioAtencion>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_SERVICIO_ATENCION", CommandType.StoredProcedure,
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.NOMBRE) ? string.Empty : oBEServicio.NOMBRE)),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEServicio.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEServicio.TIPO),
                baseSql.CreateParameter("ID_ENTIDAD_USUARIO", SqlDbType.Int, oBEServicio.ID_ENTIDAD));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEServicioAtencion>().readDataReaderList(oReader);
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
        public List<BEServicio> Listar(BEServicio oBEServicio)
        {
            var Result = new List<BEServicio>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_SERVICIO", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEServicio.ID_SERVICIO),
                baseSql.CreateParameter("ID_TIPOSERVICIO", SqlDbType.Int, oBEServicio.ID_TIPOSERVICIO),
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBEServicio.ID_ENTIDAD),
                baseSql.CreateParameter("ID_MODOSERVICIO", SqlDbType.Int, oBEServicio.ID_MODOSERVICIO),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.NOMBRE) ? string.Empty : oBEServicio.NOMBRE)),
                baseSql.CreateParameter("ID_TIPOACCESO", SqlDbType.Int, oBEServicio.ID_TIPOACCESO),
                baseSql.CreateParameter("LINK", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.LINK) ? string.Empty : oBEServicio.LINK)),
                baseSql.CreateParameter("ORDEN", SqlDbType.Int, oBEServicio.ORDEN),
                baseSql.CreateParameter("ESTADO_VIGENCIA", SqlDbType.Int, oBEServicio.ESTADO_VIGENCIA),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEServicio.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEServicio.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEServicio>().readDataReaderList(oReader);
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
        public List<BEServicio> ListarPorId(BEServicio oBEServicio)
        {
            var Result = new List<BEServicio>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_SERVICIO_ID", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEServicio.ID_SERVICIO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEServicio>().readDataReaderList(oReader);
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
        public BEServicio Registrar(BEServicio oBEServicio)
        {
            var Result = new BEServicio();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_SERVICIO_V2", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_SERVICIO", SqlDbType.Int, oBEServicio.ID_SERVICIO),
                baseSql.CreateParameter("ID_TIPOSERVICIO", SqlDbType.Int, oBEServicio.ID_TIPOSERVICIO),
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBEServicio.ID_ENTIDAD),
                baseSql.CreateParameter("ID_MODOSERVICIO", SqlDbType.Int, oBEServicio.ID_MODOSERVICIO),                
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.NOMBRE) ? string.Empty : oBEServicio.NOMBRE)),
                baseSql.CreateParameter("ID_TIPOACCESO", SqlDbType.Int, oBEServicio.ID_TIPOACCESO),
                baseSql.CreateParameter("LINK", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.LINK) ? string.Empty : oBEServicio.LINK)),
                baseSql.CreateParameter("ORDEN", SqlDbType.Int, oBEServicio.ORDEN),
                baseSql.CreateParameter("ESTADO_VIGENCIA", SqlDbType.Int, oBEServicio.ESTADO_VIGENCIA),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEServicio.USUARIO_REGISTRA) ? string.Empty : oBEServicio.USUARIO_REGISTRA)),
                baseSql.CreateParameter("DNI_URL", SqlDbType.VarChar, oBEServicio.DNI_URL)
                );

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEServicio>().readDataReader(oReader);
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
