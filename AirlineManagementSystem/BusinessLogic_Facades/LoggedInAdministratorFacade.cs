using AirlineManagementSystem.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// shows the ability to each different admin level: change, add, or remove. To:
/// {airline companies, customers, administrators} 
/// throws different exceptions, whether the admin/ airline/ customer is exsists.
/// </summary>
namespace AirlineManagementSystem.BusinessLogic_Facades
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        public void CreateAdmin(LoginToken<Administrators> token, Administrators admin)
        {
            if (token != null)
            {
                if (!(_adminDAO.Get((int)admin.UserID).Equals(token.User.ID)) && (token.User.Level == 3 || token.User.Level == 4))
                {
                    _adminDAO.Add(admin);
                }
                throw new AdminAlreadyExistsException($"Admin is already exists.\n Admin info: {admin.ID} {admin.FirstName} {admin.LastName}");
                throw new AdminLevelNotQualifiedToCreateException($"Admin level: {admin.Level} not qualified to create new admin");
            }
            throw new ArgumentNullException();
        }

        public void CreateNewAirline(LoginToken<Administrators> token, AirlineCompany airline)
        {
            if (token != null)
            {
                if (!(_airlineDAO.Get(airline.UserId).Equals(token.User.ID)) && (token.User.Level == 3 || token.User.Level == 4))
                {
                    _airlineDAO.Add(airline);
                }
                throw new AirlineAlreadyExistsException("Airline is already exists.\n Airline info: " +
                                                        $"{airline.ID} {airline.Name}");
            }
            throw new ArgumentNullException();
        }

        public void CreateNewCustomer(LoginToken<Administrators> token, Customer customer)
        {
            if (token != null)
            {
                if (!(_customerDAO.Get((int)customer.UserId).Equals(token.User.ID)) && (token.User.Level == 3 || token.User.Level == 4))
                {
                    _customerDAO.Add(customer);
                }
                throw new CustomerAlreadyExistsException($"Customer is already exists.\n Admin info: " +
                                                        $"{customer.ID} {customer.FirstName} {customer.LastName} {customer.PhoneNo}");
            }
            throw new ArgumentNullException();
        }

        public IList<Customer> GetAllCustomers(LoginToken<Administrators> token)
        {
            if (token != null)
            {
                return _customerDAO.GetAll();
            }
            return null;
        }

        public void RemoveAdmin(LoginToken<Administrators> token, Administrators admin)
        {
            if (token != null)
            {
                if (token.User.Level >= 3 && token.User.Level <= 4)
                {
                    _adminDAO.Remove(admin);
                }
                Console.WriteLine($"you cannot change or remove higher admin level");
            }
            throw new ArgumentNullException();
        }

        public void RemoveAirline(LoginToken<Administrators> token, AirlineCompany airline)
        {
            if (token != null)
            {
                if (token.User.Level == 1)
                {
                    Console.WriteLine($"admin level: {token.User.Level}, not qualified to change or remove customers");
                }
                if (token.User.Level > 1 && token.User.Level < 5)
                {
                    _airlineDAO.Remove(airline);
                }
            }
            throw new ArgumentNullException();
        }

        public void RemoveCustomer(LoginToken<Administrators> token, Customer customer)
        {
            if (token != null)
            {
                if (token.User.Level == 1)
                {
                    Console.WriteLine($"admin level: {token.User.Level}, not qualified to change or remove customers");
                }
                if (token.User.Level > 1 && token.User.Level < 5)
                {
                    _customerDAO.Remove(customer);
                }
            }
            throw new ArgumentNullException();
        }

        public void UpdateAdmin(LoginToken<Administrators> token, Administrators admin)
        {
            if (token != null)
            {
                if (token.User.Level >= 3 && token.User.Level <= 4)
                {
                    _adminDAO.Update(admin);
                }
                Console.WriteLine($"admin level: {token.User.Level}, don't have the ability to change or update other higher admin level");
            }
            throw new ArgumentNullException();
        }

        public void UpdateAirlineDetails(LoginToken<Administrators> token, AirlineCompany airline)
        {
            if (token != null)
            {
                if (token.User.Level > 0 && token.User.Level <= 4)
                {
                    _airlineDAO.Update(airline);
                }
            }
            throw new ArgumentNullException();
        }

        public void UpdateCustomerDetails(LoginToken<Administrators> token, Customer customer)
        {
            if (token != null)
            {
                if (token.User.Level > 0 && token.User.Level < 5)
                {
                    _customerDAO.Update(customer);
                }
            }
            throw new ArgumentNullException();
        }
    }
}
