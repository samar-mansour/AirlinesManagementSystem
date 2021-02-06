using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public interface IAnonymousUserFacade
    {
        IList<Flights> GetFlights();
        IList<AirlineCompany> GetAllAirlineCompanies();
        Dictionary<Flights, int> GetAllFlightVacancy();
        Flights GetFlightById(int id);
        IList<Flights> GetFlightsByOriginCountry(int countryCode);
        IList<Flights> GetFlightsByDestinationCountry(int countryCode);
        IList<Flights> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flights> GetFlightsByLandingDate(DateTime landingDate);

    }
}
