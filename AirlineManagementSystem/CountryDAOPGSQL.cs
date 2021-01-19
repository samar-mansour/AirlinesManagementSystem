using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit two interfaces:
    //one have all the methods of ICountryDAO, the other interface checks the connection to the data
    class CountryDAOPGSQL : HelperClass, ICountryDAO
    {
        private string m_conn = AppConfigFile.Instance.ConnectionString;
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        

        //Adding new country record to the database
        public void Add(Country t)
        {
            GetOpenConnection(m_conn);
            var res_sp_add = Run_Sp(m_conn, "sp_add_countries", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
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
                new NpgsqlParameter("id",country)
            });
            
            return country;
        }

        //Returning function as a country list --> Reading all the record from the database
        public IList<Country> GetAll()
        {
            IList<Country> country = new List<Country>();
            GetOpenConnection(m_conn);
            using (var conn = new NpgsqlConnection(m_conn))
            {
                conn.Open();
                try
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("sp_get_all_countries", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            country.Add(
                                new Country
                                {
                                    ID = Convert.ToInt32(reader["id"]),
                                    Name = reader["name"].ToString(),
                                    CodeCountryName = reader["code_name"].ToString()
                                });
                        }
                    }
                }
                catch (Exception ex)
                {
                    my_logger.Debug($"Failed to Get all the recods from country database. Error : {ex}");
                    my_logger.Info($"Get All Country records: [sp_get_all_countries]");
                }
                return country;
            }

        }



        //Removing record from the data according to the id
        public void Remove(Country t)
        {
            GetOpenConnection(m_conn);
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            }); 
        }

        //Updating country data using the Run_Country_SP
        public void Update(Country t)
        {
            GetOpenConnection(m_conn);
            var res_sp_update = Run_Sp(m_conn, "sp_update_name_code_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
        }
    }
}
