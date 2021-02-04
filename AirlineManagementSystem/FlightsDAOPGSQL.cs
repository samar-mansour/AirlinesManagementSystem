using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class FlightsDAOPGSQL : ConnectionHelper, IFlightDAO
    {
        public void Add(Flights t)
        {
            var res_sp_add = Run_Sp(m_conn, "sp_add_flights", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_airlineID", t.AirlineCompId),
                new NpgsqlParameter("_originCountry", t.OriginCountryId),
                new NpgsqlParameter("_destinationCountry", t.DestinationCountryId),
                new NpgsqlParameter("_departureTime", t.DepartureTime),
                new NpgsqlParameter("_landingTime", t.LandingTime),
                new NpgsqlParameter("_remainingTickets", t.RemainingTickets)
            });
            if (res_sp_add != null)
                t.RemainingTickets--;
            Console.WriteLine($"Added new flight successfully");
        }

        public Flights Get(int id)
        {
            List<Flights> flightList = new List<Flights>();
            Flights flight = new Flights();
            var res_sp_get = Run_Sp(m_conn, "sp_get_flight_by_id", new NpgsqlParameter[]
            {
                new NpgsqlParameter("f_id", id)
            });
            flightList.AddRange((IEnumerable<Flights>)res_sp_get);
            flight = (Flights)flightList.Select(a => res_sp_get);
            return flight;
        }

        public IList<Flights> GetAll()
        {
            IList<Flights> flight = new List<Flights>();
            Flights reader = new Flights();

            var res_sp_get_all = Run_Sp(m_conn, "sp_get_all_flights", new NpgsqlParameter[]
            {
                new NpgsqlParameter("id", reader.ID),
                new NpgsqlParameter("airline", reader.AirlineCompId),
                new NpgsqlParameter("originCountry", reader.OriginCountryId),
                new NpgsqlParameter("destinationCountry", reader.DestinationCountryId),
                new NpgsqlParameter("departureTime", reader.DepartureTime),
                new NpgsqlParameter("landingTime", reader.LandingTime),
                new NpgsqlParameter("remainingTickets", reader.RemainingTickets)

            });
            flight = (IList<Flights>)res_sp_get_all.Select(item => item.Values).ToList();
            return flight;
        }

        //need to fix
        public Dictionary<Flights, int> GetAllFlightsVacancy()
        {
            Dictionary<Flights, int> flight = new Dictionary<Flights, int>();
            Flights reader = new Flights();
            if (reader.RemainingTickets != 0)
            {
                var sp_get_all_flight_vacancy = Run_Sp(m_conn, "sp_get_all_flight_vacancy", new NpgsqlParameter[]
                {
                new NpgsqlParameter("airline", reader.AirlineCompId),
                new NpgsqlParameter("originCountry", reader.OriginCountryId),
                new NpgsqlParameter("destinationCountry", reader.DestinationCountryId),
                new NpgsqlParameter("departureTime", reader.DepartureTime),
                new NpgsqlParameter("landingTime", reader.LandingTime),
                new NpgsqlParameter("remainingTickets", reader.RemainingTickets)
                });
                flight.Add(sp_get_all_flight_vacancy.ForEach(value => sp_get_all_flight_vacancy), reader.ID);
                reader = (Flights)flight.Select(a => sp_get_all_flight_vacancy);
            }
            else
            {
                Console.WriteLine($"No remaining tickets!");
            }
            return flight;
        }

        public Flights GetFlightById(int id)
        {
            Flights flight = new Flights();
            List<Flights> flightList = new List<Flights>();

            Customer customer = new Customer();
            var res_flight_by_customerID = Run_Sp(m_conn, "sp_get_flight_by_customer", new NpgsqlParameter[]
            {
                new NpgsqlParameter("customer", customer.FirstName)
            });
            flightList.AddRange((IEnumerable<Flights>)res_flight_by_customerID);
            flight = (Flights)flightList.Select(a => res_flight_by_customerID);
            return flight;
        }

        public IList<Flights> GetFlightsByCustomer(Customer customer)
        {
            IList<Flights> flight = new List<Flights>();
            Customer reader = new Customer();

            var res_flight_by_customer = Run_Sp(m_conn, "res_flight_by_customer", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_id", reader.ID)

            });
            flight = (IList<Flights>)res_flight_by_customer.Select(item => item.Values).ToList();
            return flight;
        }

        public IList<Flights> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            List<Flights> flight = new List<Flights>();
            var res_sp_get_departure_date = Run_Sp(m_conn, "sp_get_flight_by_departure_date", new NpgsqlParameter[]
            {
                new NpgsqlParameter("departureDate", departureDate)
            });
            flight.AddRange((IEnumerable<Flights>)res_sp_get_departure_date);
            return flight;
        }

        public IList<Flights> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flights> flight = new List<Flights>();
            var res_sp_get_destination = Run_Sp(m_conn, "sp_get_flight_by_destination_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("country_code", countryCode)
            });
            flight.AddRange((IEnumerable<Flights>)res_sp_get_destination);
            return flight;
        }

        public IList<Flights> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flights> flight = new List<Flights>();
            var res_flight_by_landing_date = Run_Sp(m_conn, "sp_get_flight_by_landing_date", new NpgsqlParameter[]
            {
                new NpgsqlParameter("landingDate", landingDate)
            });
            flight.AddRange((IEnumerable<Flights>)res_flight_by_landing_date);
            return flight;
        }

        public IList<Flights> GetFlightsByOriginCountry(int countryCode)
        {
            List<Flights> flight = new List<Flights>();
            var res_flight_origin = Run_Sp(m_conn, "sp_get_flight_by_origin_country", new NpgsqlParameter[]
            {
                new NpgsqlParameter("country_code", countryCode)
            });
            flight.AddRange((IEnumerable<Flights>)res_flight_origin);
            return flight;
        }

        public void Remove(Flights t)
        {
            var res_sp_remove = Run_Sp(m_conn, "sp_remove_flight", new NpgsqlParameter[]
            {
                new NpgsqlParameter("f_id",t.ID)
            });
            Console.WriteLine($"{t.ID} was successfully removed");
        }

        public void Update(Flights t)
        {
            Console.Write("Enter user ID to update information: ");
            int newID = Convert.ToInt32(Console.ReadLine());
            var res_sp_update = Run_Sp(m_conn, "sp_update_airline_company", new NpgsqlParameter[]
            {
                new NpgsqlParameter("_airlineID",t.AirlineCompId),
                new NpgsqlParameter("_originCountry", t.OriginCountryId),
                new NpgsqlParameter("_destinationCountry", t.DestinationCountryId),
                new NpgsqlParameter("_departureTime", t.DepartureTime),
                new NpgsqlParameter("_landingTime", t.LandingTime),
                new NpgsqlParameter("_remainingTickets", t.RemainingTickets),
                new NpgsqlParameter("new_id", newID)
            });
            Console.WriteLine($"Successfully Updated users information => [{res_sp_update}]");
        }
    }
}
