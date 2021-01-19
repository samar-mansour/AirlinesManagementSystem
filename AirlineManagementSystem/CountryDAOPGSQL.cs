using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    // This class using stored procedures from postgerSQL. Also inherit two interfaces:
    //one have all the methods of ICountryDAO, the other interface checks the connection to the data
    class CountryDAOPGSQL : IConnectionChecker, ICountryDAO
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
        private static List<Dictionary<string, object>> Run_Country_Sp(string conn_string, string sp_name, NpgsqlParameter[] parameters)
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
                my_logger.Error($"Failed to {sp_name} into/from database. Error : {ex}");
                my_logger.Debug($"Run_Country_Sp: [{sp_name}]");
                Console.WriteLine($"Function {sp_name} failed.");
            }
            return values;
        }

        //Adding new country record to the database
        public void Add(Country t)
        {
            var res_sp_add = Run_Country_Sp(m_conn, "sp_add_countries", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
        }

        //Returning country record accourding to giving id from the user
        public Country Get(int id)
        {
            Country country = new Country() 
            {
                    ID = id
            };

            var res_sp_get = Run_Country_Sp(m_conn, "sp_get_country_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",country)
            });
            
            return country;
        }

        //Returning function as a country list --> Reading all the record from the database
        public IList<Country> GetAll()
        {
            IList<Country> country = new List<Country>();

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
            var res_sp_remove = Run_Country_Sp(m_conn, "sp_remove_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            }); 
        }

        //Updating country data using the Run_Country_SP
        public void Update(Country t)
        {
            var res_sp_update = Run_Country_Sp(m_conn, "sp_update_name_code_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
        }
    }
}
