using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit two interfaces:
    //one have all the methods of ICountryDAO, the other interface checks the connection to the data
    public class CountryDAOPGSQL : ConnectionDataInfo, ICountryDAO
    {
        public void Add(Country t)
        {
            try
            {
                var res_sp_add = Run_Sp(m_conn, "sp_add_countries", new NpgsqlParameter[]
                {
                new NpgsqlParameter("_country_name",t.Name),
                new NpgsqlParameter("_country_code", t.CodeCountryName)
                });
                Console.WriteLine($"{res_sp_add} added successfully!");
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error: {ex}\nCountry: [{t.ID}, {t.Name}, {t.CodeCountryName}] excited cannot be added");
            }
            
        }

        public Country Get(int id)
        {
            Country country = new Country();
            List<Country> countryList = new List<Country>();
            Flights flight = new Flights();
            var res_sp_get = Run_Sp(m_conn, "sp_get_flight_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("f_id", id)
            });
            countryList.AddRange((IEnumerable<Country>)res_sp_get);
            country = (Country)countryList.Select(a => res_sp_get);
            return country;
        }

        public IList<Country> GetAll()
        {
            IList<Country> country = new List<Country>();
            Country reader = new Country();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_countries", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id", reader.ID),
                new NpgsqlParameter("country", reader.Name),
                new NpgsqlParameter("code",reader.CodeCountryName)

            });
            country = (IList<Country>)res_sp_get_all.Select(item => item.Values).ToList();
            return country;

        }

        public Users GetUserByUsername(string name)
        {
            List<Users> usersList = new List<Users>();
            Users user = new Users();
            var res_sp_get_country_username = Run_Sp(m_conn, "sp_get_country_username", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_username", name)
            });
            usersList.AddRange((IEnumerable<Users>)res_sp_get_country_username);
            user = (Users)usersList.Select(a => res_sp_get_country_username);
            return user;
        }

        public void Remove(Country t)
        {
            try
            {
                var res_sp_remove = Run_Sp(m_conn, "sp_remove_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
                Console.WriteLine($"{res_sp_remove} record has been removed");
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error: {ex}\nCountry: [{t.ID}, {t.Name}, {t.CodeCountryName}] not excited, cannot be removed");
            }

        }

        public void Update(Country t)
        {
            try
            {
                Console.Write("Enter user ID to update information: ");
                int newID = Convert.ToInt32(Console.ReadLine());
                var res_sp_update = Run_Sp(m_conn, "sp_update_name_code_country", new NpgsqlParameter[]
                {
                new NpgsqlParameter("_country_code",t.Name),
                new NpgsqlParameter("_country_name", t.CodeCountryName),
                new NpgsqlParameter("new_id", newID)
                });
                Console.WriteLine($"{res_sp_update} have been updated");
            }
            catch (Exception ex)
            {
                my_logger.Info($"Error: {ex}\nCountry: [{t.ID}, {t.Name}, {t.CodeCountryName}] not excited, cannot be updated");
            }
            
        }
    }
}
