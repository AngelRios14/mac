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
    public class DAEntidad: IDAEntidad
    {
        public List<BEEntidad> Listar(BEEntidad oBEEntidad)
        {
            var Result = new List<BEEntidad>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_ENTIDAD", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.VarChar, oBEEntidad.ID_ENTIDAD),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.NOMBRE) ? string.Empty : oBEEntidad.NOMBRE)),
                baseSql.CreateParameter("RUC", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.RUC) ? string.Empty : oBEEntidad.RUC)),
                baseSql.CreateParameter("COD_UBIGEO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.COD_UBIGEO) ? string.Empty : oBEEntidad.COD_UBIGEO)),
                baseSql.CreateParameter("ORDEN", SqlDbType.Int, oBEEntidad.ORDEN),
                baseSql.CreateParameter("ES_MUNICIPALIDAD", SqlDbType.Bit, oBEEntidad.ES_MUNICIPALIDAD),
                baseSql.CreateParameter("PAGINADO", SqlDbType.Int, oBEEntidad.PAGINADO),
                baseSql.CreateParameter("TIPO", SqlDbType.Int, oBEEntidad.TIPO));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEEntidad>().readDataReaderList(oReader);
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

        public List<BEEntidad> ListarPorId(BEEntidad oBEEntidad)
        {
            var Result = new List<BEEntidad>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_ENTIDAD_ID", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBEEntidad.ID_ENTIDAD));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEEntidad>().readDataReaderList(oReader);
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

        public List<BEEntidad> BuscarUbigeo(BEEntidad oBEEntidad)
        {
            var Result = new List<BEEntidad>();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_LST_UBIGEO", CommandType.StoredProcedure,
                baseSql.CreateParameter("CONDICION", SqlDbType.Int, oBEEntidad.CONDICION),
                baseSql.CreateParameter("COD_UBIGEO_DEPARTAMENTO", SqlDbType.VarChar, oBEEntidad.COD_UBIGEO_DEPARTAMENTO),
                baseSql.CreateParameter("COD_UBIGEO_PROVINCIA", SqlDbType.VarChar, oBEEntidad.COD_UBIGEO_PROVINCIA));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEEntidad>().readDataReaderList(oReader);
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

        public BEEntidad Registrar(BEEntidad oBEEntidad) {
            var Result = new BEEntidad();
            BaseSql baseSql = new BaseSql();
            try
            {
                baseSql.OpenConection();
                baseSql.CreateCommand("SP_INS_ENTIDAD", CommandType.StoredProcedure,
                baseSql.CreateParameter("ID_ENTIDAD", SqlDbType.Int, oBEEntidad.ID_ENTIDAD),
                baseSql.CreateParameter("NOMBRE", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.NOMBRE) ? string.Empty : oBEEntidad.NOMBRE)),
                baseSql.CreateParameter("RUC", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.RUC) ? string.Empty : oBEEntidad.RUC)),
                baseSql.CreateParameter("COD_UBIGEO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.COD_UBIGEO) ? string.Empty : oBEEntidad.COD_UBIGEO)),
                baseSql.CreateParameter("ORDEN", SqlDbType.Int, oBEEntidad.ORDEN),
                baseSql.CreateParameter("RUTA_LOGO", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.RUTA_LOGO) ? string.Empty : oBEEntidad.RUTA_LOGO)),
                baseSql.CreateParameter("ES_MUNICIPALIDAD", SqlDbType.VarChar, oBEEntidad.ES_MUNICIPALIDAD),
                baseSql.CreateParameter("USUARIO_REGISTRA", SqlDbType.VarChar, (string.IsNullOrEmpty(oBEEntidad.USUARIO_REGISTRA) ? string.Empty : oBEEntidad.USUARIO_REGISTRA)));

                using (IDataReader oReader = baseSql.GetDataReader(CommandBehavior.CloseConnection))
                {
                    Result = new GenericInstance<BEEntidad>().readDataReader(oReader);
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
