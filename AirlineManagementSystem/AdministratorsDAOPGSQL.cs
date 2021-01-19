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
    class AdministratorsDAOPGSQL : HelperClass, IAdministratorsDAO
    {
        private string m_conn = AppConfigFile.Instance.ConnectionString;
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        //Adding new administrator record to the database --> using the Run_Admin_SP() function
        public void Add(Administrators t)
        {
            GetOpenConnection(m_conn);
            var res_sp_add = Run_Sp(m_conn, "sp_add_admin", new NpgsqlParameter[]
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
            GetOpenConnection(m_conn);
            Administrators admin = new Administrators()
            {
                ID = id
            };
            var res_sp_get = Run_Sp(m_conn, "sp_get_admin_by_id", new NpgsqlParameter[]
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
            GetOpenConnection(m_conn);
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
            GetOpenConnection(m_conn);
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
        }

        //Updating administrator data record --> using the Run_Admin_SP() function
        public void Update(Administrators t)
        {
            GetOpenConnection(m_conn);
            var res_sp_update = Run_Sp(m_conn, "sp_update_admin", new NpgsqlParameter[]
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
