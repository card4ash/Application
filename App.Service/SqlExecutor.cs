using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AppProj.Service
{
    public class SqlExecutor
    {
        private SqlConnection connection;
        public SqlExecutor()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AppModelContainer_Exec"].ConnectionString);
        }

        public object ExecStoredProcedure(string Name, SqlParameter[] parameters)
        {
            object obj = new object();
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Name, connection);
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }

                obj = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return obj;
        }

        public DataTable ExecStoredProcedureWithReturn(string Name, SqlParameter[] parameters)
        {
            DataTable data = new DataTable();
            
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Name, connection);
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }

                data.Load(command.ExecuteReader());
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return data;
        }
    }
}
