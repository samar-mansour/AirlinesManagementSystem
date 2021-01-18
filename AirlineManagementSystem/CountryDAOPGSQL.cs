﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    class CountryDAOPGSQL : ICountryDAO
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

        private static List<Dictionary<string, object>> Run_Country_Sp(string conn_string, string sp_name, NpgsqlParameter[] parameters)
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
        public void Add(Country t)
        {
            var res_sp_add = Run_Country_Sp(m_conn, "sp_add_countries", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID),
                new NpgsqlParameter("name",t.Name),
                new NpgsqlParameter("code_name", t.CodeCountryName)
            });
        }

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

        public IList<Country> GetAll()
        {
            //IList<Country> countries = new List<Dictionary<string, object>>();
            //var res_sp_get = Run_Country_Sp(m_conn, "sp_get_country_by_id", new NpgsqlParameter[]
            //{
            //    new NpgsqlParameter()
            //});
            //countries.Add(res_sp_get);
            //return countries;
            return null;
        }

        public void Remove(Country t)
        {
            var res_sp_remove = Run_Country_Sp(m_conn, "sp_remove_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id",t.ID)
            }); 
        }

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
