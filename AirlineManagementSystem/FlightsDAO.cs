using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public class FlightsDAO : ConnectionHelper, IFlightDAO
    {
        public void Add(Flights t)
        {
            throw new NotImplementedException();
        }

        public Flights Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetAll()
        {
            throw new NotImplementedException();
        }

        public Dictionary<Flights, int> GetAllFlightsVacancy()
        {
            throw new NotImplementedException();
        }

        public Flights GetFlightById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetFlightsByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetFlightsByDestinationCountry(int countryCode)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetFlightsByLandingDate(DateTime landingDate)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetFlightsByOriginCountry(int countryCode)
        {
            throw new NotImplementedException();
        }

        public void Remove(Flights t)
        {
            throw new NotImplementedException();
        }

        public void Update(Flights t)
        {
            throw new NotImplementedException();
        }
    }
}
