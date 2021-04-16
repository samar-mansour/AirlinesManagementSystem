using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit from connection data class and interface.
    public class AdministratorsDAOPGSQL : ConnectionDataInfo, IAdministratorsDAO
    {        

        public void Add(Administrators t)
        {
            try
            {
                var res_sp_add = Run_Sp(m_conn, "sp_add_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_admin_name",t.FirstName),
                new NpgsqlParameter("_admin_surname", t.LastName),
                new NpgsqlParameter("_admin_level", t.Level),
                new NpgsqlParameter("_userid", t.UserID)
            });
                res_sp_add.ForEach(admin => Console.WriteLine($"New administrator: [{admin}] has been added successfully "));
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error: {ex}\nAdminstrator: [{t.ID}, {t.FirstName}, {t.LastName}, {t.Level}] is excited. cannot be added twice");
            }

        }

        public Administrators Get(int id)
        {
            List<Administrators> adminlist = new List<Administrators>();
            Administrators admin = new Administrators();

            var res_sp_get = Run_Sp(m_conn, "sp_get_admin_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("a_id", id)
            });
            adminlist.AddRange((IEnumerable<Administrators>)res_sp_get.ToList());
            admin = (Administrators)adminlist.Select(a => a);
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
            admin = (IList<Administrators>)res_sp_get_all.SelectMany(item => item.Values).ToList();
            return admin;
        }

        public Users GetUserByUsername(string name)
        {
            List<Users> usersList = new List<Users>();
            Users user = new Users();
            var res_sp_get_username = Run_Sp(m_conn, "sp_get_admin_username", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username", name)
            });
            usersList.AddRange((IEnumerable<Users>)res_sp_get_username);
            user = (Users)usersList.Select(a => res_sp_get_username);
            return user;
        }

        public void Remove(Administrators t)
        {
            try
            {
                var res_sp_remove = Run_Sp(m_conn, "sp_remove_admin", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
                Console.WriteLine($"Run_Sp_Remove => {res_sp_remove} was removed successfully");
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error: {ex}\nAdminstrator: [{t.ID}, {t.FirstName}, {t.LastName}, {t.Level}] is not excited.");
            }

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
