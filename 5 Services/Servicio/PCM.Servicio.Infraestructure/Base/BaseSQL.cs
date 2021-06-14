
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PCM.Servicio.Infraestructure.Base
{
    public class BaseSql
    {
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private string sError;

        public BaseSql()   {
            sError = "";
        }

        public void OpenConection()
        {
           string connection= ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connection);
            sqlCommand = new SqlCommand();
        }
        public void CloseConection()
        {
            if (sqlCommand != null)
            {
                sqlCommand.Dispose();
                sqlCommand.Connection.Close();
                sqlCommand = null;
            }
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }
        }
        public void CreateCommand(string Source, System.Data.CommandType sqlCommandType, params object[] Args)
        {
            sqlCommand.CommandText = Source;
            sqlCommand.CommandType = sqlCommandType;
            sqlCommand.Parameters.AddRange(Args);
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Connection.Open();
        }
        public void CreateCommand(string Source, System.Data.CommandType sqlCommandType)
        {
            sqlCommand.CommandText = Source;
            sqlCommand.CommandType = sqlCommandType;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Connection.Open();
        }


        public SqlParameter CreateParameter(string ParameterName, System.Data.ParameterDirection Direction, SqlDbType SqlDbType, Object Value)
        {
            var SqlParameter = new SqlParameter
            {
                ParameterName = "@" + ParameterName,
                Direction = Direction,
                SqlDbType = SqlDbType,
                Value = Value
            };
            return SqlParameter;
        }
        public SqlParameter CreateParameterOutput(string ParameterName, SqlDbType SqlDbType, int Size)
        {
            var SqlParameter = new SqlParameter
            {
                ParameterName = "@" + ParameterName,
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = SqlDbType,
                Size = Size
            };
            return SqlParameter;
        }
        public SqlParameter CreateParameterOutput(string ParameterName, SqlDbType SqlDbType)
        {
            var SqlParameter = new SqlParameter
            {
                ParameterName = "@" + ParameterName,
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = SqlDbType
            };
            return SqlParameter;
        }
        public SqlParameter CreateParameter(string ParameterName, SqlDbType SqlDbType, Object Value)
        {
            var SqlParameter = new SqlParameter
            {
                ParameterName = "@" + ParameterName,
                Direction = System.Data.ParameterDirection.Input,
                SqlDbType = SqlDbType,
                Value = Value
            };
            return SqlParameter;
        }

        public int Execute()
        {
            return sqlCommand.ExecuteNonQuery();
        }
        public object ExecuteScalar()
        {
            return sqlCommand.ExecuteScalar();
        }
        public object GetOutParameter(int Parameter)
        {
            return sqlCommand.Parameters[Parameter].Value;
        }
        public object GetOutParameter(string Parameter)
        {
            return sqlCommand.Parameters[Parameter].Value;
        }
        public SqlParameterCollection GetOutParameter()
        {
            return sqlCommand.Parameters;
        }
        public SqlDataReader GetDataReader(System.Data.CommandBehavior sqlCommandBehavior)
        {
            return sqlCommand.ExecuteReader(sqlCommandBehavior);
        }
        public System.Data.DataTable GetDataTable()
        {
            var dt = new System.Data.DataTable();
            var da = new SqlDataAdapter(sqlCommand);
            da.Fill(dt);
            return dt;
        }
        public SqlConnection ObjectoConexion
        {
            get { return sqlConnection; }
        }
        public string getError
        {
            get { return sError; }
        }
        public void Close()
        {
            sqlConnection.Close();
        }

    }

}
