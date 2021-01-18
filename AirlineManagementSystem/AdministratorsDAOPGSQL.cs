using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    class AdministratorsDAOPGSQL : IAdministratorsDAO
    {
        private string m_conn = "Host=localhost;Username=postgres;Password=MansorySam1993$$;Database=flights_booking_system";
        private bool GetOpenConnection(string conn)
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
                //my_logger.Error($"Failed! can connect to database: {ex}");
                return false;
            }

        }

        private static List<Dictionary<string, object>> Run_Admin_Sp(string conn_string, string sp_name, NpgsqlParameter[] parameters)
        {
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new NpgsqlConnection(conn_string))
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(sp_name, conn);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Function {sp_name} failed.");
            }

            return values;
        }
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

        public IList<Administrators> GetAll()
        {
            //IList<Administrators> admin = new List<Dictionary<string, object>>();
            //var res_sp_get = Run_Admin_Sp(m_conn, "sp_get_all_administrator", new NpgsqlParameter[]
            //{
            //    new NpgsqlParameter()
            //});
            //admin.Add(res_sp_get);
            //return admin;
            return null;
        }

        public void Remove(Administrators t)
        {
            var res_sp_remove = Run_Admin_Sp(m_conn, "sp_remove_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
        }

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
