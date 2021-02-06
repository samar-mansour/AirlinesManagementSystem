using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public interface ILoggedInCustomerFacade
    {
        IList<Flights> GetAllMyFlights(LoginToken<Customer> token);
        Tickets PurchaseTicket(LoginToken<Customer> token, Flights flight);
        void CancelTicket(LoginToken<Customer> token, Tickets ticket);
    }
}
