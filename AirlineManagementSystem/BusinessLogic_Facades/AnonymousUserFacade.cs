using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        public Dictionary<Flights, int> GetAllFlightVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }

        public Flights GetFlightById(int id)
        {
            if (id < 0)
            {
                throw new NegativeIdException($"ID cannot be negative: {id}");
            }
            return _flightDAO.GetFlightById(id);
        }

        public IList<Flights> GetFlights()
        {
            return _flightDAO.GetAll();
        }

        public IList<Flights> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepatrureDate(departureDate);
        }

        public IList<Flights> GetFlightsByDestinationCountry(int countryCode)
        {
            if (countryCode < 0)
            {
                throw new NegativeIdException($"Country code cannot be negative: {countryCode}");
            }
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        public IList<Flights> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        public IList<Flights> GetFlightsByOriginCountry(int countryCode)
        {
            if (countryCode < 0)
            {
                throw new NegativeIdException($"Country code cannot be negative: {countryCode}");
            }
            return _flightDAO.GetFlightsByOriginCountry(countryCode);
        }
    }
}
