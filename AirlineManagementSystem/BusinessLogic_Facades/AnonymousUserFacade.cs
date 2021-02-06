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
            throw new NotImplementedException();
        }

        public Dictionary<Flights, int> GetAllFlightVacancy()
        {
            throw new NotImplementedException();
        }

        public Flights GetFlightById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetFlights()
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
    }
}
