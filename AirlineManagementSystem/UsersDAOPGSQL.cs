using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class UsersDAOPGSQL : ConnectionDataInfo, IUsersDAO
    {
        public void Add(Users t)
        {
            try
            {
                var res_sp_add = Run_Sp(m_conn, "sp_add_user", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username", t.Username),
                new NpgsqlParameter("_pass", t.Password),
                new NpgsqlParameter("_email", t.Email),
                new NpgsqlParameter("_userRole", t.UserRole)
            });
                res_sp_add.ForEach(user => Console.WriteLine($"New [{user}] has added successfully"));
            }
            catch (Exception ex)
            {
                my_logger.Info($"Cannot add user twice {ex}.\nUser: [ID: {t.ID}, username: {t.Username}] already exist");
            }
            
        }

        public Users Get(int id)
        {
            List<Users> userList = new List<Users>();
            Users user = new Users();
            var res_sp_get = Run_Sp(m_conn, "sp_get_user_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("user_id", id)
            });
            userList.AddRange((IEnumerable<Users>)res_sp_get);
            user = (Users)userList.Select(a => res_sp_get);
            return user;
        }

        public IList<Users> GetAll()
        {
            IList<Users> ticket = new List<Users>();
            Users reader = new Users();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_users", new NpgsqlParameter[]
            {
                new NpgsqlParameter("flightID",reader.Username),
                new NpgsqlParameter("_pass", reader.Password),
                new NpgsqlParameter("_email", reader.Email),
                new NpgsqlParameter("_userRole", reader.UserRole)

            });
            ticket = (IList<Users>)res_sp_get_all.Select(item => item.Values).ToList();
            return ticket;
        }

        public Users GetUserByUsername(string name)
        {
            List<Users> usersList = new List<Users>();
            Users user = new Users();
            var res_sp_get_username = Run_Sp(m_conn, "sp_get_by_username", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username", name)
            });
            usersList.AddRange((IEnumerable<Users>)res_sp_get_username);
            user = (Users)usersList.Select(a => res_sp_get_username);
            return user;
        }

        public void Remove(Users t)
        {
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_user", new NpgsqlParameter[]
            {
                new NpgsqlParameter("u_id",t.ID)
            });
            Console.WriteLine($"Run_Sp_Remove => {res_sp_remove}\n{t.Username} airline company was removed successfully");
        }

        public void Update(Users t)
        {
            Console.Write("Enter user ID to update information: ");
            int newID = Convert.ToInt32(Console.ReadLine());
            var res_sp_update = Run_Sp(m_conn, "sp_update_users", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username",t.Username),
                new NpgsqlParameter("_pass", t.Password),
                new NpgsqlParameter("_email", t.Email),
                new NpgsqlParameter("_userRole", t.UserRole),
                new NpgsqlParameter("new_id", newID)
            });
            Console.WriteLine($"Successfully Updated users information => [{res_sp_update}]");
        }
    }
}
