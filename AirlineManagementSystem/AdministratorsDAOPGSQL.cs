using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit two interfaces:
    //one have all the methods of administratorDAO, the other interface checks the connection to the data
    class AdministratorsDAOPGSQL : IConnectionChecker, IAdministratorsDAO
    {
        private string m_conn;
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
        private static List<Dictionary<string, object>> Run_Admin_Sp(string conn_string, string sp_name, NpgsqlParameter[] parameters)
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
                my_logger.Error($"Failed to {sp_name} into database. Error : {ex}");
                my_logger.Debug($"Run_Admin_Sp: [{sp_name}]");
                Console.WriteLine($"Function {sp_name} failed.");
            }

            return values;
        }

        //Adding new administrator record to the database --> using the Run_Admin_SP() function
        public void Add(Administrators t)
        {
            var res_sp_add = Run_Admin_Sp(m_conn, "sp_add_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("first_name",t.FirstName),
                new NpgsqlParameter("last_name", t.LastName),
                new NpgsqlParameter("level", t.Level),
                new NpgsqlParameter("user_id", t.UserID)
            });
        }

        //Returning adminstrator record depends on giving id --> using the Run_Admin_SP() function
        public Administrators Get(int id)
        {
            Administrators admin = new Administrators()
            {
                ID = id
            };

            var res_sp_get = Run_Admin_Sp(m_conn, "sp_get_admin_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",admin)
            });

            return admin;
        }

        //Returning function as a administrator list --> Reading all the record from the database
        //using the Run_Admin_SP() function
        public IList<Administrators> GetAll()
        {
            IList<Administrators> admin = new List<Administrators>();

            using (var conn = new NpgsqlConnection(m_conn))
            {
                conn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("sp_get_all_administrator", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            admin.Add(
                                new Administrators
                                {
                                    ID = Convert.ToInt32(reader["id"]),
                                    FirstName = reader["first_name"].ToString(),
                                    LastName = reader["last_name"].ToString(),
                                    Level = Convert.ToInt32(reader["level"]),
                                    UserID = Convert.ToInt32(reader["user_id"])
                                });
                        }
                    }
                }
                catch (Exception ex)
                {
                    my_logger.Debug($"Failed to get all administrator records. Error : {ex}");
                    my_logger.Info($"GetAll: [sp_get_all_administrator]");
                }
                return admin;
            }
        }

        //Removing record from the data depending on the giving id --> using the Run_Admin_SP() function
        public void Remove(Administrators t)
        {
            var res_sp_remove = Run_Admin_Sp(m_conn, "sp_remove_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
        }

        //Updating administrator data record --> using the Run_Admin_SP() function
        public void Update(Administrators t)
        {
            var res_sp_update = Run_Admin_Sp(m_conn, "sp_update_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("first_name",t.FirstName),
                new NpgsqlParameter("last_name", t.LastName),
                new NpgsqlParameter("level", t.Level),
                new NpgsqlParameter("user_id", t.UserID)

            });
        }
    }
}
