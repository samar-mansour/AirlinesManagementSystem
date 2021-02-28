using AirlineManagementSystem.BusinessLogic_Facades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirlineManagementSystem.Login
{
    public class LoginService : ILoginService
    {
        private static object singletone_key = new object();
        private static object key = new object();
        private static LoginService m_Instance;

        private IAirlineCompanyDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        private IAdministratorsDAO _adminDAO;
        private IUsersDAO _userDAO;

        public static LoginService GetInstance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (singletone_key)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new LoginService();
                        }
                    }
                }
                return m_Instance;

            }
        }
        private LoginService()
        {

        }

        public bool TryLogin(string userName, string password, out LoginToken<IUser> token, out FacadeBase facadeBase)
        {
            Users users = new Users();

            if (userName is null)
                throw new ArgumentNullException($"username cannot be: {userName}");
            if (password is null)
                throw new ArgumentNullException($"password cannot be: {password}");

            //in case it's admin "level 4"
            if (userName == "admin" && password == "9999")
            {
                if (_adminDAO.GetUserByUsername(userName).Equals(_userDAO.GetUserByUsername(userName)))
                {
                    if (users.Password == password)
                    {
                        token = new LoginToken<Administrators> 
                        { 
                            User = new Administrators() 
                            { 
                                FirstName = "admin",
                                Level = 4 
                            } 
                        };
                        facadeBase = new LoggedInAdministratorFacade();
                        return true;
                    }
                }
                
            }

            if (UserAuthentication.IsUserAuthorized(userName, password))
            {
                if (token.User is LoginToken<Customer>)
                {

                }
                token = ; 
                return true;
            }
            token = null;
            return false;
        }
    }
}
