using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public interface ILoggedInAirlineFacade
    {
        IList<Tickets> GetAllTickets(LoginToken<AirlineCompany> token);
        IList<Tickets> GetAllFlights(LoginToken<AirlineCompany> token);
        void CancelFlight(LoginToken<AirlineCompany> token, Flights flight);
        void CreateFlight(LoginToken<AirlineCompany> token, Flights flight);
        void UpdateFlight(LoginToken<AirlineCompany> token, Flights flight);
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword);
        void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline);
    }
}
