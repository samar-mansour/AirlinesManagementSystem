using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem
{
    public interface IFlightDAO : IBasicDb<Flights>
    {
        Dictionary<Flights, int> GetAllFlightsVacancy();
        Flights GetFlightById(int id);
        IList<Flights> GetFlightsByOriginCountry(int countryCode);
        IList<Flights> GetFlightsByDestinationCountry(int countryCode);
        IList<Flights> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flights> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flights> GetFlightsByCustomer(Customer customer);

    }
}
