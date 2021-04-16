using AirlineManagementSystem.BusinessLogic_Facades;
using AirlineManagementSystem.Login;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// singleton class  that have a daily thread (+create FlightsHistory)
/// also it removes the old flights and adding them to history file/database,
/// P.S - i'll change this with noSql i'll use "MongoDB"
/// More importantly, the login are through this class only!
/// </summary>
namespace AirlineManagementSystem.FlightsCenter
{
    public class FlightsCenterSystem
    {
        private static FlightsCenterSystem _instance;
        private static object key = new object();
        private LoginService loginService = new LoginService();
        private FlightsCenterSystem()
        {
            FlightsDAOPGSQL flight = new FlightsDAOPGSQL();
            var reader = File.OpenText("ConnectionStringConfig.txt");
            string connection_string = reader.ReadToEnd();
            Task task = new Task(() =>
            {
                using (var con = new NpgsqlConnection(connection_string))
                {
                    while (true)
                    {
                        Thread.Sleep(100000);
                        flight.GetAll().ToList().ForEach(a =>
                        {
                            if (a.LandingTime < DateTime.Now.AddHours(5))
                            {
                                con.Open();
                                NpgsqlCommand cmd = new NpgsqlCommand("sp_add_to_history", con);
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.ExecuteNonQuery();
                                flight.Remove(a);
                            }
                        });
                    }
                }
            });
            task.RunSynchronously();
        }

        public static FlightsCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlightsCenterSystem();
                    }
                }
            }
            return _instance;
        }

        public void login(string userName, string password, out LoginToken<IUser> token, out FacadeBase facade)
        {
            loginService.TryLogin(userName, password, out LoginToken<IUser> l, out FacadeBase fb);
            token = l;
            facade = fb;
        }
    }
}
