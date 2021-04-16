using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public abstract class ConnectionDataInfo
    {
        public string m_conn = AppConfigFile.GetInstance().ConnectionString;
        public static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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
            catch (Exception ex)
            {
                my_logger.Error($"Failed! can connect to database: {ex}");
                my_logger.Fatal($"Test Connection DB: {conn} Failed");
                return false;
            }
        }

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
            catch (Exception ex)
            {
                my_logger.Fatal($"Failed to get: {sp_name} into/from database. Error : {ex}");
                my_logger.Debug($"Stored procedure name: [{sp_name}]");
                my_logger.Error($"Stored Procedure DB: {sp_name} Failed");
                Console.WriteLine($"Function {sp_name} failed. parameters: {string.Join(",", parameters.Select(_ => _.ParameterName + " : " + _.Value))}");
            }
            return values;
        }
    }
}
