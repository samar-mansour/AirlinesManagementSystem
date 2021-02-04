using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public abstract class ConnectionHelper
    {
        public string m_conn = AppConfigFile.GetInstance().ConnectionString;

        //Log not working --> giving argument null exception!!!!!!
        ///private static readonly ILogger my_logger;

        public static bool TestConnection(string conn)
        {
            try
            {
                using (var my_conn = new NpgsqlConnection(conn))
                {
                    my_conn.Open();
                    return true;
                }
            }
            catch (Exception)// ex)
            {
                //my_logger.LogDebug($"Failed! can connect to database: {ex}");
                //my_logger.Log(LogLevel.Critical, "Test Connection DB: {conn} Failed", conn);
                return false;
            }
        }

        //Returning data function as list contaning a dictionary --> runs any stored procedure from database. 
        public static List<Dictionary<string, object>> Run_Sp(string conn_string, string sp_name, NpgsqlParameter[] parameters)
        {
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new NpgsqlConnection(conn_string))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sp_name, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddRange(parameters); 

                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();

                            foreach (var item in reader.GetColumnSchema())
                            {
                                object column_value = reader[item.ColumnName];
                                row.Add(item.ColumnName, column_value);
                            }
                            values.Add(row);
                        }
                    }
                }
            }
            catch (Exception )//ex
            {
                //my_logger.LogError($"Failed to {sp_name} into/from database. Error : {ex}");
                //my_logger.LogDebug($"Stored procedure name: [{sp_name}]");
                //my_logger.Log(LogLevel.Critical, "Stored Procedure DB: {sp_name} Failed", sp_name);
                Console.WriteLine($"Function {sp_name} failed. parameters: {string.Join(",", parameters.Select(_ => _.ParameterName + " : " + _.Value))}");
            }
            return values;
        }


    }
}
