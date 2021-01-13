using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineManagementSystem
{
    class CountryDAOPGSQL : ICountryDAO
    {
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

        public void Add(Country t)
        {
            throw new NotImplementedException();
        }

        public Country Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Country> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Country t)
        {
            throw new NotImplementedException();
        }

        public void Update(Country t)
        {
            throw new NotImplementedException();
        }
    }
}
