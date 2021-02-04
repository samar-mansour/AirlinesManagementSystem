using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit helper class and interface.
    public class AdministratorsDAOPGSQL : ConnectionHelper, IAdministratorsDAO
    {        

        public void Add(Administrators t)
        {
            var res_sp_add = Run_Sp(m_conn, "sp_add_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_admin_name",t.FirstName),
                new NpgsqlParameter("_admin_surname", t.LastName),
                new NpgsqlParameter("_admin_level", t.Level),
                new NpgsqlParameter("_userid", t.UserID)
            });
            Console.WriteLine($"New Adminstrator added successfully => [{res_sp_add}]");
        }

        public Administrators Get(int id)
        {
            Administrators admin = new Administrators();
            var res_sp_get = Run_Sp(m_conn, "sp_get_admin_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("a_id", id)
            });
            if (res_sp_get != null)
            {
                admin.ID = id;
            }
            return admin;
        }


        public IList<Administrators> GetAll()
        {
            IList<Administrators> admin = new List<Administrators>();
            Administrators reader = new Administrators();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_administrator", new NpgsqlParameter[]
            {
                new NpgsqlParameter("first_name", reader.FirstName),
                new NpgsqlParameter("last_name",reader.LastName),
                new NpgsqlParameter("level",reader.Level),
                new NpgsqlParameter("user_id",reader.UserID)

            });
            admin = (IList<Administrators>)res_sp_get_all.Select(item => item.Values).ToList();
            return admin;
        }

        public void Remove(Administrators t)
        {
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
            Console.WriteLine($"Run_Sp_Remove => {res_sp_remove} was removed successfully");
        }

        public void Update(Administrators t)
        {
            Console.Write("Enter user ID to update information: ");
            int newID = Convert.ToInt32(Console.ReadLine());
            var res_sp_update = Run_Sp(m_conn, "sp_update_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_admin_name",t.FirstName),
                new NpgsqlParameter("_admin_surname", t.LastName),
                new NpgsqlParameter("_admin_level", t.Level),
                new NpgsqlParameter("_userID", t.UserID),
                new NpgsqlParameter("new_id", newID)
            });
            Console.WriteLine($"Successfully Updated users information => [{res_sp_update}]");
        }
    }
}
