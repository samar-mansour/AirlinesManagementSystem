using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit two interfaces:
    //one have all the methods of ICountryDAO, the other interface checks the connection to the data
    public class CountryDAOPGSQL : ConnectionHelper, ICountryDAO
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Adding new country record to the database
        public void Add(Country t)
        {
            var res_sp_add = Run_Sp(m_conn, "sp_add_countries", new NpgsqlParameter[]
            {
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
            Console.WriteLine($"{res_sp_add} added successfully!");
        }

        //Returning country record accourding to giving id from the user
        public Country Get(int id)
        {
            GetOpenConnection(m_conn);
            Country country = new Country() 
            {
                    ID = id
            };
            var res_sp_get = Run_Sp(m_conn, "sp_get_country_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",country.ID),
                new NpgsqlParameter("name",country.Name)
            });
            
            return country;
        }

        //Returning function as a country list --> Reading all the record from the database
        public IList<Country> GetAll()
        {
            IList<Country> country = new List<Country>();

            return country;
            //SqlDataReader reader = cmd.ExecuteReader();

            //if (reader.Read() == true)
            //{
            //    store = new Stores
            //    {
            //        ID = Convert.ToInt32(reader["ID"]),
            //        name = reader["Name"].ToString(),
            //        floor = Convert.ToInt32(reader["StoreFloor"]),
            //        categoryID = Convert.ToInt32(reader["Category_ID"])
            //    };
            //}


        }



        //Removing record from the data according to the id
        public void Remove(Country t)
        {
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            });
            Console.WriteLine($"{res_sp_remove} record has been removed");
        }

        //Updating country data using the Run_Country_SP
        public void Update(Country t)
        {
            var res_sp_update = Run_Sp(m_conn, "sp_update_name_code_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
            Console.WriteLine($"{res_sp_update} have been updated");
        }
    }
}
