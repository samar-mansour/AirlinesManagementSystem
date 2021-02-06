using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        public void CreateAdmin(LoginToken<Administrators> token, Administrators admin)
        {
            throw new NotImplementedException();
        }

        public void CreateNewAirline(LoginToken<Administrators> token, AirlineCompany airline)
        {
            throw new NotImplementedException();
        }

        public void CreateNewCustomer(LoginToken<Administrators> token, Customer customer)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> GetAllCustomers(LoginToken<Administrators> token)
        {
            throw new NotImplementedException();
        }

        public void RemoveAdmin(LoginToken<Administrators> token, Administrators admin)
        {
            throw new NotImplementedException();
        }

        public void RemoveAirline(LoginToken<Administrators> token, AirlineCompany airline)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(LoginToken<Administrators> token, Customer customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdmin(LoginToken<Administrators> token, Administrators admin)
        {
            throw new NotImplementedException();
        }

        public void UpdateAirlineDetails(LoginToken<Administrators> token, AirlineCompany airline)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomerDetails(LoginToken<Administrators> token, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
