using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public void CancelTicket(LoginToken<Customer> token, Tickets ticket)
        {
            throw new NotImplementedException();
        }

        public IList<Flights> GetAllMyFlights(LoginToken<Customer> token)
        {
            throw new NotImplementedException();
        }

        public Tickets PurchaseTicket(LoginToken<Customer> token, Flights flight)
        {
            throw new NotImplementedException();
        }
    }
}
