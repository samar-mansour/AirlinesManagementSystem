using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    abstract class HelperClass
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool GetOpenConnection(string conn)
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

                        cmd.Parameters.Add(parameters);

                        var reader = cmd.ExecuteReader();
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
                my_logger.Error($"Failed to {sp_name} into/from database. Error : {ex}");
                my_logger.Debug($"Run_Country_Sp: [{sp_name}]");
                Console.WriteLine($"Function {sp_name} failed. parameters: {string.Join(",", parameters.Select(_ => _.ParameterName + " : " + _.Value))}");
            }
            return values;
        }
    }
}
