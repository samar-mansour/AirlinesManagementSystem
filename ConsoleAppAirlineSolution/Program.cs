using System;
using AirlineManagementSystem;
namespace ConsoleAppAirlineSolution
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            CountryDAOPGSQL country = new CountryDAOPGSQL();
            Console.WriteLine(ConnectionDataInfo.TestConnection(AppConfigFile.GetInstance().ConnectionString));
            //Tickets a = new Tickets();

            Administrators a = new Administrators() {FirstName = "Patreck", LastName = "Sky", Level = 1, UserID = 2 };
            
            AdministratorsDAOPGSQL admin = new AdministratorsDAOPGSQL();
            admin.Add(a);
            //admin.Get(2);
            //admin.GetAll();
            
        }
    }
}
