using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// this class is connected to the database, which uses pgsql functions
/// </summary>

namespace AirlineManagementSystem
{
    public class AirlineCompanyDAOPGSQL : ConnectionDataInfo, IAirlineCompanyDAO
    {
        public void Add(AirlineCompany t)
        {
            try
            {
                var res_sp_add = Run_Sp(m_conn, "sp_add_airline_company", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_name", t.Name),
                new NpgsqlParameter("_countryID", t.CountryId),
                new NpgsqlParameter("_userID", t.UserId)
            });
                Console.WriteLine($"[{res_sp_add}] airline company has added successfully ");
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error while adding new airline company: {ex}\nCannot add [{t.Name}, {t.ID}]");
            }
            
        }

        public AirlineCompany Get(int id)
        {
            List<AirlineCompany> airlineCompany = new List<AirlineCompany>();
            AirlineCompany airline = new AirlineCompany();
            var res_sp_get = Run_Sp(m_conn, "sp_get_airline_company_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("a_id", id)
            });
            airlineCompany.AddRange((IEnumerable<AirlineCompany>)res_sp_get);
            airline = (AirlineCompany)airlineCompany.Select(a => res_sp_get);
            return airline;
        }

        public AirlineCompany GetAirlineByUsername(string name)
        {
            AirlineCompany airline = new AirlineCompany();
            List<AirlineCompany> airlineCompany = new List<AirlineCompany>();

            Users user = new Users();
            var res_sp_get_airline_username = Run_Sp(m_conn, "sp_get_airline_by_username", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username", user.Username)
            });
            airlineCompany.AddRange((IEnumerable<AirlineCompany>)res_sp_get_airline_username);
            airline = (AirlineCompany)airlineCompany.Select(a => res_sp_get_airline_username);
            return airline;
        }

        public IList<AirlineCompany> GetAll()
        {
            IList<AirlineCompany> airline = new List<AirlineCompany>();
            AirlineCompany reader = new AirlineCompany();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_airline_companies", new NpgsqlParameter[]
            {
                new NpgsqlParameter("first_name", reader.Name),
                new NpgsqlParameter("last_name",reader.CountryId),
                new NpgsqlParameter("level",reader.UserId)

            });
            airline = (IList<AirlineCompany>)res_sp_get_all.Select(item => item.Values).ToList();
            return airline;
        }

        public IList<AirlineCompany> GetAllAirlineByCountry(int countryId)
        {
            List<AirlineCompany> airlineCompany = new List<AirlineCompany>();
            Country country = new Country();
            var res_sp_get_airline_by_countryID = Run_Sp(m_conn, "sp_get_airline_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_countryId", country.ID)
            });
            airlineCompany.AddRange((IEnumerable<AirlineCompany>)res_sp_get_airline_by_countryID);
            return airlineCompany;
        }

        public void Remove(AirlineCompany t)
        {
            try
            {
                var res_sp_remove = Run_Sp(m_conn, "sp_remove_airline_company", new NpgsqlParameter[]
            {
                new NpgsqlParameter("a_id",t.ID)
            });
                Console.WriteLine($"Run_Sp_Remove => {res_sp_remove}\n{t.Name} airline company was removed successfully");
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error while adding new airline company: {ex}\nCannot add [{t.Name}, {t.ID}]");
            }

        }

        public void Update(AirlineCompany t)
        {
            Console.Write("Enter user ID to update information: ");
            int newID = Convert.ToInt32(Console.ReadLine());
            var res_sp_update = Run_Sp(m_conn, "sp_update_airline_company", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_name",t.Name),
                new NpgsqlParameter("_countryID", t.CountryId),
                new NpgsqlParameter("_userID", t.UserId),
                new NpgsqlParameter("new_id", newID)
            });
            Console.WriteLine($"Successfully Updated users information => [{res_sp_update}]");
        }
    }
}
