using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public abstract class FacadeBase
    {
        protected IAirlineCompanyDAO _airlineDAO;
        protected ICountryDAO _countryDAO;
        protected ICustomerDAO _customerDAO;
        protected IAdministratorsDAO _adminDAO;
        protected IUsersDAO _userDAO;
        protected IFlightDAO _flightDAO;
        protected ITicketsDAO _ticketDAO;
    }
}
