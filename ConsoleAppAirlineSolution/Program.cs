using System;
using AirlineManagementSystem;
namespace ConsoleAppAirlineSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            CountryDAOPGSQL country = new CountryDAOPGSQL();
            Console.WriteLine(ConnectionHelper.TestConnection(AppConfigFile.GetInstance().ConnectionString));
            Tickets a = new Tickets();
            //Console.WriteLine(GetAll());
        }
    }
}
